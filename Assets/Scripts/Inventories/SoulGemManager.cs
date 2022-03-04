using System;
using Game.Saving;
using UnityEngine;

namespace Game.Inventories 
{
    public class SoulGemManager : MonoBehaviour, ISaveable, ISoulGemCounter
    {
        [SerializeField] float karmaCount = 0f;

        private float redSoulGemCount = 0f;
        private float greenSoulGemCount = 0f;
        private float blueSoulGemCount = 0f;

        public event Action onChange;

        public float GetKarmaCount()
        {
            return karmaCount;
        }
        public float GetRedSoulCount()
        {
            return redSoulGemCount;
        }
        public float GetGreenSoulCount()
        {
            return greenSoulGemCount;
        }
        public float GetBlueSoulCount()
        {
            return blueSoulGemCount;
        }

        public void UpdateKarma(float amount)
        {
            karmaCount += amount;
            if (onChange != null)
            {
                onChange();
            }
        }

        public void CountSoulItems(SO_InventoryItem item, int number)
        {
            if (item is SO_SoulItem)
            {
                UpdateKarma(item.GetKarmaValue() * number);
                CountSoulTypes(item.GetKarmaValue(), number);
            }
        }

        private void CountSoulTypes(float soulValue, int number)
        {
            switch (soulValue)
            {
                case -1f:
                    redSoulGemCount += 1 * number;
                    onChange();
                    break;

                case 0f:
                    greenSoulGemCount += 1 * number;
                    onChange();
                    break;

                case 1f:
                    blueSoulGemCount += 1 * number;
                    onChange();
                    break;
                
                default:
                    break;
            }

        }

        private struct SoulCountRecord
        {
            public float karmaRecord;
            public float redRecord;
            public float greenRecord;
            public float blueRecord;
        }

        object ISaveable.CaptureState()
        {
            var soulRecord = new SoulCountRecord();

            soulRecord.karmaRecord = karmaCount;
            soulRecord.redRecord = redSoulGemCount;
            soulRecord.greenRecord = greenSoulGemCount;
            soulRecord.blueRecord = blueSoulGemCount;

            return soulRecord;
        }

        void ISaveable.RestoreState(object state)
        {
            var soulRecord = (SoulCountRecord)state;

            karmaCount = soulRecord.karmaRecord;
            redSoulGemCount = soulRecord.redRecord;
            greenSoulGemCount = soulRecord.greenRecord;
            blueSoulGemCount = soulRecord.blueRecord;

            if (onChange != null)
            {
                onChange();
            }
        }
    }
}