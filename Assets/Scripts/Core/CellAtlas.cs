using System;
using System.Collections.Generic;
using Core.Api;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "CellAtlas", menuName = "Game/CellAtlas")]
    public class CellAtlas : ScriptableObject, dfghsjsdf
    {
        public enum CellType
        {
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            None
        }

        [Serializable]
        public struct TypeCeilPair
        {
            public CellType TypeId;
            public Sprite Sprite;
        }

        public List<TypeCeilPair> Atlas;
    }
}