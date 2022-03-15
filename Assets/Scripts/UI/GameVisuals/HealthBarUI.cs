using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using UnityEngine;

namespace Game.UI.GameVisuals
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas rootCanvas = null;
        private IHealth healthComponent = null;

        private void Awake() 
        {
            healthComponent = GetComponentInParent<IHealth>();
        }


        void Update()
        {
            if (Mathf.Approximately(healthComponent.GetFraction(), 0) ||  Mathf.Approximately(healthComponent.GetFraction(), 1))
            {
                rootCanvas.enabled = false;
                return;
            }

            rootCanvas.enabled = true;
            foreground.localScale = new Vector3(healthComponent.GetFraction(), 1, 1);
        }
    }
}