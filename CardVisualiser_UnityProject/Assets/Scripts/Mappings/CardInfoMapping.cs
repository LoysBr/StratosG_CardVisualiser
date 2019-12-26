using StratosphereGames.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StratosphereGames
{
    public enum CardId
    {
        None = 0,
        Troop = 1,
        Tank = 2,
        Jeep = 3,
    }

    [Serializable]
    public class CardInfo
    {
        public SpriteType Sprite;
        public int Level;
        public Color RarityColor;
        public int Costs;
        public int ConstructionCosts; // in scraps
        public int CollectedScraps;
    }

    [Serializable]
    public class CardInfoMappingElement : IHasEnumType<CardId>
    {
        [SerializeField]
        private CardId _Type;
        public CardId Type
        {
            get
            {
                return _Type;
            }
        }
        public CardInfo Info;
    }

    [CreateAssetMenu(fileName = "CardInfoMapping", menuName = "AssetMappings/CardInfoMapping")]
    public class CardInfoMapping : MappingAsset<CardInfoMapping, CardId, CardInfoMappingElement>
    {
        public CardInfo GetCardInfoForType(CardId id)
        {
            var element = GetElementForType(id);
            if(element != null)
            {
                return element.Info;
            }
            return null;
        }
    }
}
