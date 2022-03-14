using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Game.Control
{
    public class AudioMasterControl : MonoBehaviour 
    {
            [SerializeField] AudioMixer mixer;
            [SerializeField] static float volumeLevel = 1.0f;
            [SerializeField] Slider sliderVolumeCtrl;

            private void Awake ()
            {
                SetLevel (volumeLevel);
                if (sliderVolumeCtrl != null)
                {
                    sliderVolumeCtrl.value = volumeLevel;
                }
            }

            public void SetLevel (float sliderValue)
            {
                mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
                volumeLevel = sliderValue;
            }
    }
}
