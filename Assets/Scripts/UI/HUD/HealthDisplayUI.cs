using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Combat;

namespace Game.UI.HUD
{
    public class HealthDisplayUI : MonoBehaviour
    {
        [SerializeField] Image healthBarUIMask;
        private float originalSize;
        PlayerHealth playerHealth;

        private void Awake()
        {
            playerHealth = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerHealth>();
        }
        void Start()
        {
            originalSize = healthBarUIMask.rectTransform.rect.width;
        }

        private void Update()
        {
            SetValue(playerHealth.GetPercentage()/100);
        }
        public void SetValue(float value)
        {				
            healthBarUIMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
        }
    }
}