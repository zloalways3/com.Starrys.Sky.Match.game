using System;
using Core.Api;
using UnityEngine;

namespace Core.PlayerInput
{
    public class fgjssfg : IUpdateListener
    {
        private bool _isDisabled;
        public event Action<Cell> OnCellClicked;
        public event Action OnDeselected;

        void IUpdateListener.OnUpdate()
        {
            if (_isDisabled)
                return;

            ListenInput();
        }

        public void Disable()
        {
            _isDisabled = true;
        }

        public void Enable()
        {
            _isDisabled = false;
        }

        private void ListenInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),  Camera.main.ScreenToWorldPoint(Input.mousePosition), Mathf.Infinity);

                if (hit.collider != null && hit.collider.TryGetComponent<Cell>(out Cell cell))
                {
                    OnCellClicked?.Invoke(cell);
                }
                else
                {
                    OnDeselected?.Invoke();
                }
            }
        }
    }
}