using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        private CardCategoryType            CurrentCategory;

        private List<GameObject>            InstantiatedCards;

        private void Awake()
        {
            CardMapping = CardInfoMapping.GetMapping<CardInfoMapping>();
            InstantiatedCards = new List<GameObject>();
        }

        private void Start()
        {
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
            DeleteAllInstantiatedCards();

            //for display testing purpose I add more cards
            for (int i = 6; i > 0; i--)
            {
                PopulateOneCardOfEach(CurrentCategory);
            }
        }

        private void PopulateOneCardOfEach(CardCategoryType category)
        {
            foreach(CardInfoMappingElement cardMappingElt in CardMapping.Mapping)
            {
                GameObject cardObj = Instantiate(CardElementPrefab, CardRoot);

                if (cardMappingElt.CardInfoDataMapping != null)
                {
                    foreach (UIDataMapping mapping in cardMappingElt.CardInfoDataMapping)
                    {
                        //TODO : IF category "dataName" == mappingElement.Name ?

                        mapping.SetParentObject(cardObj);
                        mapping.FindObjectFromName();
                        mapping.SetValues();
                    }
                }

                //DisplayCardElementsForCategory(category, presenter);
                InstantiatedCards.Add(cardObj);
            }
        }

        private void DeleteAllInstantiatedCards()
        {
            foreach (GameObject obj in InstantiatedCards)
                Destroy(obj);
        }

        private void DisplayCardElementsForCategory(CardCategoryType category, CardElementPresenter presenter)
        {
            switch (category)
            {
                case CardCategoryType.None:
                    break;
                case CardCategoryType.Shop:
                    presenter.ShowCosts(true);
                    presenter.ShowRarity(false);
                    presenter.ShowLevel(false);
                    presenter.ShowFactoryCosts(false);
                    break;
                case CardCategoryType.Deck:
                    presenter.ShowCosts(false);
                    presenter.ShowRarity(true);
                    presenter.ShowLevel(true);
                    presenter.ShowFactoryCosts(false);
                    break;
                case CardCategoryType.Factory:
                    presenter.ShowCosts(false);
                    presenter.ShowRarity(true);
                    presenter.ShowLevel(false);
                    presenter.ShowFactoryCosts(true);
                    break;
                default:
                    break;
            }
        }
    }

}