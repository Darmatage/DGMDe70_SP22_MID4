using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Inventories;

namespace Game.UI.HUD
{
    public class SoulGemDisplayUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI karmasValueField;
        [SerializeField] TextMeshProUGUI soulsCountField;

        SoulGemManager playerSoulCount = null;

        private void Start() {
            playerSoulCount = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<SoulGemManager>();

            if (playerSoulCount != null)
            {
                playerSoulCount.onChange += RefreshUI;
            }

            RefreshUI();
        }

        private void RefreshUI()
        {
            karmasValueField.text = playerSoulCount.GetKarmaCount().ToString();
            soulsCountField.text = String.Format("R{0} | G{1} | B{2}", playerSoulCount.GetRedSoulCount(), playerSoulCount.GetGreenSoulCount(), playerSoulCount.GetBlueSoulCount());
        }

    }
}