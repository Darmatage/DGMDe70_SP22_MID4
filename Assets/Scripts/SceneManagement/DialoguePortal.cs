using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Control;
using Game.Enums;

namespace Game.SceneManagement
{
    public class DialoguePortal : MonoBehaviour
    {
    // enum DestinationIdentifier
    // {
    //     A, B, C, D, E, F
    // }
    [SerializeField] private SceneName sceneNameGoto = SceneName.Scene_Main;
    [SerializeField] Transform _dialogueSpawnPoint;
    public Transform DialogueSpawnPoint { get => _dialogueSpawnPoint; }
    [SerializeField] CutSceneDestinationIdentifier _dialogueDestination;
    public CutSceneDestinationIdentifier DialogueDestination { get => _dialogueDestination; }
    [SerializeField] float fadeOutTime = 0.5f;
    [SerializeField] float fadeWaitTime = 0.5f;
    [SerializeField] float fadeInTime = 0.5f;
    private int sceneToLoad = -1;

    private void Awake() 
    {
        sceneToLoad = (int)sceneNameGoto;
    }
       public void GoToCutScene() 
       {
            StartCoroutine(Transitions());   
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

                yield return fader.FadeOut(fadeOutTime);
                wrapper.Save();

                yield return SceneManager.LoadSceneAsync(sceneToLoad);

                wrapper.Load();

                yield return new WaitForEndOfFrame();

                NPCPortal otherPortal = GetNPCPortal();
                UpdatePlayer(otherPortal);

                wrapper.Save();

                EventHandler.CallActiveGameUI(false);

                yield return new WaitForSeconds(fadeWaitTime);
                fader.FadeIn(fadeInTime);

                Destroy(gameObject);
       }
       private void UpdatePlayer(NPCPortal otherPortal)
       {
           GameObject player = GameObject.FindWithTag("Player");
           player.transform.position = otherPortal.ReappearSpawnPoint.position;
           player.transform.rotation = otherPortal.ReappearSpawnPoint.rotation;
       }
       private NPCPortal GetNPCPortal()
       {
           foreach (NPCPortal portal in FindObjectsOfType<NPCPortal>())
           {
               if (portal == this) continue;
               if (portal.NPCDestination != _dialogueDestination) continue;
               
               return portal;
           }
           return null;
       }
    }
}


