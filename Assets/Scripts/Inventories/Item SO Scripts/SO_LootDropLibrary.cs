using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventories
{
    /// <summary>
    /// This ScriptableObject is used to create a list of potential items an ememy can drop on death.
    /// The list can include multiple items with their drop potential,
    /// as well as the option to change chances by difficultly level.
    /// </summary>

    [CreateAssetMenu(fileName = "LootDropTable", menuName = ("Game/NPCs/Loot Drop Table"))]
    public class SO_LootDropLibrary : ScriptableObject
    {
        [Tooltip("What items can be dropped?")]
        [SerializeField] DropConfig[] potentialDrops;

        [Header("Overall Drop Change & Number")]
        [Tooltip("What is the change of any item being dropped at each level?")]
        [SerializeField] float[] dropChancePercentage;
        [Tooltip("What is the min individual number of items being dropped per level?")]
        [SerializeField] int[] minDrops;
        [Tooltip("What is the max individual number of items being dropped per level?")]
        [SerializeField] int[] maxDrops;

        [System.Serializable]
        class DropConfig
        {
            [Tooltip("What item is to be dropped?")]
            public SO_InventoryItem item;
            [Tooltip("How likely should this item be dropped?")]
            public float[] relativeChance;
            
            [Header("Stackable Item Options")]
            [Tooltip("If stackable, how many? (Min & Max)")]
            public int[] minNumber;
            public int[] maxNumber;
            public int GetRandomNumber(int level)
            {
                if (!item.IsStackable())
                {
                    return 1;
                }
                int min = GetByLevel(minNumber, level);
                int max = GetByLevel(maxNumber, level);
                return UnityEngine.Random.Range(min, max + 1);
            }
        }

        public struct Dropped
        {
            public SO_InventoryItem item;
            public int number;
        }

        public IEnumerable<Dropped> GetRandomDrops(int level)
        {
            if (!ShouldRandomDrop(level))
            {
                yield break;
            }
            for (int i = 0; i < GetRandomNumberOfDrops(level); i++)
            {
                yield return GetRandomDrop(level);
            }
        }

        bool ShouldRandomDrop(int level)
        {
            return Random.Range(0, 100) < GetByLevel(dropChancePercentage, level);
        }

        int GetRandomNumberOfDrops(int level)
        {
            int min = GetByLevel(minDrops, level);
            int max = GetByLevel(maxDrops, level);
            return Random.Range(min, max);
        }

        Dropped GetRandomDrop(int level)
        {
            var drop = SelectRandomItem(level);
            var result = new Dropped();
            result.item = drop.item;
            result.number = drop.GetRandomNumber(level);
            return result;
        }

        DropConfig SelectRandomItem(int level)
        {
            float totalChance = GetTotalChance(level);
            float randomRoll = Random.Range(0, totalChance);
            float chanceTotal = 0;
            foreach (var drop in potentialDrops)
            {
                chanceTotal += GetByLevel(drop.relativeChance, level);
                if (chanceTotal > randomRoll)
                {
                    return drop;
                }
            }
            return null;
        }

        float GetTotalChance(int level)
        {
            float total = 0;
            foreach (var drop in potentialDrops)
            {
                total += GetByLevel(drop.relativeChance, level);
            }
            return total;
        }

        static T GetByLevel<T>(T[] values, int level)
        {
            if (values.Length == 0)
            {
                return default;
            }
            if (level > values.Length)
            {
                return values[values.Length - 1];
            }
            if (level <= 0)
            {
                return default;
            }
            return values[level - 1];
        }
    }
}
