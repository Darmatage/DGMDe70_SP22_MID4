using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Combat;

namespace Game.UI.HUD
{
    public class HealthDisplayUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI healthDisplay;
        PlayerHealth playerHealth;

        private void Awake()
        {
            playerHealth = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerHealth>();
        }
        void Start()
        {
            healthDisplay.text = playerHealth.GetHealthPoints().ToString();
        }

        private void Update()
        {
            healthDisplay.text = playerHealth.GetHealthPoints().ToString();
        }
    }
}