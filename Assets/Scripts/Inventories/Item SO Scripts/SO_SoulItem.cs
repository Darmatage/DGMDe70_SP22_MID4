using System;
using Game.Enums;
using UnityEngine;

namespace Game.Inventories
{
    [CreateAssetMenu(fileName = "Soul", menuName = "Game/Inventory/New Soul Item")]
    public class SO_SoulItem : SO_CollectableItem
    {
        [Tooltip("What type of soul is this?")]
        [SerializeField] SoulType soulType = SoulType.None;

        public SoulType GetSoulType()
        {
            return soulType;
        }

        public override bool IsDroppable()
        {
            return false;
        }

        public override float GetKarmaValue()
        {
            float karmaValue = 0f;

            switch (soulType)
            {
                case SoulType.Blue: 
                    karmaValue = 1;
                    break;

                case SoulType.Red: 
                    karmaValue = -1;
                    break;

                case SoulType.Green: 
                    karmaValue = 0;
                    break;

                default: 
                    karmaValue = 0;
                    break;
            }

            return karmaValue;
        }
    }
}