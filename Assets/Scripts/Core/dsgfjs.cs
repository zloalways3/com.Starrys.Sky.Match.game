using System;
using System.Collections.Generic;
using Core.Api;
using DG.Tweening;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Core
{
    public class dsgfjs : dfghsjsdf
    {
        private const int FieldResX = 6;
        private const int FieldResY = 6;

        private readonly GameObject _fieldParent;
        private readonly GameObject _fieldBg;
        private readonly Vector2 _offset;
        private readonly Cell[,] _fieldMatrix = new Cell[FieldResX, FieldResY];
        private readonly Vector3[,] _worldPositions = new Vector3[FieldResX, FieldResY];
        private readonly int _typeCount = Enum.GetValues(typeof(CellAtlas.CellType)).Length - 1;

        private readonly Vector2Int[] _neighborVectors =
            { Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down };

        private Cell _selected;
        private Cell _firstChange;
        private Cell _secondChange;
        private bool _cheatsEnabled = false;

        public event Action OnAnimationStateStarted;
        public event Action OnAnimationStateEnded;

        public dsgfjs(GameObject fieldParent, Cell reference, GameObject fieldBg)
        {
            _fieldParent = fieldParent;
            _fieldBg = fieldBg;
            _offset = reference.BackgroundRenderer.size;

            InitField();
        }

        public void OnNewLevel()
        {
            //InitField();
            //ResetField();
            GenerateField(true);
        }

        public void OnCellClicked(Cell cell)
        {
            if (_selected == null)
            {
                Select(cell);
                adsfhsa.Get<ClickAdsfhjed>().Play();
            }
            else if (_selected != null)
            {
                //Select(cell);
                TryChangeCellPositions(first: _selected, second: cell);
                adsfhsa.Get<ClickAdsfhjed>().Play();
                Deselect();
            }
            else
            {
                Deselect();
            }
        }

        public void Deselect()
        {
            if (_selected != null)
                _selected.transform.localScale = Vector3.one;

            _selected = null;
        }

        public void SetFieldVisibility(bool isVisible)
        {
            if (_fieldBg != null) _fieldBg.SetActive(isVisible);

            for (int i = 0; i < _fieldMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < _fieldMatrix.GetLength(1); j++)
                {
                    if (_fieldMatrix[i, j] == null)
                        continue;

                    _fieldMatrix[i, j].BackgroundRenderer.enabled = isVisible;
                    _fieldMatrix[i, j].IconRenderer.enabled = isVisible;
                    _fieldMatrix[i, j].GetComponent<Collider2D>().enabled = isVisible;
                }
            }
        }

        public void ResetField() => GenerateField();

        private void Select(Cell cell)
        {
            _selected = cell;
            _selected.transform.DOScale(Vector3.one * 1.2f, .1f);
        }

        private void TryChangeCellPositions(Cell first, Cell second)
        {
            if (!CanChange(first, second))
                return;

            first.BackgroundRenderer.sortingOrder += 2;
            first.IconRenderer.sortingOrder += 2;

            var firstTween = first.transform.DOMove(second.transform.position, .5f);
            var secondTween = second.transform.DOMove(first.transform.position, .5f);
            _firstChange = first;
            _secondChange = second;

            firstTween.onComplete += OnChangeAnimComplete;

            OnAnimationStateStarted?.Invoke();
        }

        private void OnChangeAnimComplete()
        {
            _firstChange.BackgroundRenderer.sortingOrder -= 2;
            _firstChange.IconRenderer.sortingOrder -= 2;

            SwapCells(_firstChange, _secondChange);

            OnAnimationStateEnded?.Invoke();

            DeleteMatches();
            while (!HasValidTurn())
            {
                GenerateField();
            }
        }

        private bool CanChange(Cell first, Cell second)
        {
            bool isNeighbor =
                (Mathf.Abs(first.Position.x - second.Position.x) <= 1 && first.Position.y == second.Position.y) ||
                (first.Position.x - second.Position.x == 0 && Mathf.Abs(first.Position.y - second.Position.y) <= 1);

            SwapCells(first, second);
            bool hasMatches = FindMatches().Count != 0;
            SwapCells(first, second);

            return _cheatsEnabled || (isNeighbor && hasMatches);
        }

        private void SwapCells(Cell first, Cell second)
        {
            _fieldMatrix[first.Position.x, first.Position.y] = second;
            _fieldMatrix[second.Position.x, second.Position.y] = first;

            (first.Position, second.Position) = (second.Position, first.Position);
        }

        private void InitField()
        {
            for (int i = 0; i < _fieldParent.transform.childCount; i++)
            {
                var rowParent = _fieldParent.transform.GetChild(i);

                for (int j = 0; j < rowParent.childCount; j++)
                {
                    Cell cell = rowParent.GetChild(j).GetComponent<Cell>();

                    if (cell.gameObject.activeSelf)
                    {
                        _fieldMatrix[i, j] = cell;
                        _fieldMatrix[i, j].Position = new Vector2Int(i, j);
                        _worldPositions[i, j] = _fieldMatrix[i, j].transform.position;
                    }
                    else
                    {
                        _fieldMatrix[i, j] = null;
                        _worldPositions[i, j] = cell.transform.position;
                    }
                }
            }

            GenerateField(true);
            while (!HasValidTurn())
            {
                GenerateField();
            }
        }

        private bool IsValidPosition(Vector2Int position)
        {
            return IsValidPosition(position.x, position.y);
        }

        private bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < FieldResX && y >= 0 && y < FieldResY && _fieldMatrix[x, y] != null;
        }

        private bool HasValidTurn()
        {
            for (int i = 0; i < FieldResX; i++)
            {
                for (int j = 0; j < FieldResY; j++)
                {
                    if (HasThreeEqualNeighbors(_fieldMatrix[i, j]))
                        return true;

                    foreach (var neighbor in _neighborVectors)
                    {
                        if (_fieldMatrix[i, j] == null || !IsValidPosition(i + neighbor.x, j + neighbor.y))
                            continue;

                        if (_fieldMatrix[i, j].Type == _fieldMatrix[i + neighbor.x, j + neighbor.y].Type)
                        {
                            if (IsValidTurn(_fieldMatrix[i, j], _fieldMatrix[i + neighbor.x, j + neighbor.y]))
                                return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool HasThreeEqualNeighbors(Cell cell)
        {
            if (cell == null)
                return false;

            var neighborTypes = new Dictionary<CellAtlas.CellType, int>();
            foreach (var neighborVector in _neighborVectors)
            {
                if (!IsValidPosition(cell.Position + neighborVector))
                    continue;

                Cell neighbor = _fieldMatrix[cell.Position.x + neighborVector.x, cell.Position.y + neighborVector.y];
                if (neighborTypes.ContainsKey(neighbor.Type))
                {
                    neighborTypes[neighbor.Type]++;
                }
                else
                {
                    neighborTypes.Add(neighbor.Type, 1);
                }
            }

            foreach (var typeCount in neighborTypes.Values)
            {
                if (typeCount >= 3)
                    return true;
            }

            return false;
        }

        private bool IsValidTurn(Cell first, Cell second)
        {
            if (first.Type != second.Type)
                return false;

            List<Cell> cellsToCheck = new List<Cell>();


            if (first.Position.x == second.Position.x)
            {
                if (Math.Min(first.Position.y, second.Position.y) - 1 >= 0)
                {
                    cellsToCheck.Add(_fieldMatrix[first.Position.x, Math.Min(first.Position.y, second.Position.y) - 1]);
                }

                if (Math.Max(first.Position.y, second.Position.y) + 1 < FieldResY)
                {
                    cellsToCheck.Add(_fieldMatrix[first.Position.x, Math.Max(first.Position.y, second.Position.y) + 1]);
                }
            }
            else
            {
                if (Math.Min(first.Position.x, second.Position.x) - 1 >= 0)
                {
                    cellsToCheck.Add(_fieldMatrix[Math.Min(first.Position.x, second.Position.x) - 1, first.Position.y]);
                }

                if (Math.Max(first.Position.x, second.Position.x) + 1 < FieldResY)
                {
                    cellsToCheck.Add(_fieldMatrix[Math.Max(first.Position.x, second.Position.x) + 1, first.Position.y]);
                }
            }

            foreach (var cell in cellsToCheck)
            {
                int countEqual = 0;

                foreach (var neighbor in _neighborVectors)
                {
                    if (cell == null || !IsValidPosition(cell.Position + neighbor))
                        continue;

                    if (_fieldMatrix[cell.Position.x + neighbor.x, cell.Position.y + neighbor.y].Type == first.Type)
                    {
                        countEqual++;
                    }
                }

                if (countEqual > 1)
                {
                    return true;
                }
            }

            return false;
        }

        private void DeleteMatches()
        {
            var matches = FindMatches();
            if (matches.Count == 0)
                return;

            int countDeleteCells = 0;

            foreach (var match in matches)
            {
                countDeleteCells += match.Item1.Count + match.Item2.Count;

                foreach (var cell in match.Item1)
                {
                    adsfhsa.Get<adfshazasdf>().UpdateScoreType(cell.Type);
                    adsfhsa.Get<dfgjrssdertaes>().Push(cell);
                }

                foreach (var cell in match.Item2)
                {
                    if (cell.gameObject.activeSelf)
                    {
                        adsfhsa.Get<adfshazasdf>().UpdateScoreType(cell.Type);
                        adsfhsa.Get<dfgjrssdertaes>().Push(cell);
                    }
                }
            }

            adsfhsa.Get<adfshazasdf>().OnMatch(countDeleteCells);
            DropCells();
            DeleteMatches();
        }

        private void DropCells()
        {
            for (int j = 0; j < FieldResY; j++)
            {
                var emptyPositions = new List<int>();
                for (int i = FieldResX - 1; i >= 0; i--)
                {
                    if (_fieldMatrix[i, j] == null)
                        continue;

                    if (_fieldMatrix[i, j].Type == CellAtlas.CellType.None)
                    {
                        emptyPositions.Add(i);
                        continue;
                    }

                    if (emptyPositions.Count > 0) // moving existing
                    {
                        int newI = emptyPositions[0];
                        emptyPositions.RemoveAt(0);

                        _fieldMatrix[i, j].Position = new Vector2Int(newI, j);
                        _fieldMatrix[newI, j] = _fieldMatrix[i, j];
                        _fieldMatrix[newI, j].transform.DOMove(_worldPositions[newI, j], .5f);

                        emptyPositions.Add(i);
                    }
                }

                foreach (var position in emptyPositions)
                {
                    AddCell(position, j);
                }
            }
        }

        private void AddCell(int x, int y)
        {
            _fieldMatrix[x, y] = adsfhsa.Get<dfgjrssdertaes>().dfgshjsdjs();
            _fieldMatrix[x, y].gameObject.transform.position = _worldPositions[0, y] + Vector3.up * _offset.y;
            _fieldMatrix[x, y].Position = new Vector2Int(x, y);
            _fieldMatrix[x, y].Type = (CellAtlas.CellType)UnityEngine.Random.Range(0, _typeCount);
            _fieldMatrix[x, y].transform.DOMove(_worldPositions[x, y], .5f);
        }


        private void GenerateField(bool isFirst = false)
        {
            for (int i = 0; i < FieldResX; i++)
            {
                for (int j = 0; j < FieldResY; j++)
                {
                    if (_fieldMatrix[i, j] == null)
                        continue;

                    Sequence seq = DOTween.Sequence();
                    if (isFirst)
                    {
                        _fieldMatrix[i, j].transform.localScale = Vector3.zero;
                    }

                    seq.Append(_fieldMatrix[i, j].transform.DOScale(Vector3.zero, .5f));
                    seq.AppendInterval(.25f);
                    seq.Append(_fieldMatrix[i, j].transform.DOScale(Vector3.one, .5f));
                    _fieldMatrix[i, j].Type = (CellAtlas.CellType)UnityEngine.Random.Range(0, _typeCount);
                    seq.Play();
                }
            }

            var matches = FindMatches();
            foreach (var match in matches)
            {
                for (int i = 0; i < match.Item1.Count; i += 3)
                {
                    ReplaceType(match.Item1[i]);
                }

                for (int i = 0; i < match.Item2.Count; i += 3)
                {
                    ReplaceType(match.Item2[i]);
                }
            }
        }

        private void ReplaceType(Cell cell)
        {
            HashSet<int> occupiedTypes = new HashSet<int>();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (Math.Abs(i + j) != 1 || !IsValidPosition(cell.Position.x + i, cell.Position.y + j))
                        continue;

                    occupiedTypes.Add((int)_fieldMatrix[cell.Position.x + i, cell.Position.y + j].Type);
                }
            }

            int newType = UnityEngine.Random.Range(0, _typeCount);
            while (occupiedTypes.Contains(newType))
            {
                newType = UnityEngine.Random.Range(0, _typeCount);
            }

            cell.Type = (CellAtlas.CellType)newType;
        }

        private List<Tuple<List<Cell>, List<Cell>>> FindMatches()
        {
            var matches = new List<Tuple<List<Cell>, List<Cell>>>();
            bool[][] inMatch = new bool[FieldResX][];
            for (int index = 0; index < FieldResX; index++)
            {
                inMatch[index] = new bool[FieldResY];
            }

            for (int i = 0; i < FieldResX; i++)
            {
                for (int j = 0; j < FieldResY; j++)
                {
                    if (!inMatch[i][j])
                    {
                        var match = GetMatch(_fieldMatrix[i, j]);
                        if (match != null && (match.Item1.Count > 0 || match.Item2.Count > 0))
                        {
                            matches.Add(match);
                            foreach (var cell in match.Item1)
                            {
                                inMatch[cell.Position.x][cell.Position.y] = true;
                            }

                            foreach (var cell in match.Item2)
                            {
                                inMatch[cell.Position.x][cell.Position.y] = true;
                            }
                        }
                    }
                }
            }

            return matches;
        }

        private Tuple<List<Cell>, List<Cell>> GetMatch(Cell cell)
        {
            if (cell == null)
                return null;

            List<Cell> verticalMatch = new List<Cell>();
            List<Cell> horizontalMatch = new List<Cell>();
            int upMatch = cell.Position.y;
            int downMatch = cell.Position.y;
            int leftMatch = cell.Position.x;
            int rightMatch = cell.Position.x;

            while (IsValidPosition(cell.Position.x, upMatch - 1) &&
                   _fieldMatrix[cell.Position.x, upMatch - 1].Type == cell.Type)
            {
                upMatch--;
            }

            while (IsValidPosition(cell.Position.x, downMatch + 1) &&
                   _fieldMatrix[cell.Position.x, downMatch + 1].Type == cell.Type)
            {
                downMatch++;
            }

            while (IsValidPosition(leftMatch - 1, cell.Position.y) &&
                   _fieldMatrix[leftMatch - 1, cell.Position.y].Type == cell.Type)
            {
                leftMatch--;
            }

            while (IsValidPosition(rightMatch + 1, cell.Position.y) &&
                   _fieldMatrix[rightMatch + 1, cell.Position.y].Type == cell.Type)
            {
                rightMatch++;
            }

            if (downMatch - upMatch >= 2)
            {
                for (int i = upMatch; i <= downMatch; i++)
                {
                    verticalMatch.Add(_fieldMatrix[cell.Position.x, i]);
                }
            }

            if (rightMatch - leftMatch >= 2)
            {
                for (int i = leftMatch; i <= rightMatch; i++)
                {
                    horizontalMatch.Add(_fieldMatrix[i, cell.Position.y]);
                }
            }

            return new Tuple<List<Cell>, List<Cell>>(verticalMatch, horizontalMatch);
        }
    }
}