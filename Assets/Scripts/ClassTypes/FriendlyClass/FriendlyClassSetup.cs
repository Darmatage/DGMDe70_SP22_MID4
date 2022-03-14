using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Enums;
using UnityEngine;

namespace Game.ClassTypes.Friendly
{
    public class FriendlyClassSetup : MonoBehaviour, IClassSetup
    {
        [Range(1,5)]
        [SerializeField] int difficultyLevel = 1;
        [SerializeField] FriendlyType friendlyType;
        [SerializeField] float movementSpeed = 1f;
        [SerializeField] SO_FriendlyClassStats friendlyClassStats = null;

        public float GetStat(AIBaseStat stat)
        {
            return (GetBaseStat(stat));
        }

        public int GetDifficultyLevel()
        {
            return difficultyLevel;
        }
        public float GetMovementSpeed()
        {
            return movementSpeed;
        }
        private float GetBaseStat(AIBaseStat stat)
        {
            return friendlyClassStats.GetStat(friendlyType, stat, difficultyLevel);
        }
    }
    
}
