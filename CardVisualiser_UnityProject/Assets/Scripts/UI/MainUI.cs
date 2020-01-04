using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StratosphereGames.Base;

namespace StratosphereGames
{
    [Serializable]
    public struct ToggleButtonMapping {
        public CardCategoryType Type;
        public ToggleButton Button;
    }

    public class MainUI : MonoBehaviour
    {
        //[SerializeField]
        //private List<CardCategoryDescription> CardCategories;
        [SerializeField]
        private CardCategoryType            StartCategory;
        [SerializeField]
        private List<ToggleButtonMapping>   ButtonMapping;
        [SerializeField]
        private RectTransform               CardRoot; // cards will be added here
        [SerializeField]
        private GameObject                  CardElementPrefab; // cards will be added here

        private CardInfoMapping             CardMapping;
        private CardCategoryMapping         CategoryMapping;
        private CardCategoryType            CurrentCategory;

        private void Awake()
        {
            CardMapping = CardInfoMapping.GetMapping<CardInfoMapping>();
            CategoryMapping = CardCategoryMapping.GetMapping<CardCategoryMapping>();
        }

        private void Start()
        {           
            PopulateOneCardOfEach();
            SetCategory(StartCategory);
        }

        public void SetCategory(CardCategoryType type)
        {
            CurrentCategory = type;
            UpdateForCurrentCategory();
        }

        private void UpdateForCurrentCategory()
        {
            UpdateButtons();
            UpdateContent();
        }

        private void UpdateButtons()
        {
            foreach (var element in ButtonMapping)
            {
                bool newSetState = element.Type == CurrentCategory;
                element.Button.SetActivation(newSetState);
            }
        }

        public void HandleSetCategory(int category)
        {
            CardCategoryType newType = (CardCategoryType)category;
            SetCategory(newType);
        }

        private void UpdateContent()
        {
            DisplayCardElementsForCategory(CurrentCategory);
        }

        private void PopulateOneCardOfEach()
        {
            if (CardMapping != null && CardMapping.Mapping != null)
            {
                foreach (CardInfoMappingElement cardMappingElt in CardMapping.Mapping)
                {
                    GameObject cardObj = Instantiate(CardElementPrefab, CardRoot);

                    if (cardMappingElt.CardUIDataList != null)
                    {
                        foreach (CardUIDataMapping UiData in cardMappingElt.CardUIDataList)
                        {
                            UiData.SetParentObject(cardObj);
                            UiData.FindObjectFromName();
                            UiData.SetValues();
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Need a Card Info Mapping instance with elements.");
            }
        }
                
        private void DisplayCardElementsForCategory(CardCategoryType category)
        {
            List<CardDataType> displayedCardDataTypes = CategoryMapping.DisplayedCardDataForCategoryType(category);

            foreach (CardInfoMappingElement cardMappingElt in CardMapping.Mapping)
            {                
                foreach (CardUIDataMapping UiData in cardMappingElt.CardUIDataList)
                {   
                    UiData.Show(displayedCardDataTypes.Contains(UiData.CardUIDataType));
                }                
            }
        }
    }

}