using Game.Control;
using Game.Curses;
using Game.ClassTypes.Enemy;
using Game.Enums;
using UnityEngine;
using UnityEngine.AI;
using Game.ClassTypes;

namespace Game.Inventories
{
    public class LootDropper : ItemDropper
    {
        [Tooltip("How far can the loot be scattered from the dropper.")]
        [SerializeField] float scatterDistance = 2f;
        [SerializeField] SO_LootDropLibrary lootDropLibrary;
        [SerializeField] SO_SoulItem redSoulGemItem = null;
        [SerializeField] SO_SoulItem blueSoulGemItem = null;

        PlayerCurses playerCurses;

        public void RandomDrop()
        {
            var aiClass = GetComponent<IClassSetup>();

            var drops = lootDropLibrary.GetRandomDrops(aiClass.GetDifficultyLevel());
            foreach (var drop in drops)
            {
                DropItem(drop.item, drop.number);
            }   
        }

        public void GemDrop(GameObject instigator)
        {
            if (redSoulGemItem == null || blueSoulGemItem == null) return;

            bool isMonster = instigator.GetComponent<PlayerTransformControl>().IsMonster;

            int soulGemDrop = 1;

            if (isMonster)
            {
                DropItem(redSoulGemItem, soulGemDrop + GetBonusSoulGems(instigator));
            }
            if (!isMonster)
            {
                DropItem(blueSoulGemItem, soulGemDrop);
            }

        }

        private int GetBonusSoulGems(GameObject instigator)
        {
            if (instigator.CompareTag(Tags.PLAYER_TAG))
            {
                playerCurses = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerCurses>();
                return playerCurses.GetCurse().GetCurseEffectModifier(CurseEffectTypes.SoulBonus);
            }
            return 0;
        }

        protected override Vector3 GetDropLocation()
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * scatterDistance;
            randomPoint.z = 0f;
            return randomPoint;
        }
    }
}