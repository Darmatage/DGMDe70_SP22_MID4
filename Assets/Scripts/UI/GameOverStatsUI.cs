using System;
using System.Collections;
using System.Collections.Generic;
using Game.Inventories;
using Game.ClassTypes.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameOverStatsUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI soulsCountDisplay;
        [SerializeField] Slider karmaSlider;
        private float karmaStartValue = 50f;
        
        PlayerBaseStats playerBaseStats;
        SoulGemManager playerSoulCount = null;

        private void Awake()
        {
            playerBaseStats = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerBaseStats>();
            playerSoulCount = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<SoulGemManager>();
        }
        void Start()
        {
            if (playerSoulCount != null)
            {
                playerSoulCount.onChange += RefreshUI;
            }
            RefreshUI();
            karmaSlider.maxValue = 100f;
        }

        private void RefreshUI()
        {
            soulsCountDisplay.text = String.Format("Souls: R{0} | G{1} | B{2}", playerSoulCount.GetRedSoulCount(), playerSoulCount.GetGreenSoulCount(), playerSoulCount.GetBlueSoulCount());
            karmaSlider.value = karmaStartValue + playerSoulCount.GetKarmaCount();
        }

    }
}
