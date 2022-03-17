using System.Collections.Generic;
using Game.Enums;

namespace Game.ClassTypes.Player
{
    public interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifiers(PlayerStats stat);
        IEnumerable<float> GetPercentageModifiers(PlayerStats stat);
    }
}