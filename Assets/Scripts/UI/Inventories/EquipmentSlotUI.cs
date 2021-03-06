using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Core.UI.Dragging;
using Game.Inventories;
using Game.Enums;
using TMPro;
using Game.Curses;
using Game.Curses.Effects;

namespace Game.UI.Inventories
{
    /// <summary>
    /// An slot for the players equipment.
    /// </summary>
    public class EquipmentSlotUI : MonoBehaviour, IItemHolder, IDragContainer<SO_InventoryItem>
    {
        [SerializeField] InventoryItemIcon icon = null;
        [SerializeField] EquipLocation equipLocation = EquipLocation.None;
        [SerializeField] GameObject slotIconContainer = null;
        [SerializeField] GameObject slotTextContainer = null;
        [SerializeField] TextMeshProUGUI slotText = null;

        GameObject player;
        Equipment playerEquipment;
        List<EquipmentMaterial> restrictedMaterialList = new List<EquipmentMaterial>();
       
        private void Awake() 
        {
            player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
            playerEquipment = player.GetComponent<Equipment>();
            playerEquipment.equipmentUpdated += RedrawUI;
        }
        private void Start() 
        {
            slotText.text = equipLocation.ToString();
            if(slotIconContainer != null) slotTextContainer.SetActive(false);
            RedrawUI();
        }

        public int MaxAcceptable(SO_InventoryItem item)
        {
            SO_EquipableItem equipableItem = item as SO_EquipableItem;
            if (equipableItem == null) return 0;
            if (equipableItem.GetAllowedEquipLocation() != equipLocation) return 0;
            if (GetItem() != null) return 0;
            if (IsEquipmentMaterialRestricted(equipableItem)) return 0;

            return 1;
        }

        public void AddItems(SO_InventoryItem item, int number)
        {
            playerEquipment.AddItem(equipLocation, (SO_EquipableItem) item);
        }

        public SO_InventoryItem GetItem()
        {
            return playerEquipment.GetItemInSlot(equipLocation);
        }

        public bool IsDroppable()
        {
            return playerEquipment.IsItemDroppable(equipLocation);
        }

        public bool IsSwappable()
        {
            return playerEquipment.IsItemSwappable(equipLocation);
        }

        public int GetNumber()
        {
            if (GetItem() != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void RemoveItems(int number)
        {
            playerEquipment.RemoveItem(equipLocation);
        }

        private bool IsEquipmentMaterialRestricted(SO_EquipableItem item)
        {
            if (!player.GetComponent<PlayerCurses>().DoesCurseHaveEffect(CurseEffectTypes.EquipmentRestrictMaterial, PlayerTransformState.Either)) return false;
            if (restrictedMaterialList.Count == 0) CreateRestrictedMaterialList();
            
            foreach (EquipmentMaterial restrictedMaterialItem in restrictedMaterialList)
            {
                if (item.GetEquipmentMaterial() == restrictedMaterialItem) return true;
            }
            return false;
        }

        private void CreateRestrictedMaterialList()
        {
            var curseEffect = player.GetComponent<PlayerCurses>().GetCurseEffectStrategyByTransformState(CurseEffectTypes.EquipmentRestrictMaterial, PlayerTransformState.Either) as SO_EquipmentRestrictEffect;
            foreach (EquipmentMaterial item in curseEffect.GetRestictedArmorMaterial())
            {
                restrictedMaterialList.Add(item);
            }
        }

        void RedrawUI()
        {
            icon.SetItem(playerEquipment.GetItemInSlot(equipLocation));
            if(slotIconContainer == null)
            {
                if(playerEquipment.GetItemInSlot(equipLocation) == null) slotTextContainer.SetActive(true);
                if(playerEquipment.GetItemInSlot(equipLocation) != null) slotTextContainer.SetActive(false);
            }
            else
            {
                if(playerEquipment.GetItemInSlot(equipLocation) == null) slotIconContainer.SetActive(true);
                if(playerEquipment.GetItemInSlot(equipLocation) != null) slotIconContainer.SetActive(false);
            }

        }
    }
}