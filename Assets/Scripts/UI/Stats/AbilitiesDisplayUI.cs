using System;
using System.Collections;
using System.Collections.Generic;
using Game.ClassTypes.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Stats
{
    public class AbilitiesDisplayUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI classDisplay;
        [SerializeField] TextMeshProUGUI strengthDisplay;
        [SerializeField] TextMeshProUGUI constitutionDisplay;
        [SerializeField] TextMeshProUGUI intelligenceDisplay;
        
        PlayerBaseStats playerBaseStats;

        private void Awake()
        {
            playerBaseStats = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerBaseStats>();
        }
        private void Update()
        {
            classDisplay.text = String.Format("Class: {0}", playerBaseStats.GetCharacterClass());
            strengthDisplay.text = String.Format("Strength: 1");
            constitutionDisplay.text = String.Format("Constitution: 1");
            intelligenceDisplay.text = String.Format("Intelligence: 1");
        }


    }
}
