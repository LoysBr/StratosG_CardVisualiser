using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StratosphereGames
{
    public enum UIDataType
    {
        None = 0,
        IntegersText = 1,
        Sprite = 2,
        ImageColor = 3
    }

    [System.Serializable]
    public class UIDataElement
    {
        public string Format;
        public int IntegerValue1;
        public int IntegerValue2;
        public Color ColorValue;
        public SpriteType SpriteTypeValue;
    }

    [System.Serializable]
    public class UIDataMappingElement
    {
        public string Name;
        public UIDataType Type;
        public string UIObjectName;
        public UIDataElement UIData;

        public void SetAndShow()
        {
            GameObject obj = GameObject.Find(UIObjectName);

            if(obj)
            {
                obj.SetActive(true);
                switch (Type)
                {
                    case UIDataType.None:
                        break;
                    case UIDataType.IntegersText:
                        Text txt = obj.GetComponent<Text>();
                        if(txt)
                        {
                            if(UIData.Format.Length != 0)
                            {
                                txt.text = string.Format(UIData.Format, UIData.IntegerValue1.ToString(), UIData.IntegerValue2.ToString());
                            }
                            else
                            {
                                txt.text = UIData.IntegerValue1.ToString();
                            }
                        }
                        else
                        {
                            Debug.LogErrorFormat("No Text Component found on {0}.", UIObjectName);
                        }
                        break;
                    case UIDataType.Sprite:
                        {
                            Image image = obj.GetComponent<Image>();
                            if (image)
                            {
                                image.sprite = UISpriteMapping.GetMapping<UISpriteMapping>().GetSpriteForType(UIData.SpriteTypeValue);
                            }
                            else
                            {
                                Debug.LogErrorFormat("No Image Component found on {0}.", UIObjectName);
                            }
                        }
                        break;
                    case UIDataType.ImageColor:
                        {
                            Image image = obj.GetComponent<Image>();
                            if (image)
                            {
                                image.color = UIData.ColorValue;
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

    }
}