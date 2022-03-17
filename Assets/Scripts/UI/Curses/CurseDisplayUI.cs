using System;
using System.Collections;
using System.Collections.Generic;
using Game.Curses;
using Game.Curses.Effects;
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
        [SerializeField] CurseEffectStrategySlotUI effectStrategySlotPrefab;
        
        PlayerCurses playerCurses;

        private void Awake()
        {
            playerCurses = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerCurses>();
        }
        void Start()
        {
            SetupCurseDetails();
        }

        private void SetupCurseDetails()
        {
            curseNameDisplay.text = playerCurses.GetCurseName();
            curseDescriptionDisplay.text = playerCurses.GetCurseDescription();
            ListCurseEffect(CurseEffectConditionType.Advantage, curseEffectsAdvantagesListArea);
            ListCurseEffect(CurseEffectConditionType.Disadvantage, curseEffectsDisadvantagesListArea);
        }
        private void ListCurseEffect(CurseEffectConditionType conditionType, Transform curseEffectListArea)
        {
            foreach (Transform child in curseEffectListArea)
            {
                Destroy(child.gameObject);
            }

            foreach (var effect in playerCurses.GetCurseEffectNamesByConditionType(conditionType))
            {
                var listItemInstance = Instantiate(effectStrategySlotPrefab, curseEffectListArea);
                listItemInstance.Setup(effect.Value);
            }
        }
    }
}
