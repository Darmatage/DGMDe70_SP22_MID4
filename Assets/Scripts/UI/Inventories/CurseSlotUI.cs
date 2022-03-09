using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Inventories;
using Game.Curses;

namespace Game.UI.Inventories
{
    public class CurseSlotUI : MonoBehaviour
    {
        [SerializeField] Image cooldownOverlay = null;

        CooldownStore cooldownStore;
        PlayerCurses playerCurses;
       
        private void Awake() 
        {
            var player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
            cooldownStore = player.GetComponent<CooldownStore>();
            playerCurses = player.GetComponent<PlayerCurses>();
        }
        private void Update()
        {
            cooldownOverlay.fillAmount = cooldownStore.GetFractionRemaining(playerCurses.GetCurse());
        }
    }
}