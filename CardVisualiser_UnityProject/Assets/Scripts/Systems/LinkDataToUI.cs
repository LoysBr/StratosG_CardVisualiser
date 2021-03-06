﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StratosphereGames.Base
{
    public enum UIDataType
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

    /// <summary>
    /// Makes the link between UI Objects and UIDataValues
    /// N.B. : usage :
    /// At init you must execute in this order : SetParentObject,  
    /// FindObjectFromName then SetValues
    /// </summary>
    [System.Serializable]
    public class LinkDataToUI
    {       
        public UIDataType Type;
        public UIDataValues UIDataValues;
        
        //user precises the name of the UIObject 
        //it can be the Parent of Image/text
        public string UIObjectName;

        //to show/hide object
        protected GameObject UIObject;

        //to look for Child components only into this object
        protected GameObject ParentObject;
        public void SetParentObject(GameObject obj) { ParentObject = obj; }

        /// <summary>
        /// Do this at init to find the GameObject thanks to the specified name
        /// 
        /// First you need to "SetParentObject" to specify where to look for children objects
        /// Then we check if object name matches with one of the children
        /// We also filter children depending on their components :
        /// e.g. for a "IntegersText" we're looking only in objects with a Text component
        /// </summary>
        public void FindObjectFromName()
        {
            if (ParentObject)
            {     
                switch (Type)
                {
                    case UIDataType.IntegersText:
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
                    case UIDataType.Sprite:
                    case UIDataType.ImageColor:
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

        /// <summary>
        /// At init, after doing SetParentObject and FindObjectFromName, do this
        /// to finally set the values (from Data, to UI Objects)
        /// </summary>
        public void SetValues()
        {
            if (UIObject)
            {                  
                switch (Type)
                {
                    case UIDataType.None:
                        break;
                    case UIDataType.IntegersText:
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
                    case UIDataType.Sprite:
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
                    case UIDataType.ImageColor:
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