using System;
using System.Collections.Generic;
using Core.Api;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "TextAtlas", menuName = "Game/TextAtlas")]
    public class TextAtlas : ScriptableObject, dfghsjsdf
    {
        public enum TextType
        {
            Privacy,
            Rules,
            Greetings
        }

        [Serializable]
        public struct TypeTextPair
        {
            public TextType TypeId;
            public string Text;
            public string Label;
        }

        public List<TypeTextPair> Map;
    }
}