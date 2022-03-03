using UnityEngine;
using UnityEditor;

   /// <summary>
   /// This is only in place to delete the save game file eveny tile the game is stopped being played while testing.
    /// </summary>

// [InitializeOnLoadAttribute]
public class SavedFileCleaner : MonoBehaviour
{
    // // register an event handler when the class is initialized
    // static SavedFileCleaner()
    // {
    //     EditorApplication.playModeStateChanged += LogPlayModeState;
    // }
    // private static void LogPlayModeState(PlayModeStateChange state)
    // {
    //     if(state == PlayModeStateChange.ExitingPlayMode)
    //     {
    //         Debug.Log("Delete save file");
    //         EventHandler.CallQuitPlayGameEvent();
    //     }
    // }

}
