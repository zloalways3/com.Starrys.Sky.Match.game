using System;
using System.Collections.Generic;
using System.Linq;
using Core.Api;
using Core.PlayerInput;
using Core.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public class sdfhg : dfghsjsdf
    {
        private readonly fgjssfg _input;
        private readonly dsgfjs _field;
        private bool ___adfhj;

        public sdfhg()
        {
            _input = adsfhsa.Get<fgjssfg>();
            _field = adsfhsa.Get<dsgfjs>();

            _input.OnCellClicked += Select;
            _input.OnDeselected += Deselect;

            ___adfhj = true;

            _field.OnAnimationStateStarted += OnFieldAnimStart;
            _field.OnAnimationStateEnded += OnFieldAnimEnd;
        }

        public void OnTurn()
        {
            ___adfhj = true;
        }

        private void OnFieldAnimEnd()
        {
            OnTurn();
        }

        private void OnFieldAnimStart()
        {
            ___adfhj = false;
        }

        private void Deselect()
        {
            if (!___adfhj)
                return;

            _field.Deselect();
        }

        private void Select(Cell clicked)
        {
            if (!___adfhj)
                return;

            _field.OnCellClicked(clicked);
        }
    }

    public class dfgjrssdertaes : dfghsjsdf
    {
        private const int InitialAmount = 5;

        private readonly Cell _reference;
        private readonly List<Cell> _pool = new List<Cell>();

        public dfgjrssdertaes(Cell reference)
        {
            _reference = reference;
            for (int i = 0; i < InitialAmount; i++)
            {
                Cell instance = Object.Instantiate(reference);
                instance.gameObject.SetActive(false);

                _pool.Add(instance);
            }
        }

        public void Push(Cell cell)
        {
            cell.Type = CellAtlas.CellType.None;
            cell.gameObject.SetActive(false);
            cell.Position = Vector2Int.one * -10;
        }

        public Cell dfgshjsdjs()
        {
            if (_pool.Count == 0)
                return Object.Instantiate(_reference);

            Cell instance = _pool[0];
            _pool.RemoveAt(0);
            instance.gameObject.SetActive(true);

            return instance;
        }
    }

    public class dgstjusdfgxjs : dfghsjsdf
    {
        private readonly dsgfjs _field;
        private readonly dfshjsdfzhjds _dfshjsdfzhjds;
        private readonly adfshazasdf _adfshazasdf;
        private readonly adsfhdjsfgmasdfh _processor;
        private sadfhdshyasa _listener;

        public int dsfgjhdhjsd { get; private set; }


        public event Action OnLoad;

        public dgstjusdfgxjs()
        {
            _field = adsfhsa.Get<dsgfjs>();
            _dfshjsdfzhjds = adsfhsa.Get<dfshjsdfzhjds>();
            _adfshazasdf = adsfhsa.Get<adfshazasdf>();
            _processor = adsfhsa.Get<adsfhdjsfgmasdfh>();
        }

        public void LoadLevel(int index)
        {
            dsfgjhdhjsd = index;

            _dfshjsdfzhjds.adsfhfadshsd();
            _processor.SetPauseState(isPaused: false);
            _field.SetFieldVisibility(isVisible: true);
            _field.OnNewLevel();
            _dfshjsdfzhjds.asdfhadsfh();
            _adfshazasdf.adfhsafshddgjaf();
            _listener.Reset();

            OnLoad?.Invoke();
        }

        public void dfghjsgjdfsafhsd(sadfhdshyasa listener)
        {
            _listener = listener;
        }
    }

    public class adfshazasdf : dfghsjsdf
    {
        private const float MatchExtraCellLinearModifier = 1.1f;
        private const int DefaultMatchValue = 100;

        private List<TargetScore> _score;
        private int _commonScore;

        public int Current => _commonScore;
        public List<TargetScore> Target => _score;
        public event Action<int> OnValueChanged;

        public adfshazasdf()
        {
            var buff = adsfhsa.Get<LevelScoreConstraints>().Map[0].Score;
            _score = new List<TargetScore>();

            for (int i = 0; i < buff.Count; i++)
                _score.Add(new TargetScore() { Key = buff[i].Key, Score = 0 });
        }

        public void OnMatch(int amount)
        {
            int extraCells = 0;

            if (amount > 3)
                extraCells = amount - 3;

            // bool isMatch = _score.Any(score => score.Key == type);

            _commonScore += DefaultMatchValue +
                            Mathf.RoundToInt(extraCells * (DefaultMatchValue * MatchExtraCellLinearModifier));

            // if (isMatch)
            // {
            //     //var match = _score.First(score => score.Key == type);
            //     var buff = _score;
            //     _score = new List<TargetScore>();
            //
            //     for (int i = 0; i < buff.Count; i++)
            //     {
            //         if (type == buff[i].Key)
            //         {
            //             _score.Add(new TargetScore() { Key = buff[i].Key, Score = buff[i].Score + amount });
            //
            //             continue;
            //         }
            //
            //         _score.Add(new TargetScore() { Key = buff[i].Key, Score = buff[i].Score });
            //     }
            // }

            OnValueChanged?.Invoke(_commonScore);
        }

        public void UpdateScoreType(CellAtlas.CellType type)
        {
            bool isMatch = _score.Any(score => score.Key == type);
            if (!isMatch)
                return;

            for (int i = 0; i < _score.Count; i++)
            {
                if (type == _score[i].Key)
                {
                    _score[i] = new TargetScore() { Key = type, Score = _score[i].Score + 1 };
                    break;
                }
            }
        }

        public void adfhsafshddgjaf()
        {
            var buff = adsfhsa.Get<LevelScoreConstraints>()
                .Map[adsfhsa.Get<dgstjusdfgxjs>().dsfgjhdhjsd].Score;
            _score = new List<TargetScore>();

            for (int i = 0; i < buff.Count; i++)
                _score.Add(new TargetScore() { Key = buff[i].Key, Score = 0 });

            _commonScore = 0;
            OnValueChanged?.Invoke(_commonScore);
        }
    }

    public class dfshjsdfzhjds : IUpdateListener
    {
        private float _asdfha;
        private bool _issdfhdh;

        public int Current => Mathf.RoundToInt(_asdfha);

        void IUpdateListener.OnUpdate()
        {
            if (!_issdfhdh)
                return;

            //_timeElapsed += Time.deltaTime;
        }

        public void adsfhfadshsd() => _issdfhdh = true;
        public void fgjsjsdf() => _issdfhdh = false;
        public void asdfhadsfh() => _asdfha = 0;
    }

    public class wer42 : dfghsjsdf
    {
        private readonly List<BaseWindow> _windows;
        private readonly Canvas _canvas;
        private readonly Dictionary<Type, BaseWindow> _onScene = new Dictionary<Type, BaseWindow>();

        public wer42(List<BaseWindow> windows, Canvas canvas)
        {
            _windows = windows;
            _canvas = canvas;
        }

        public T Open<T>() where T : BaseWindow
        {
            if (!_onScene.ContainsKey(typeof(T)))
                _onScene[typeof(T)] = Object.Instantiate(_windows.First(window => window.GetType() == typeof(T)),
                    _canvas.transform);

            _onScene[typeof(T)].gameObject.SetActive(true);

            return _onScene[typeof(T)] as T;
        }

        public T Get<T>() where T : BaseWindow
        {
            if (!_onScene.ContainsKey(typeof(T)))
                _onScene[typeof(T)] = Object.Instantiate(_windows.First(window => window.GetType() == typeof(T)),
                    _canvas.transform);

            return _onScene[typeof(T)] as T;
        }
    }

    public class sadfhdshyasa : IUpdateListener
    {
        private const int TimeConstraintInSeconds = 90;

        private readonly LevelScoreConstraints _constraints;
        private readonly dfshjsdfzhjds _dfshjsdfzhjds;
        private bool _isInvoked;
        private readonly dgstjusdfgxjs _dgstjusdfgxjs;

        public event Action OnGameEnd;

        public sadfhdshyasa(dgstjusdfgxjs dgstjusdfgxjs)
        {
            adsfhsa.Get<adfshazasdf>().OnValueChanged += CheckWin;
            _dfshjsdfzhjds = adsfhsa.Get<dfshjsdfzhjds>();
            _constraints = adsfhsa.Get<LevelScoreConstraints>();
            _dgstjusdfgxjs = dgstjusdfgxjs;
        }

        private void CheckWin(int _)
        {
            List<TargetScore> current = adsfhsa.Get<adfshazasdf>().Target;

            if (!_isInvoked &&
                current[0].Score >= _constraints.Map[_dgstjusdfgxjs.dsfgjhdhjsd].Score[0].Score &&
                current[1].Score >= _constraints.Map[_dgstjusdfgxjs.dsfgjhdhjsd].Score[1].Score &&
                current[2].Score >= _constraints.Map[_dgstjusdfgxjs.dsfgjhdhjsd].Score[2].Score)
            {
                OnGameEnd?.Invoke();
                _isInvoked = true;
                adsfhsa.Get<dfshjsdfzhjds>().fgjsjsdf();
            }
        }

        void IUpdateListener.OnUpdate()
        {
            // if (!_isInvoked && _timer.Current > TimeConstraintInSeconds)
            // {
            //     OnGameEnd?.Invoke();
            //     _isInvoked = true;
            //     ServiceLocator.Get<Timer>().Stop();
            // }
        }

        public void Reset() => _isInvoked = false;
    }

    public class dfjhadfsa : dfghsjsdf
    {
        private readonly sadfhdshyasa _listener;
        private readonly LevelScoreConstraints _constraints;
        private readonly dgstjusdfgxjs _level;
        private readonly adfshazasdf _adfshazasdf;

        public enum GameResult
        {
            Win,
            Lose
        }

        public event Action<GameResult> OnGameResult;

        public dfjhadfsa()
        {
            _listener = adsfhsa.Get<sadfhdshyasa>();
            _constraints = adsfhsa.Get<LevelScoreConstraints>();
            _level = adsfhsa.Get<dgstjusdfgxjs>();
            _adfshazasdf = adsfhsa.Get<adfshazasdf>();

            _listener.OnGameEnd += GetResult;
        }

        private void GetResult()
        {
            // if (_score.Current < _constraints.Map.First(pair => pair.LevelId.Equals(_level.CurrentLevelIndex)).Score)
            // {
            //     OnGameResult?.Invoke(GameResult.Lose);
            //
            //     return;
            // }

            OnGameResult?.Invoke(GameResult.Win);
        }
    }

    public class adsrfjadsda : dfghsjsdf
    {
        private readonly LevelScoreConstraints _constraints;
        private readonly dgstjusdfgxjs _loader;
        private readonly dfjhadfsa _resolver;
        private readonly wer42 _ui;
        private readonly adfshazasdf _adfshazasdf;
        private bool[] _openedLevels;

        public bool[] OpenedLevels => _openedLevels;
        public event Action OnNewlevelOpened;

        public adsrfjadsda()
        {
            _constraints = adsfhsa.Get<LevelScoreConstraints>();
            _loader = adsfhsa.Get<dgstjusdfgxjs>();
            _resolver = adsfhsa.Get<dfjhadfsa>();
            _ui = adsfhsa.Get<wer42>();
            _adfshazasdf = adsfhsa.Get<adfshazasdf>();
            _openedLevels = new bool[_constraints.Map.Count];
            _openedLevels[0] = true;

            _resolver.OnGameResult += TryOpenLevel;
        }

        private void TryOpenLevel(dfjhadfsa.GameResult result)
        {
            adsfhsa.Get<dsgfjs>().SetFieldVisibility(false);
            EndGameWindow endWindow = _ui.Open<EndGameWindow>();
            endWindow.SetPrev(_ui.Open<GameWindow>());
            endWindow.SetResult(result);

            if (result == dfjhadfsa.GameResult.Win)
            {
                if (_loader.dsfgjhdhjsd + 1 >= _constraints.Map.Count)
                {
                    endWindow.ButtonText = "To level menu";
                    endWindow.Init(dfjhadfsa.GameResult.Win, true);
                }
                else
                {
                    endWindow.Init(dfjhadfsa.GameResult.Win, false);
                    _openedLevels[_loader.dsfgjhdhjsd + 1] = true;
                    OnNewlevelOpened?.Invoke();
                }

                endWindow.Score = _adfshazasdf.Current.ToString();
                return;
            }

            endWindow.Init(dfjhadfsa.GameResult.Lose, false);
        }
    }

    public class adsfhjed : dfghsjsdf
    {
        protected readonly AudioSource _source;

        public float Volume
        {
            get => _source.volume;
            set => _source.volume = value;
        }

        public adsfhjed(AudioSource source)
        {
            _source = source;
        }
    }

    public class ClickAdsfhjed : adsfhjed
    {
        public ClickAdsfhjed(AudioSource source) : base(source)
        {
        }

        public void Play()
        {
            _source.Play();
        }
    }
}