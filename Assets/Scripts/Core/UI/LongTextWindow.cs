using TMPro;
using UnityEngine;

namespace Core.UI
{
    public class LongTextWindow : BaseWindow
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_Text _label;
        
        private BaseWindow _prevWindow;

        public string Text
        {
            get => _text.text;
            set => _text.text = value;
        }

        public string Label
        {
            get => _label.text;
            set => _label.text = value;
        }

        public void SetPrevWindow(BaseWindow window)
        {
            _prevWindow = window;
        }

        protected override void Close()
        {
            _prevWindow.gameObject.SetActive(true);
            if (_prevWindow is GameWindow gameWindow)
            {
                gameWindow.Unpause();
            }
            
            base.Close();
        }
    }
}