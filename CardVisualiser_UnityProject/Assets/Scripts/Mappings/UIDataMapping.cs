using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StratosphereGames.Base;

namespace StratosphereGames
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
        public string UIObjectName;
        public UIDataValues UIDataValues;

        private GameObject ParentObject;
        public void SetParentObject(GameObject obj) { ParentObject = obj; }

        public void SetAndShow()
        {
            if (ParentObject)
            {
                //Find Objects with name "UIObjectName"
                Text[] textComponents = ParentObject.GetComponentsInChildren<Text>();
                Image[] imageComponents = ParentObject.GetComponentsInChildren<Image>();
                GameObject obj = null;
                switch (Type)
                {                    
                    case UIDataValueType.IntegersText:
                        foreach(Text text in textComponents)
                        {
                            if(text.gameObject.name.CompareTo(UIObjectName) == 0)
                            {
                                obj = text.gameObject;
                                break;
                            }
                            if (text.gameObject.transform.parent.gameObject.name.CompareTo(UIObjectName) == 0)
                            {
                                obj = text.gameObject.transform.parent.gameObject;
                                break;
                            }
                        }
                        break;
                    case UIDataValueType.Sprite:                        
                    case UIDataValueType.ImageColor:
                        foreach (Image img in imageComponents)
                        {
                            if (img.gameObject.name.CompareTo(UIObjectName) == 0)
                            {
                                obj = img.gameObject;
                                break;
                            }
                            if (img.gameObject.transform.parent.gameObject.name.CompareTo(UIObjectName) == 0)
                            {
                                obj = img.gameObject.transform.parent.gameObject;
                                break;
                            }
                        }
                        break;
                    default:
                        break;
                }

                //now show and set
                if (obj)
                {
                    obj.SetActive(true);
                    switch (Type)
                    {
                        case UIDataValueType.None:
                            break;
                        case UIDataValueType.IntegersText:
                            Text txt = obj.GetComponent<Text>();
                            if(txt == null)
                            {
                                txt = obj.GetComponentInChildren<Text>();
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
                                Image image = obj.GetComponent<Image>();
                                if (image == null)
                                {
                                    image = obj.GetComponentInChildren<Image>();
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
                                Image image = obj.GetComponent<Image>();
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
                    Debug.LogErrorFormat("Found no GameObject with name= {0}.", UIObjectName);
                }
            }
            else
            {
                Debug.LogErrorFormat("No ParentObject set.");
            }
        }
    }
}