using System.Collections;
using Core.PlayerInput;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class EndGameWindow : BaseWindow
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _loseText;
        [SerializeField] private TMP_Text _buttonText;

        private dfjhadfsa.GameResult _result;
        private BaseWindow _prev;
        private bool _isLastLevel;

        public string Label
        {
            get => _label.text;
            set => _label.text = value;
        }

        public void SetResult(dfjhadfsa.GameResult res)
        {
            if (res == dfjhadfsa.GameResult.Win)
                SetWin();
            else
                SetLose();
        }

        public string Score
        {
            get => _scoreText.text;
            set => _scoreText.text = $"{value}";
        }

        public string ButtonText
        {
            get => _buttonText.text;
            set => _buttonText.text = value;
        }

        public void Init(dfjhadfsa.GameResult res, bool isLastLevel)
        {
            _result = res;
            _isLastLevel = isLastLevel;
            StartCoroutine(HideFieldRoutine());
        }

        public void SetPrev(BaseWindow prev)
        {
            _prev = prev;
        }

        protected override void OnAwake()
        {
            _nextLevelButton?.onClick.AddListener(OpenLevel);
        }
        
        protected override void OnEnableWindow()
        {
            StartCoroutine(HideFieldRoutine());
        }

        private IEnumerator HideFieldRoutine()
        {
            yield return null;
            adsfhsa.Get<dsgfjs>().SetFieldVisibility(false);
        }

        private void SetLose()
        {
            Label = "Lose";
            ButtonText = "Try again";
            if (_loseText != null) _loseText.gameObject.SetActive(true);
            _scoreText.gameObject.SetActive(false);
        }

        private void SetWin()
        {
            Label = "Win";
            ButtonText = "Next level";
            if (_loseText != null) _loseText.gameObject.SetActive(false);
            _scoreText.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            adsfhsa.Get<fgjssfg>().Disable();
            StartCoroutine(ClosePrefRoutine());
        }

        private void OnDisable()
        {
            adsfhsa.Get<fgjssfg>().Enable();
        }

        private IEnumerator ClosePrefRoutine()
        {
            yield return null;

            _prev.gameObject.SetActive(false);
        }

        private void OpenLevel()
        {
            var loader = adsfhsa.Get<dgstjusdfgxjs>();

            if (_result == dfjhadfsa.GameResult.Lose)
            {
                loader.LoadLevel(loader.dsfgjhdhjsd);
                _prev.gameObject.SetActive(true);
                Close();
                // ServiceLocator.Get<Score>().Reset();
                // ServiceLocator.Get<Timer>().Reset();
            }
            else
            {
                if (_isLastLevel)
                {
                    adsfhsa.Get<wer42>().Open<LevelMenuWindow>();
                    Close();
                    return;
                }

                loader.LoadLevel(loader.dsfgjhdhjsd + 1);
                if (_prev is GameWindow gameWindow)
                {
                    gameWindow.LevelText = loader.dsfgjhdhjsd + 1;
                }

                _prev.gameObject.SetActive(true);
                Close();
            }
        }
    }
}