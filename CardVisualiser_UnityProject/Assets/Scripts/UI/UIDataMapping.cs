using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StratosphereGames.Base
{
    public enum UIDataValueType
    {
        None = 0,
        IntegersText = 1,
        Sprite = 2,
        ImageColor = 3
    }

    [System.Serializable]
    public class UIDataValues
    {
        public string Format;
        public int IntegerValue1;
        public int IntegerValue2;
        public Color ColorValue;
        public SpriteType SpriteTypeValue;
    }

    //Make the link between UI Objects and UIDataValues
    [System.Serializable]
    public class UIDataMapping
    {       
        public string Name;
        public UIDataValueType Type;
        public UIDataValues UIDataValues;
        
        //user precises the name of the UIObject 
        //it can be the Parent of Image/text
        public string UIObjectName;

        //to show/hide object
        private GameObject UIObject;

        //to look for Child components only into this object
        private GameObject ParentObject;
        public void SetParentObject(GameObject obj) { ParentObject = obj; }
        
        public void FindObjectFromName()
        {
            if (ParentObject)
            {     
                switch (Type)
                {
                    case UIDataValueType.IntegersText:
                        Text[] textComponents = ParentObject.GetComponentsInChildren<Text>();

                        foreach (Text text in textComponents)
                        {
                            if (text.gameObject.name.CompareTo(UIObjectName) == 0)
                            {
                                UIObject = text.gameObject;
                                return;
                            }
                            if (text.gameObject.transform.parent.gameObject.name.CompareTo(UIObjectName) == 0)
                            {
                                UIObject = text.gameObject.transform.parent.gameObject;
                                return;
                            }
                        }

                        Debug.LogErrorFormat("Found no Text Component on UIObject or his children ({0}).", UIObjectName);
                        break;
                    case UIDataValueType.Sprite:
                    case UIDataValueType.ImageColor:
                        Image[] imageComponents = ParentObject.GetComponentsInChildren<Image>();

                        foreach (Image img in imageComponents)
                        {
                            if (img.gameObject.name.CompareTo(UIObjectName) == 0)
                            {
                                UIObject = img.gameObject;
                                return;
                            }
                            if (img.gameObject.transform.parent.gameObject.name.CompareTo(UIObjectName) == 0)
                            {
                                UIObject = img.gameObject.transform.parent.gameObject;
                                return;
                            }
                        }

                        Debug.LogErrorFormat("Found no Image Component on UIObject or his children ({0}).", UIObjectName);
                        break;
                    default:
                        break;
                }                
            }
            else
            {
                Debug.LogErrorFormat("No ParentObject set.");
            }
        }  
        
        public void Show(bool visibility)
        {
            if(UIObject)
            {
                UIObject.SetActive(visibility);
            }
            else
            {
                Debug.LogErrorFormat("UIObject not set.");
            }
        }

        public void SetValues()
        {
            if (UIObject)
            {                  
                switch (Type)
                {
                    case UIDataValueType.None:
                        break;
                    case UIDataValueType.IntegersText:
                        Text txt = UIObject.GetComponent<Text>();
                        if(txt == null)
                        {
                            txt = UIObject.GetComponentInChildren<Text>();
                        }

                        if (txt)
                        {
                            if (UIDataValues.Format.Length != 0)
                            {
                                txt.text = string.Format(UIDataValues.Format, UIDataValues.IntegerValue1.ToString(), UIDataValues.IntegerValue2.ToString());
                            }
                            else
                            {
                                txt.text = UIDataValues.IntegerValue1.ToString();
                            }
                        }
                        else
                        {
                            Debug.LogErrorFormat("No Text Component found on {0}.", UIObjectName);
                        }
                        break;
                    case UIDataValueType.Sprite:
                        {
                            Image image = UIObject.GetComponent<Image>();
                            if (image == null)
                            {
                                image = UIObject.GetComponentInChildren<Image>();
                            }
                            if (image)
                            {
                                image.sprite = UISpriteMapping.GetMapping<UISpriteMapping>().GetSpriteForType(UIDataValues.SpriteTypeValue);
                            }
                            else
                            {
                                Debug.LogErrorFormat("No Image Component found on {0}.", UIObjectName);
                            }
                        }
                        break;
                    case UIDataValueType.ImageColor:
                        {
                            Image image = UIObject.GetComponent<Image>();
                            if (image)
                            {
                                image.color = UIDataValues.ColorValue;
                            }
                            else
                            {
                                Debug.LogErrorFormat("No Image Component found on {0}.", UIObjectName);
                            }
                        }
                        break;
                }
            }
            else
            {
                Debug.LogErrorFormat("UIObject not set.");
            }
        }
    }
}