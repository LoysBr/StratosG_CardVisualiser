using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardElementPresenter : MonoBehaviour
{
    [SerializeField]
    private Image           PictureImageUI;

    [SerializeField]
    private GameObject      CostsUI;
    [SerializeField]
    private Text            CostsTextUI;

    [SerializeField]
    private GameObject      LevelUI;
    [SerializeField]
    private Text            LevelTextUI;
    
    [SerializeField]
    private Image           RarityImageUI;

    [SerializeField]
    private GameObject      FactoryCostsUI;
    [SerializeField]
    private Text            FactoryCostsTextUI;
   

    public void SetPicture(Sprite picture)
    {
        PictureImageUI.sprite = picture;
    }

    public void SetCosts(int costs)
    {
        CostsTextUI.text = costs.ToString();
    }

    public void SetLevel(int level)
    {
        LevelTextUI.text = string.Format("LVL {0}", level.ToString());
    }

    public void SetRarity(Color rarityColor)
    {
        RarityImageUI.color = rarityColor;
    }

    public void SetFactoryCosts(int partsCosts, int collectedParts)
    {
        FactoryCostsTextUI.text = string.Format("{0}/{1}", collectedParts.ToString(), partsCosts.ToString());
    }

    public void ShowCosts(bool visibility)
    {
        CostsUI.SetActive(visibility);
    }

    public void ShowRarity(bool visibility)
    {
        RarityImageUI.enabled = visibility;
    }

    public void ShowLevel(bool visibility)
    {
        LevelUI.SetActive(visibility);
    }

    public void ShowFactoryCosts(bool visibility)
    {
        FactoryCostsUI.SetActive(visibility);
    }
}
