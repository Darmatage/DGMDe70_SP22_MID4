using System;
using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Enums;
using Game.Inventories;
using Game.PlayerClass;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Stats
{
    public class StatsDisplayUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI maxHealthDisplay;
        [SerializeField] TextMeshProUGUI damageDisplay;
        [SerializeField] TextMeshProUGUI defenceDisplay;
        [SerializeField] TextMeshProUGUI soulsCountDisplay;
        //[SerializeField] TextMeshProUGUI karmaValueDisplay;
        [SerializeField] Slider karmaSlider;
        private float karmaStartValue = 50f;

        PlayerHealth playerHealth;
        PlayerBaseStats playerBaseStats;
        SoulGemManager playerSoulCount = null;

        private void Awake()
        {
            playerHealth = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerHealth>();
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
            maxHealthDisplay.text = playerHealth.GetHealthPoints().ToString();
            karmaSlider.maxValue = 100f;
        }

        private void Update()
        {
            maxHealthDisplay.text = String.Format("Max Health: {0}", playerHealth.GetMaxHealthPoints());
            damageDisplay.text = String.Format("Damage: {0}", playerBaseStats.GetStat(PlayerStats.BaseDamage));
            defenceDisplay.text = String.Format("Defence: {0}", playerBaseStats.GetStat(PlayerStats.BaseDefence)); //Defence is calculate as: TotalDamage /= 1 + PlayerDefence / EnemyDamage;
        }

        private void RefreshUI()
        {
            soulsCountDisplay.text = String.Format("Souls: R{0} | G{1} | B{2}", playerSoulCount.GetRedSoulCount(), playerSoulCount.GetGreenSoulCount(), playerSoulCount.GetBlueSoulCount());
            //karmaValueDisplay.text = String.Format("Karma: {0}", playerSoulCount.GetKarmaCount());
            karmaSlider.value = karmaStartValue + playerSoulCount.GetKarmaCount();
        }

    }
}
