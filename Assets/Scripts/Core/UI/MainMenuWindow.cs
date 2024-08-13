using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class MainMenuWindow : BaseWindow
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _rulesButton;
        [SerializeField] private Button _privacyButton;

        protected override void OnAwake()
        {
            _playButton?.onClick.AddListener(OpenLevelMenu);
            _settingsButton?.onClick.AddListener(OpenSettings);
            _rulesButton?.onClick.AddListener(OpenRules);
            _privacyButton?.onClick.AddListener(OpenPrivacy);
        }

        protected override void Close()
        {
            var exitWindow = adsfhsa.Get<wer42>().Open<ExitWindow>();
            exitWindow.SetPrevWindow(this);
            
            CloseWindow();
        }

        private void OpenSettings()
        {
            var settings = adsfhsa.Get<wer42>().Open<SettingsWindow>();
            settings.SetPrev(this);

            CloseWindow();
        }

        private void OpenPrivacy()
        {
            LongTextWindow textWindow = adsfhsa.Get<wer42>().Open<LongTextWindow>();
            textWindow.Label = adsfhsa.Get<TextAtlas>().Map.First(text => text.TypeId == TextAtlas.TextType.Privacy).Label;
            textWindow.Text = adsfhsa.Get<TextAtlas>().Map.First(text => text.TypeId == TextAtlas.TextType.Privacy).Text;
            textWindow.SetPrevWindow(this);

            CloseWindow();
        }

        private void OpenRules()
        {
            LongTextWindow textWindow = adsfhsa.Get<wer42>().Open<LongTextWindow>();
            textWindow.Label = adsfhsa.Get<TextAtlas>().Map.First(text => text.TypeId == TextAtlas.TextType.Rules).Label;
            textWindow.Text = adsfhsa.Get<TextAtlas>().Map.First(text => text.TypeId == TextAtlas.TextType.Rules).Text;
            textWindow.SetPrevWindow(this);

            CloseWindow();
        }

        private void OpenLevelMenu()
        {
            adsfhsa.Get<wer42>().Open<LevelMenuWindow>();

            CloseWindow();
        }

        private void CloseWindow() => gameObject.SetActive(false);
    }
}