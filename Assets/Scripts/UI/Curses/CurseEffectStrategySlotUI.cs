using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Game.Curses;
using TMPro;

namespace Game.UI.Curses
{
    public class CurseEffectStrategySlotUI : MonoBehaviour, ICurseEffectHolder
    {
        [SerializeField] TextMeshProUGUI titleText = null;

        SO_EffectStrategy effectStratgey;

        public void Setup(SO_EffectStrategy effectStratgey)
        {
            this.effectStratgey = effectStratgey;
            titleText.text = effectStratgey.GetCurseEffectName();
        }
        public SO_EffectStrategy GetItem()
        {
            return effectStratgey;
        }
    }
}