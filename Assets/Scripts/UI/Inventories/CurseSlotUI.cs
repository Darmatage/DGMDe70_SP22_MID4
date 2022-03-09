using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Core.UI.Dragging;
using Game.Inventories;
using Game.Enums;
using TMPro;

namespace Game.UI.Inventories
{
    /// <summary>
    /// An slot for the players equipment.
    /// </summary>
    public class CurseSlotUI : MonoBehaviour
    {
        [SerializeField] Image cooldownOverlay = null;

        CooldownStore cooldownStore;
        EquipmentSlotUI equipmentSlotUI;
       
        private void Awake() 
        {
            var player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
            equipmentSlotUI = GetComponent<EquipmentSlotUI>();
            cooldownStore = player.GetComponent<CooldownStore>();
        }
        private void Update()
        {
            cooldownOverlay.fillAmount = cooldownStore.GetFractionRemaining(equipmentSlotUI.GetItem());
        }
    }
}