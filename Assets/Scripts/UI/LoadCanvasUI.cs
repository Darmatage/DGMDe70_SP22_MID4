using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class LoadCanvasUI : MonoBehaviour
    {
        [SerializeField] Slider loadingSlider;
        [SerializeField] float buttonLoadWaitTime = 5f;

        private float sliderMaxValue = 0f;

        public event Action loadScreenUpdated;

        private void Awake() 
        {
            sliderMaxValue = buttonLoadWaitTime * 60f;
 
            loadingSlider.maxValue = sliderMaxValue;
        }

        private void OnEnable() 
        {
            loadingSlider.gameObject.SetActive(true);
            loadingSlider.value = 0f;
        }

        void FixedUpdate()
        {
            loadingSlider.value += 1;
            if (loadingSlider.value == sliderMaxValue)
            {
                loadingSlider.gameObject.SetActive(false);
                if (loadScreenUpdated != null)
                {
                    loadScreenUpdated();
                }
            }
        }
    }
}