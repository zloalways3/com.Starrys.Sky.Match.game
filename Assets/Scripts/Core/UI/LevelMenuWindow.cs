using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class LevelMenuWindow : BaseWindow
    {
        [SerializeField] private LayoutGroup _levelsLayoutGroup;
        [SerializeField] private Button _privacyButton;

        private readonly List<Button> _levelButtons = new List<Button>();
        private readonly List<TMP_Text> _levelIdTexts = new List<TMP_Text>();
        private readonly List<TMP_Text> _levelTexts = new List<TMP_Text>();

        private readonly Color _starColor = new Color(172f / 255, 37f / 255, 47f / 255, 1);

        public int OpenLevels => adsfhsa.Get<adsrfjadsda>().OpenedLevels.Count(val => val);

        protected override void OnAwake()
        {
            adsfhsa.Get<adsrfjadsda>().OnNewlevelOpened += UpdateLevels;
            _privacyButton?.onClick.AddListener(OpenPrivacy);

            for (int i = 0; i < _levelsLayoutGroup.transform.childCount; i++)
            {
                foreach (var button in _levelsLayoutGroup.transform.GetChild(i).GetComponentsInChildren<Button>())
                {
                    _levelButtons.Add(button);
                    int levelId = _levelButtons.Count - 1;
                    button.onClick.AddListener(() => OpenLevel(levelId));

                    var texts = button.GetComponentsInChildren<TMP_Text>();
                    _levelIdTexts.Add(texts[0]);
                    _levelTexts.Add(texts[1]);
                }
            }

            UpdateLevels();
        }

        protected override void Close()
        {
            adsfhsa.Get<wer42>().Open<MainMenuWindow>();
            gameObject.SetActive(false);
        }

        private void OpenPrivacy()
        {
            LongTextWindow textWindow = adsfhsa.Get<wer42>().Open<LongTextWindow>();
            textWindow.Label = adsfhsa.Get<TextAtlas>().Map
                .First(text => text.TypeId == TextAtlas.TextType.Privacy).Label;
            textWindow.Text = adsfhsa.Get<TextAtlas>().Map
                .First(text => text.TypeId == TextAtlas.TextType.Privacy).Text;
            textWindow.SetPrevWindow(this);

            gameObject.SetActive(false);
        }

        private void UpdateLevels()
        {
            for (int i = 0; i < _levelButtons.Count; i++)
            {
                _levelButtons[i].interactable = i < OpenLevels;
                if (i >= OpenLevels)
                {
                    _levelIdTexts[i].alpha = 0.5f;
                    _levelTexts[i].alpha = 0.5f;
                }
                else
                {
                    _levelIdTexts[i].alpha = 1;
                    _levelTexts[i].alpha = 1;
                }

                if (i < OpenLevels - 1)
                {
                    _levelTexts[i].color = _starColor;
                }
            }
        }

        private void OpenLevel(int level)
        {
            adsfhsa.Get<dgstjusdfgxjs>().LoadLevel(level);
            GameWindow gameWindow = adsfhsa.Get<wer42>().Open<GameWindow>();
            gameWindow.LevelText = level + 1;
            gameWindow.Unpause();

            gameObject.SetActive(false);
        }
    }
}