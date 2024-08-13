using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class PrivacyDialogWindow : BaseWindow
    {
        [SerializeField] private Button _privacyButton;

        protected override void OnAwake()
        {
            _privacyButton?.onClick.AddListener(OpenPrivacy);
        }

        protected override void Close()
        {
            adsfhsa.Get<wer42>().Open<MainMenuWindow>();

            base.Close();
        }

        private void OpenPrivacy()
        {
            LongTextWindow textWindow = adsfhsa.Get<wer42>().Open<LongTextWindow>();
            textWindow.Label = adsfhsa.Get<TextAtlas>().Map.First(text => text.TypeId == TextAtlas.TextType.Privacy).Label;
            textWindow.Text = adsfhsa.Get<TextAtlas>().Map.First(text => text.TypeId == TextAtlas.TextType.Privacy).Text;
            textWindow.SetPrevWindow(this);

            gameObject.SetActive(false);
        }
    }
}