using UnityEngine;
using System;

namespace Game.PlayerClass
{
    public class PlayerExperience : MonoBehaviour//, ISaveable
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

        // public object CaptureState()
        // {
        //     return experiencePoints;
        // }

        // public void RestoreState(object state)
        // {
        //     experiencePoints = (float)state;
        // }
    }
}