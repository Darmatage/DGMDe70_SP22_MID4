using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Combat;

namespace Game.UI.HUD
{
    public class ManaDisplayUI : MonoBehaviour
    {
        [SerializeField] Image manaBarUIMask;
        private float originalSize;
        PlayerMana playerMana;

        private void Awake()
        {
            playerMana = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerMana>();
        }
        void Start()
        {
            originalSize = manaBarUIMask.rectTransform.rect.width;
        }

        private void Update()
        {
            SetValue(playerMana.GetPercentage()/100);
        }
        public void SetValue(float value)
        {				
            manaBarUIMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
        }
    }
}