using UnityEngine;

namespace Game.Saving
{
    [System.Serializable]
    public class SerializableVector2
    {
        float x, y;

        public SerializableVector2(Vector2 vector)
        {
            x = vector.x;
            y = vector.y;
        }

        public Vector2 ToVector()
        {
            return new Vector2(x, y);
        }
    }
}