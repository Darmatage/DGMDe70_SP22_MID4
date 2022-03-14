using UnityEngine;
using System;
using Game.Saving;

namespace Game.ClassTypes.Player
{
    public class PlayerExperience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;

        public event Action onExperienceGained;

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperienceGained();
        }

        public float GetPoints()
        {
            return experiencePoints;
        }

        object ISaveable.CaptureState()
        {
            return experiencePoints;
        }

        void ISaveable.RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }
}