using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Control;
using Game.Enums;

namespace Game.SceneManagement
{
    public class Portal : MonoBehaviour
    {
    // enum DestinationIdentifier
    // {
    //     A, B, C, D, E, F
    // }
    [SerializeField] private SceneName sceneNameGoto = SceneName.Scene_Main;
    [SerializeField] Transform spawnPoint;
    [SerializeField] DestinationIdentifier destination;
    [SerializeField] float fadeOutTime = 0.5f;
    [SerializeField] float fadeWaitTime = 0.5f;
    [SerializeField] float fadeInTime = 0.5f;
    private int sceneToLoad = -1;

    private void Awake() 
    {
        sceneToLoad = (int)sceneNameGoto;
    }
       private void OnTriggerEnter2D(Collider2D other) 
       {
           if(other.tag == "Player") {
               {
                   StartCoroutine(Transitions());
               }
           }    
       }

       private IEnumerator Transitions ()
       {
           if (sceneToLoad < 0)
           {
                Debug.LogError("Scene to load is not set");
                yield break;
           }
           
                DontDestroyOnLoad(gameObject);

                CanvasFader fader = FindObjectOfType<CanvasFader>();
                SavingWrapperControl wrapper = FindObjectOfType<SavingWrapperControl>();

                EventHandler.CallActiveGameUI(true);

                yield return fader.FadeOut(fadeOutTime);
                wrapper.Save();

                yield return SceneManager.LoadSceneAsync(sceneToLoad);

                wrapper.Load();

                yield return new WaitForEndOfFrame();

                Portal otherPortal = GetOtherPortal();
                UpdatePlayer(otherPortal);

                wrapper.Save();

                EventHandler.CallActiveGameUI(false);

                yield return new WaitForSeconds(fadeWaitTime);
                fader.FadeIn(fadeInTime);

                

                Destroy(gameObject);
       }
       private void UpdatePlayer(Portal otherPortal)
       {
           GameObject player = GameObject.FindWithTag("Player");
           player.transform.position = otherPortal.spawnPoint.position;
           player.transform.rotation = otherPortal.spawnPoint.rotation;
       }
       private Portal GetOtherPortal()
       {
           foreach (Portal portal in FindObjectsOfType<Portal>())
           {
               if (portal == this) continue;
               if (portal.destination != destination) continue;
               
               return portal;
           }
           return null;
       }
    }
}


