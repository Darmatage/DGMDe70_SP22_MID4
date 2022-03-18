using UnityEngine;
using TMPro;
using Game.Inventories;
using Game.Enums;
using System;

namespace Game.UI.Inventories
{
    /// <summary>
    /// Root of the tooltip prefab to expose properties to other classes.
    /// </summary>
    public class ItemTooltip : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI titleText = null;
        [SerializeField] TextMeshProUGUI bodyText = null;
        [SerializeField] TextMeshProUGUI infoText = null;

        public void Setup(SO_InventoryItem item)
        {
            titleText.text = item.GetDisplayName();
            bodyText.text = item.GetDescription();

            if (item as SO_EquipableItem)
            {
                var equipmentItem = item as SO_EquipableItem;
                if (equipmentItem.GetEquipmentMaterial() == EquipmentMaterial.None)
                {
                    infoText.gameObject.SetActive(false);
                }
                else
                {
                    infoText.text = String.Format("Equipment - {0}", equipmentItem.GetEquipmentMaterial().ToString());
                }
            }
            else if (item as SO_ActionItem)
            {
                infoText.text = String.Format("Usable");
            }
            else
            {
                infoText.gameObject.SetActive(false);
            }
        }
    }
}