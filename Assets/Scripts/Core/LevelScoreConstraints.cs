using System;
using System.Collections.Generic;
using Core.Api;
using UnityEngine;

namespace Core
{
    [Serializable]
    public struct TargetScore
    {
        public CellAtlas.CellType Key;
        public int Score;
    }

    [CreateAssetMenu(fileName = "LevelScoreConstraints", menuName = "Game/LevelConstaints")]
    public class LevelScoreConstraints : ScriptableObject, dfghsjsdf
    {

        [Serializable]
        public struct LevelScorePair
        {
            public int LevelId;
            public List<TargetScore> Score;
        }

        public List<LevelScorePair> Map;
    }
}