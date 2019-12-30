using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StratosphereGames
{
    [System.Serializable]
    public class CardCategoryDescription
    {
        public CardCategoryType CategoryType;

        public List<UIDataMappingElement> DataMappers;
    }
}
