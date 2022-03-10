using System;
using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Enums;
using Game.PlayerClass;
using TMPro;
using UnityEngine;

namespace Game.UI.Stats
{
    public class StatsDisplayUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI healthDisplay;
        [SerializeField] TextMeshProUGUI damageDisplay;
        [SerializeField] TextMeshProUGUI defenceDisplay;

        PlayerHealth playerHealth;
        PlayerBaseStats playerBaseStats;

        private void Awake()
        {
            playerHealth = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerHealth>();
            playerBaseStats = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerBaseStats>();
        }
        void Start()
        {
            healthDisplay.text = playerHealth.GetHealthPoints().ToString();
        }

        private void Update()
        {
            healthDisplay.text = String.Format("{0:0.0}/{1:0}", playerHealth.GetHealthPoints(), playerHealth.GetMaxHealthPoints());
            damageDisplay.text = String.Format("{0}", playerBaseStats.GetStat(PlayerStats.BaseDamage));
            defenceDisplay.text = String.Format("{0}", playerBaseStats.GetStat(PlayerStats.BaseDefence)); //Defence is calculate as: TotalDamage /= 1 + PlayerDefence / EnemyDamage;
        }

    }
}
