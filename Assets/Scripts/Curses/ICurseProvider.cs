using System.Collections.Generic;
using Game.Enums;

namespace Game.Curses
{
    public interface ICurseProvider
    {
        IEnumerable<float> GetCurseModifiers(CurseEffectTypes curseEffectType);
    }
}