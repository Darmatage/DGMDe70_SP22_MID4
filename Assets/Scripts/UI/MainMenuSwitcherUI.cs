using UnityEngine;

namespace Game.UI
{
    public class MainMenuSwitcherUI : MonoBehaviour
    {
        [SerializeField] GameObject entryPoint;

        private void Start() 
        {
            SwitchTo(entryPoint);
        }
        public void SwitchTo(GameObject toDisplay)
        {
            if(toDisplay.transform.parent != transform) return;

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(child.gameObject == toDisplay);
            }
        }

        public void QuitGame()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

    }
}
