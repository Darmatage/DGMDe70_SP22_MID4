using Game.EnemyClass;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Inventories
{
    public class LootDropper : ItemDropper
    {
        [Tooltip("How far can the loot be scattered from the dropper.")]
        [SerializeField] float scatterDistance = 4f;
        [SerializeField] SO_LootDropLibrary lootDropLibrary;

        public void RandomDrop()
        {
            var baseStats = GetComponent<EnemyClassSetup>();

            var drops = lootDropLibrary.GetRandomDrops(baseStats.GetDifficultyLevel());
            foreach (var drop in drops)
            {
                DropItem(drop.item, drop.number);
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