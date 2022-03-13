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
        [SerializeField] GameObject curseEffectsAdvantagesListArea;
        [SerializeField] GameObject curseEffectsDisadvantagesListArea;

        private string[] curseHumanEffectsArray = null;
        private string[] curseMonsterEffectsArray = null;
        PlayerCurses playerCurses;

        private void Awake()
        {
            playerCurses = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerCurses>();
        }
        void Start()
        {
            curseMonsterEffectsArray = playerCurses.GetCurseMonsterEffectNames();
            curseHumanEffectsArray = playerCurses.GetCurseHumanEffectNames();

            foreach (var effect in curseMonsterEffectsArray)
            {
                Debug.Log(effect);
            }
            foreach (var effect in curseHumanEffectsArray)
            {
                Debug.Log(effect);
            }

            curseNameDisplay.text = playerCurses.GetCurseName();
            curseDescriptionDisplay.text = playerCurses.GetCurseDescription();

        }

    }
}
