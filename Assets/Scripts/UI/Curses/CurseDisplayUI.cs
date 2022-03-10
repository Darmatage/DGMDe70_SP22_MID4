using System;
using System.Collections;
using System.Collections.Generic;
using Game.Curses;
using Game.Enums;
using TMPro;
using UnityEngine;

namespace Game.UI.Curses
{
    public class CurseDisplayUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI curseNameDisplay;
        [SerializeField] TextMeshProUGUI curseDescriptionDisplay;
        [SerializeField] TextMeshProUGUI curseEffectsDisplay;

        private string[] curseEffectsArray = null;
        PlayerCurses playerCurses;

        private void Awake()
        {
            playerCurses = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerCurses>();
        }
        void Start()
        {
            curseEffectsArray = playerCurses.GetCurseEffectNames();

            foreach (var effect in curseEffectsArray)
            {
                Debug.Log(effect);
            }

            curseNameDisplay.text = playerCurses.GetCurseName();
            curseDescriptionDisplay.text = playerCurses.GetCurseDescription();

        }

    }
}
