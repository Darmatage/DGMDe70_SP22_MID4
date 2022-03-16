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
        [SerializeField] GameObject effectListItemPrefab;
        
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
            ListCurseEffect(PlayerTransformState.Monster, curseEffectsAdvantagesListArea);
            ListCurseEffect(PlayerTransformState.Human, curseEffectsDisadvantagesListArea);


            // if(playerCurses.DoesCurseHaveEffect(CurseEffectTypes.ArmorRestrictMaterial, PlayerTransformState.Human))
            // {
            //     var test = playerCurses.GetCurseEffectStrategy(CurseEffectTypes.ArmorRestrictMaterial, PlayerTransformState.Human) as SO_ArmorRestrictEffect;
            //     foreach (var item in test.GetRestictedArmorMaterial())
            //     {
            //         Debug.Log(item.ToString());
            //     }
            // }
        }

        private void ListCurseEffect(PlayerTransformState transformState, Transform curseEffectListArea)
        {
            foreach (var effect in playerCurses.GetCurseEffectNames(transformState))
            {
                GameObject listItemInstance = Instantiate(effectListItemPrefab, curseEffectListArea);
                TMP_Text listItemText = listItemInstance.GetComponentInChildren<TMP_Text>();
                listItemText.text = effect.ToString();
                Debug.Log(effect);
            }
        }
    }
}
