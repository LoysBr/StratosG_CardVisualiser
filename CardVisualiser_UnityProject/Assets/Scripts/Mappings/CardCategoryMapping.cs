﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using StratosphereGames.Base;

namespace StratosphereGames
{
    public enum CardCategoryType
    {
        None = 0,
        Shop = 1,
        Deck = 2,
        Factory = 3,
    }

    [Serializable]
    public class CardCategoryMappingElement : IHasEnumType<CardCategoryType>
    {
        [SerializeField]
        private CardCategoryType _Type;
        public CardCategoryType Type
        {
            get
            {
                return _Type;
            }
        }

        public List<CardDataType> DisplayedCardData;
    }

    [CreateAssetMenu(fileName = "CardCategoryMapping", menuName = "AssetMappings/CardCategoryMapping")]
    public class CardCategoryMapping : MappingAsset<CardCategoryMapping, CardCategoryType, CardCategoryMappingElement>
    {
        public List<CardDataType> DisplayedCardDataForCategoryType(CardCategoryType type)
        {
            var element = GetElementForType(type);
            if (element != null)
            {
                return element.DisplayedCardData;
            }
            return null;
        }
    }
}