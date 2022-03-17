using System;
using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Enums;
using UnityEngine;

namespace Game.UI.Curser
{
    public class CurserControl : MonoBehaviour
    {
        [SerializeField] CursorMapping[] cursorMappings = null;
        private PlayerCombat playerCombat;
        private bool isGamePaused = false;

        private void Awake() 
        {
            playerCombat = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerCombat>();
        }

        private void OnEnable()
        {
            EventHandler.ActiveGameUI += SetIsActiveGameUI;
        }

        private void OnDisable()
        {
            EventHandler.ActiveGameUI -= SetIsActiveGameUI;
        }

        private void Update() 
        {
            if (SetInGameCurser()) return;
            if (SetUICurser()) return;
            SetCursor(CursorType.None);
        }

        private bool SetUICurser()
        {
            if (isGamePaused)
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        private bool SetInGameCurser()
        {
            if (playerCombat.IsWeaponRangeAttack() && !isGamePaused)
            {
                SetCursor(CursorType.Range);
                return true;
            }
            else if (!playerCombat.IsWeaponRangeAttack() && !isGamePaused)
            {
                SetCursor(CursorType.Melee);
                return true;
            }
            return false;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        private void SetIsActiveGameUI(bool setGamePaused)
        {
            isGamePaused = setGamePaused;
        }

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }


    }
}