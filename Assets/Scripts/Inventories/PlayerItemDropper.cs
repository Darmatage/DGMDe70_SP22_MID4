using UnityEngine;

namespace Game.Inventories
{
    public class PlayerItemDropper : ItemDropper
    {
        [Tooltip("How far can an item be dropped from the player?")]
        [SerializeField] float scatterDistance = 4f;

        protected override Vector3 GetDropLocation()
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * scatterDistance;
            randomPoint.z = 0f;
            return randomPoint;
        }
    }
}