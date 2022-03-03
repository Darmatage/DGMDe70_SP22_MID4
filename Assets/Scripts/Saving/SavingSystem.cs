using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Saving
{
    /// <summary>
    /// This component provides the interface to the saving system. It provides
    /// methods to save and restore a scene.
    ///
    /// This component should be created once and shared between all subsequent scenes.
    /// </summary>
        public class SavingSystem : MonoBehaviour
    {
        //public static Dictionary<string, object> saveState = FindObjectOfType<SavedFileCleaner>().sa;
        public IEnumerator LoadLastScene()
        {
            Dictionary<string, object> state = LoadFile();
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            if (state.ContainsKey("lastSceneBuildIndex"))
            {
                buildIndex = (int)state["lastSceneBuildIndex"];
            }
            yield return SceneManager.LoadSceneAsync(buildIndex);
            RestoreState(state);
        }

        /// <summary>
        /// Save the current scene to the provided save file.
        /// </summary>
        public void Save()
        {
            Dictionary<string, object> state = LoadFile();
            CaptureState(state);
            SaveFile(state);
        }

        public void Load()
        {
            RestoreState(LoadFile());
        }

        // PRIVATE

        private Dictionary<string, object> LoadFile()
        {
            SavedFileSingleton saveFileVar = FindObjectOfType<SavedFileSingleton>();
            return saveFileVar.GetSaveState();
        }

        private void SaveFile(Dictionary<string, object> state)
        {
            SavedFileSingleton saveFileVar = FindObjectOfType<SavedFileSingleton>();
            saveFileVar.SetSaveState(state);
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }

            state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                string id = saveable.GetUniqueIdentifier();
                if (state.ContainsKey(id))
                {
                    saveable.RestoreState(state[id]);
                }
            }
        }
    }
}