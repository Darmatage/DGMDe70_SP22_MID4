using System.Collections;
using System.Collections.Generic;
using Game.Curses;
using UnityEngine;

namespace Game.UI.Curses
{
    public interface ICurseEffectHolder
    {
        SO_EffectStrategy GetItem();
    }
}