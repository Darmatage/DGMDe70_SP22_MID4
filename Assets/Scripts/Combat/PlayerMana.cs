using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.Inventories;
using Game.ClassTypes.Player;
using Game.Saving;
using Game.Utils;
using UnityEngine;

namespace Game.Combat
{
    public class PlayerMana : MonoBehaviour, ISaveable
    {
        LazyValue<float> mana;

        private void Awake() {
            mana = new LazyValue<float>(GetMaxMana);
        }

        private void Update() {
            if (mana.value < GetMaxMana())
            {
                mana.value += GetRegenRate() * Time.deltaTime;
                if (mana.value > GetMaxMana())
                {
                    mana.value = GetMaxMana();
                }
            }
        }

        public float GetMana()
        {
            return mana.value;
        }

        public float GetMaxMana()
        {
            return GetComponent<PlayerBaseStats>().GetStat(PlayerStats.Mana);
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return mana.value / GetMaxMana();
        }

        public float GetRegenRate()
        {
            return GetComponent<PlayerBaseStats>().GetStat(PlayerStats.ManaRegenRate);
        }

        public bool UseMana(float manaToUse)
        {
            if (manaToUse > mana.value)
            {
                return false;
            }
            mana.value -= manaToUse;
            return true;
        }

        object ISaveable.CaptureState()
        {
            return mana.value;
        }

        void ISaveable.RestoreState(object state)
        {
            mana.value = (float) state;
        }
    }
}