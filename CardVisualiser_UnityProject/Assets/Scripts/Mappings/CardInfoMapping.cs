﻿using StratosphereGames.Base;
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

    public enum CardDataType
    {
        Costs = 0,
        Picture = 1,
        Level = 2,
        Rarity = 3,
        FactoryCosts = 4,
        ClanCosts = 5,
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

        public List<CardUIDataMapping> CardUIDataList;
    }

    [CreateAssetMenu(fileName = "CardInfoMapping", menuName = "AssetMappings/CardInfoMapping")]
    public class CardInfoMapping : MappingAsset<CardInfoMapping, CardId, CardInfoMappingElement>
    {
        public List<CardUIDataMapping> CardInfoDataMappingForType(CardId id)
        {
            var element = GetElementForType(id);
            if(element != null)
            {
                return element.CardUIDataList;
            }
            return null;
        }
    }

    [Serializable]
    public class CardUIDataMapping : LinkDataToUI
    {
        public CardDataType CardUIDataType;

    }
}
