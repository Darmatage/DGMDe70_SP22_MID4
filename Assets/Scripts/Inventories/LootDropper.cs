using Game.Control;
using Game.EnemyClass;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Inventories
{
    public class LootDropper : ItemDropper
    {
        [Tooltip("How far can the loot be scattered from the dropper.")]
        [SerializeField] float scatterDistance = 2f;
        [SerializeField] SO_LootDropLibrary lootDropLibrary;
        [SerializeField] SO_SoulItem redSoulGemItem = null;
        [SerializeField] SO_SoulItem blueSoulGemItem = null;

        public void RandomDrop()
        {
            var enemyClass = GetComponent<EnemyClassSetup>();

            var drops = lootDropLibrary.GetRandomDrops(enemyClass.GetDifficultyLevel());
            foreach (var drop in drops)
            {
                DropItem(drop.item, drop.number);
            }   
        }

        public void GemDrop(GameObject instigator)
        {
            if(redSoulGemItem == null || blueSoulGemItem == null) return;

            bool isMonster = instigator.GetComponent<PlayerTransformControl>().IsMonster;
            int number = 1;

            if(isMonster)
            {
                DropItem(redSoulGemItem, number);
            }
            if(!isMonster)
            {
                DropItem(blueSoulGemItem, number);
            }

        }

        protected override Vector3 GetDropLocation()
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * scatterDistance;
            randomPoint.z = 0f;
            return randomPoint;
        }
    }
}