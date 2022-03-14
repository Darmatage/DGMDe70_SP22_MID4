using System.Collections;
using System.Collections.Generic;
using Game.ClassTypes.Enemy;
using Game.ClassTypes.Player;
using Game.Enums;
using Game.Inventories;
using Game.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Combat
{
    public interface IHealth
    {
        public bool IsDead();
        public void TakeDamage(GameObject instigator, float damage);
        public float GetHealthPoints();
        public float GetMaxHealthPoints();
        public float GetPercentage();
        public float GetFraction();
    }
    
}
