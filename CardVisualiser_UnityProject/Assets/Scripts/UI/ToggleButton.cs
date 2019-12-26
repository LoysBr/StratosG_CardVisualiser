using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StratosphereGames
{
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField]
        private Image ImgButton;
        [SerializeField]
        private SpriteType SpriteOn;
        [SerializeField]
        private SpriteType SpriteOff;
        [SerializeField]
        private bool StartState;

        private bool CurActiveState;
        private UISpriteMapping SpriteMapping;

        private void Awake()
        {
            SpriteMapping = UISpriteMapping.GetMapping<UISpriteMapping>();
            CurActiveState = StartState;
            UpdateVisForCurrentState();
        }

        private void UpdateVisForCurrentState()
        {
            Sprite visSprite = null;
            switch (CurActiveState)
            {
                case true:
                    visSprite = SpriteMapping.GetSpriteForType(SpriteType.ToggleButtonOn);
                    break;
                case false:
                    visSprite = SpriteMapping.GetSpriteForType(SpriteType.ToggleButtonOff);
                    break;
            }
            ImgButton.sprite = visSprite;
        }

        public void SetActivation(bool newActiveState)
        {
            CurActiveState = newActiveState;
            UpdateVisForCurrentState();
        }
    }

}