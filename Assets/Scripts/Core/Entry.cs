using System.Collections.Generic;
using Core.PlayerInput;
using Core.UI;
using UnityEngine;

namespace Core
{
    [DefaultExecutionOrder(-10000)]
    public class Entry : MonoBehaviour
    {
        [SerializeField] private List<BaseWindow> _ui;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _fieldParent;
        [SerializeField] private GameObject _fieldBg;
        [SerializeField] private Cell _cellReference;
        [SerializeField] private CellAtlas _cellAtlas;
        [SerializeField] private TextAtlas _textAtlas;
        [SerializeField] private LevelScoreConstraints _levelConstraints;
        [SerializeField] private AudioSource _sound;
        [SerializeField] private AudioSource _clickSound;

        private adsfhdjsfgmasdfh _adsfhdjsfgmasdfh;

        private void Awake()
        {
            _adsfhdjsfgmasdfh = GetComponent<adsfhdjsfgmasdfh>();

            InstallBindings();
        }

        private void Start()
        {
            //ServiceLocator.Get<InterfaceDispatcher>().Open<MainMenuWindow>();
            adsfhsa.Get<wer42>().Open<PrivacyDialogWindow>();
            adsfhsa.Get<dsgfjs>().SetFieldVisibility(false);
        }

        private void InstallBindings()
        {
            adsfhsa.Bind<CellAtlas>(_cellAtlas);
            adsfhsa.Bind<TextAtlas>(_textAtlas);
            adsfhsa.Bind<LevelScoreConstraints>(_levelConstraints);
            adsfhsa.Bind<adsfhdjsfgmasdfh>(_adsfhdjsfgmasdfh);
            adsfhsa.Bind<dsgfjs>(new dsgfjs(_fieldParent, _cellReference,_fieldBg));
            adsfhsa.Bind<fgjssfg>(new fgjssfg());
            adsfhsa.Bind<sdfhg>(new sdfhg());
            adsfhsa.Bind<adfshazasdf>(new adfshazasdf());
            adsfhsa.Bind<dfshjsdfzhjds>(new dfshjsdfzhjds());
            
            var levelLoader = new dgstjusdfgxjs();
            adsfhsa.Bind<dgstjusdfgxjs>(levelLoader);
            adsfhsa.Bind<sadfhdshyasa>(new sadfhdshyasa(levelLoader));
            levelLoader.dfghjsgjdfsafhsd(adsfhsa.Get<sadfhdshyasa>());
            
            adsfhsa.Bind<dfgjrssdertaes>(new dfgjrssdertaes(_cellReference));
            adsfhsa.Bind<wer42>(new wer42(_ui, _canvas));
            adsfhsa.Bind<dfjhadfsa>(new dfjhadfsa());
            adsfhsa.Bind<adsrfjadsda>(new adsrfjadsda());
            adsfhsa.Bind<adsfhjed>(new adsfhjed(_sound));
            adsfhsa.Bind<ClickAdsfhjed>(new ClickAdsfhjed(_clickSound));

            _adsfhdjsfgmasdfh.Bind(adsfhsa.Get<dfshjsdfzhjds>()).AsUpdateListener();
            _adsfhdjsfgmasdfh.Bind(adsfhsa.Get<fgjssfg>()).AsUpdateListener();
            _adsfhdjsfgmasdfh.Bind(adsfhsa.Get<sadfhdshyasa>()).AsUpdateListener();
        }
    }
}