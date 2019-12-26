using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StratosphereGames.Base;
using System;

namespace StratosphereGames
{
    public enum SpriteType
    {
        None = 0,
        ToggleButtonOff = 100,
        ToggleButtonOn = 101,
        Unit_Troop = 200,
        Unit_Tank = 201,
        Unit_Jeep = 202,
    }

    [Serializable]
    public class UISpriteMappingElement : IHasEnumType<SpriteType>
    {
        [SerializeField]
        private SpriteType _Type;
        public SpriteType Type
        {
            get
            {
                return _Type;
            }
        }

        public Sprite Sprite;
    }

    [CreateAssetMenu(fileName = "UISpriteMapping", menuName = "AssetMappings/UISpriteMapping")]
    public class UISpriteMapping : MappingAsset<UISpriteMapping, SpriteType, UISpriteMappingElement>
    {

        public Sprite GetSpriteForType(SpriteType type)
        {
            var element = GetElementForType(type);
            if(element != null)
            {
                return element.Sprite;
            }
            return null;
        }
    }

}