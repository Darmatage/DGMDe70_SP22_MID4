using UnityEngine;
using TMPro;
using Game.Inventories;

namespace Game.UI.Inventories
{
    /// <summary>
    /// Root of the tooltip prefab to expose properties to other classes.
    /// </summary>
    public class ItemTooltip : MonoBehaviour
    {
        // CONFIG DATA
        [SerializeField] TextMeshProUGUI titleText = null;
        [SerializeField] TextMeshProUGUI bodyText = null;

        // PUBLIC

        public void Setup(SO_InventoryItem item)
        {
            titleText.text = item.GetDisplayName();
            bodyText.text = item.GetDescription();
        }
    }
}