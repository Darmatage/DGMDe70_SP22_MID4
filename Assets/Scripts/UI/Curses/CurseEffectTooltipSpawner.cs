using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core.UI.Tooltips;

namespace Game.UI.Curses
{
    [RequireComponent(typeof(ICurseEffectHolder))]
    public class CurseEffectTooltipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            var effectStrategy = GetComponent<ICurseEffectHolder>().GetItem();
            if (!effectStrategy) return false;

            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            var effectTooltip = tooltip.GetComponent<CurseEffectTooltip>();
            if (!effectTooltip) return;

            var effectStrategy = GetComponent<ICurseEffectHolder>().GetItem();

            effectTooltip.Setup(effectStrategy);
        }
    }
}