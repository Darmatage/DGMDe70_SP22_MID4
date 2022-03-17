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
            [SerializeField] Slider sliderVolumeCtrl;
            private SavedFileSingleton saveFileVar;

            private void Awake ()
            {
                saveFileVar = GameObject.FindWithTag(Tags.SAVING_STATE_PERSISTS_TAG).GetComponent<SavedFileSingleton>();
                SetLevel (saveFileVar.GetVolumeLevel());
            }

            public void SetLevel (float sliderValue)
            {
                saveFileVar.SetVolumeLevel(sliderValue);
                mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
                sliderVolumeCtrl.value = sliderValue;
            }
    }
}
