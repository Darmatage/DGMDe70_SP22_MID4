using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.UI
{
    public class MenuSwitcherUI : MonoBehaviour
    {
        [SerializeField] GameObject uiInventroyTab = null;
        //[SerializeField] GameObject uiStatsTab = null;

        private void Start() 
        {
            SwitchTo(uiInventroyTab);
        }

        public void SwitchTo(GameObject toDisplay)
        {
            if(toDisplay.transform.parent != transform) return;

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(child.gameObject == toDisplay);
            }
        }

    }
}