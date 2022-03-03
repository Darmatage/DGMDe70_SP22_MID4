using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Control;
using Game.Enums;

namespace Game.SceneManagement
{
    public class NPCPortal : MonoBehaviour
    {
    // enum DestinationIdentifier
    // {
    //     A, B, C, D, E, F
    // }
    [SerializeField] private SceneName sceneNameGoto = SceneName.Scene_Main;
    [SerializeField] Transform _reappearSpawnPoint;
    public Transform ReappearSpawnPoint { get => _reappearSpawnPoint; }
    [SerializeField] CutSceneDestinationIdentifier _nPCDestination;
    public CutSceneDestinationIdentifier NPCDestination { get => _nPCDestination; }
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

                DialoguePortal otherPortal = GetDialoguePortal();
                UpdatePlayer(otherPortal);

                wrapper.Save();

                EventHandler.CallActiveGameUI(true);

                yield return new WaitForSeconds(fadeWaitTime);
                fader.FadeIn(fadeInTime);

                Destroy(gameObject);
       }
       private void UpdatePlayer(DialoguePortal dialPortal)
       {
           GameObject player = GameObject.FindWithTag("Player");
           player.transform.position = dialPortal.DialogueSpawnPoint.position;
           player.transform.rotation = dialPortal.DialogueSpawnPoint.rotation;
       }
       private DialoguePortal GetDialoguePortal()
       {
           foreach (DialoguePortal portal in FindObjectsOfType<DialoguePortal>())
           {
               if (portal == this) continue;
               if (portal.DialogueDestination != _nPCDestination) continue;
               
               return portal;
           }
           return null;
       }
    }
}


