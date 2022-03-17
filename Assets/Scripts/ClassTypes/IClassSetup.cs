using Game.Enums;

namespace Game.ClassTypes
{
    public interface IClassSetup
    {
        public float GetStat(AIBaseStat stat);
        public int GetDifficultyLevel();
        public float GetMovementSpeed();
    }
}