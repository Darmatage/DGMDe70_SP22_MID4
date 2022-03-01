using System.Collections.Generic;
using Game.Enums;

namespace Game.PlayerClass
{
    public interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifiers(PlayerStats stat);
        IEnumerable<float> GetPercentageModifiers(PlayerStats stat);
    }
}