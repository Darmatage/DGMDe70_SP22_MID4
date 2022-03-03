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
    }
}