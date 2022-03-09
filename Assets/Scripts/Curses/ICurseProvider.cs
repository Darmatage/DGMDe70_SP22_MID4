using System.Collections.Generic;
using Game.Enums;

namespace Game.Curses
{
    public interface ICurseProvider
    {
        IEnumerable<int> GetCurseModifiers(CurseEffectTypes curseEffectType);
    }
}