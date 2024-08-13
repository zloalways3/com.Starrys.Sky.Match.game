using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class GameWindow : BaseWindow
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _rulesButton;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _target0;
        [SerializeField] private TMP_Text _target1;
        [SerializeField] private TMP_Text _target2;
        [SerializeField] private Image _target0Image;
        [SerializeField] private Image _target1Image;
        [SerializeField] private Image _target2Image;

        private adfshazasdf _adfshazasdf;
        private dfshjsdfzhjds _dfshjsdfzhjds;
        private LevelScoreConstraints _constraints;
        private dgstjusdfgxjs _level;
        private bool _isPaused;

        public bool IsPaused
        {
            get => _isPaused;
            private set
            {
                adsfhsa.Get<adsfhdjsfgmasdfh>().SetPauseState(isPaused: value);
                _isPaused = value;
            }
        }

        public int LevelText
        {
            set
            {
                if (_levelText != null) _levelText.text = $"level {value}";
            }
        }

        public float Time
        {
            set
            {
                int minutes = (int)value / 60;
                int seconds = Mathf.Clamp((int)value % 60, 0, 59);

                if (seconds < 10)
                    _timeText.text = $"{minutes}:0{seconds}";
                else
                    _timeText.text = $"{minutes}:{seconds}";
            }
        }

        public int Score
        {
            set
            {
                //int neededScore = _constraints.Map.First(pair => pair.LevelId.Equals(_level.CurrentLevelIndex)).Score;
                _scoreText.text = $"{value}";
            }
        }

        public int Target0
        {
            set => _target0.text = $"{Math.Max(0,value)}";
        }

        public int Target1
        {
            set => _target1.text = $"{Math.Max(0,value)}";
        }

        public int Target2
        {
            set => _target2.text = $"{Math.Max(0,value)}";
        }

        public void Unpause()
        {
            IsPaused = false;
            adsfhsa.Get<dsgfjs>().SetFieldVisibility(true);
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            _settingsButton?.onClick.AddListener(OpenSettings);
            _pauseButton?.onClick.AddListener(Pause);
            _rulesButton?.onClick.AddListener(OpenRules);
            _adfshazasdf = adsfhsa.Get<adfshazasdf>();
            _dfshjsdfzhjds = adsfhsa.Get<dfshjsdfzhjds>();
            _adfshazasdf.OnValueChanged += SetAdfshazasdf;
            _constraints = adsfhsa.Get<LevelScoreConstraints>();
            _level = adsfhsa.Get<dgstjusdfgxjs>();
            Score = 0;

            _level.OnLoad += SetTargets;
            SetTargets();
        }

        private void SetTargets()
        {
            var atlas = adsfhsa.Get<CellAtlas>();

            Target0 = _constraints.Map[_level.dsfgjhdhjsd].Score[0].Score;
            Target1 = _constraints.Map[_level.dsfgjhdhjsd].Score[1].Score;
            Target2 = _constraints.Map[_level.dsfgjhdhjsd].Score[2].Score;

            _target0Image.sprite = atlas.Atlas
                .First(cell => cell.TypeId == _constraints.Map[_level.dsfgjhdhjsd].Score[0].Key).Sprite;
            _target1Image.sprite = atlas.Atlas
                .First(cell => cell.TypeId == _constraints.Map[_level.dsfgjhdhjsd].Score[1].Key).Sprite;
            _target2Image.sprite = atlas.Atlas
                .First(cell => cell.TypeId == _constraints.Map[_level.dsfgjhdhjsd].Score[2].Key).Sprite;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            Time = 90 - _dfshjsdfzhjds.Current;
        }

        protected override void Close()
        {
            adsfhsa.Get<wer42>().Open<LevelMenuWindow>();
            adsfhsa.Get<dsgfjs>().SetFieldVisibility(false);

            gameObject.SetActive(false);
        }

        private void Pause()
        {
            IsPaused = true;

            adsfhsa.Get<wer42>().Open<PauseWindow>();
            adsfhsa.Get<dsgfjs>().SetFieldVisibility(false);

            gameObject.SetActive(false);
        }

        private void OpenSettings()
        {
            IsPaused = true;

            adsfhsa.Get<wer42>().Open<SettingsWindow>();
            adsfhsa.Get<wer42>().Get<SettingsWindow>().SetPrev(this);
            adsfhsa.Get<dsgfjs>().SetFieldVisibility(false);

            gameObject.SetActive(false);
        }

        private void OpenRules()
        {
            IsPaused = true;

            var textWindow = adsfhsa.Get<wer42>().Open<LongTextWindow>();
            textWindow.Label = adsfhsa.Get<TextAtlas>().Map
                .First(text => text.TypeId == TextAtlas.TextType.Rules).Label;
            textWindow.Text = adsfhsa.Get<TextAtlas>().Map
                .First(text => text.TypeId == TextAtlas.TextType.Rules).Text;
            textWindow.SetPrevWindow(this);

            adsfhsa.Get<dsgfjs>().SetFieldVisibility(false);
            gameObject.SetActive(false);
        }

        private void SetAdfshazasdf(int val)
        {
            Score = val;
            var curScore = adsfhsa.Get<adfshazasdf>();

            (TargetScore t0, TargetScore t1, TargetScore t2) targets = (
                _constraints.Map[_level.dsfgjhdhjsd].Score[0],
                _constraints.Map[_level.dsfgjhdhjsd].Score[1],
                _constraints.Map[_level.dsfgjhdhjsd].Score[2]);

            (int t0, int t1, int t2) currentScores = (
                curScore.Target[0].Score,
                curScore.Target[1].Score,
                curScore.Target[2].Score);

            Target0 = targets.t0.Score - currentScores.t0;
            Target1 = targets.t1.Score - currentScores.t1;
            Target2 = targets.t2.Score - currentScores.t2;
        }
    }
}