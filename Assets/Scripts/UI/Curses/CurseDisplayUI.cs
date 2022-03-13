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
        [SerializeField] Transform curseEffectsAdvantagesListArea;
        [SerializeField] Transform curseEffectsDisadvantagesListArea;
        [SerializeField] GameObject effectListItemPrefab;

        private string[] curseMonsterEffectsArray = null;
        private string[] curseHumanEffectsArray = null;
        
        PlayerCurses playerCurses;

        private void Awake()
        {
            playerCurses = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerCurses>();
        }
        void Start()
        {
            curseMonsterEffectsArray = playerCurses.GetCurseMonsterEffectNames();
            curseHumanEffectsArray = playerCurses.GetCurseHumanEffectNames();

            SetupCurseDetails();
        }

        private void SetupCurseDetails()
        {
            curseNameDisplay.text = playerCurses.GetCurseName();
            curseDescriptionDisplay.text = playerCurses.GetCurseDescription();
            ListCurseEffect(curseMonsterEffectsArray, curseEffectsAdvantagesListArea);
            ListCurseEffect(curseHumanEffectsArray, curseEffectsDisadvantagesListArea);
        }

        private void ListCurseEffect(string[] curseEffects, Transform curseEffectListArea)
        {
            foreach (var effect in curseEffects)
            {
                GameObject listItemInstance = Instantiate(effectListItemPrefab, curseEffectListArea);
                TMP_Text listItemText = listItemInstance.GetComponentInChildren<TMP_Text>();
                listItemText.text = effect.ToString();
                Debug.Log(effect);
            }
        }
    }
}
