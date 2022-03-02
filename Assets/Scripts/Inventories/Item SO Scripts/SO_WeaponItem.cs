using UnityEngine;
using System.Collections.Generic;
using Game.PlayerClass;
using Game.Enums;
using Game.Combat;

namespace Game.Inventories
{
    /// <summary>
    /// A ScriptableObject that is used for weapons.
    /// </summary>

    [CreateAssetMenu(fileName = "Weapon", menuName = "Game/Inventory/New Weapon Item")]
    public class SO_WeaponItem : SO_EquipableItem, IModifierProvider
    {
        [Tooltip("To change which animation is played based on the equiped weapon.")]
        [SerializeField] AnimatorOverrideController animatorOverride = null; 

        [Tooltip("The weapons base damage.")]
        [SerializeField] float weaponBaseDamage = 5f;

        [Header("Range Weapon Attributes")]
        [Tooltip("Adding a Projectile will make this a ranged attack.")]
        [SerializeField] PlayerProjectile projectile = null;

        [Header("Weapon Stat Modifiers")]
        [Tooltip("Modify base stats.")]
        [SerializeField] Modifier[] additiveModifiers;
        [Tooltip("Modify base stats by percentage.")]
        [SerializeField] Modifier[] percentageModifiers;
        const string weaponName = "Weapon";

        public void Spawn(Animator animator)
        {
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride; 
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform weaponPosition, Vector3 launchDirection, float calculatedDamage)
        {
            PlayerProjectile projectInstance = Instantiate(projectile, weaponPosition.position + launchDirection * 1.5f, Quaternion.identity, GameObject.FindGameObjectWithTag(Tags.PROJECTILES_TAG).transform);
            projectInstance.SetTarget(launchDirection, calculatedDamage);
        }
        public float GetDamage()
        {
            return weaponBaseDamage;
        }

        [System.Serializable]
        struct Modifier
        {
            public PlayerStats stat;
            public float value;
        }  

        public IEnumerable<float> GetAdditiveModifiers(PlayerStats stat)
        {
            if (stat == PlayerStats.BaseDamage)
            {
                yield return weaponBaseDamage;
            }
            else
            {
                foreach (var modifier in additiveModifiers)
                {
                    if (modifier.stat == stat)
                    {
                        yield return modifier.value;
                    }
                }
            }
        }

        public IEnumerable<float> GetPercentageModifiers(PlayerStats stat)
        {
            foreach (var modifier in percentageModifiers)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;
                }
            }
        }
    }
}