using UnityEngine;
using TMPro;
using Game.Curses;

namespace Game.UI.Curses
{
    /// <summary>
    /// Root of the tooltip prefab to expose properties to other classes.
    /// </summary>
    public class CurseEffectTooltip : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI titleText = null;
        [SerializeField] TextMeshProUGUI bodyText = null;

        public void Setup(SO_EffectStrategy effectStrategy)
        {
            titleText.text = effectStrategy.GetCurseEffectName();
            bodyText.text = effectStrategy.GetDescription();
        }
    }
}