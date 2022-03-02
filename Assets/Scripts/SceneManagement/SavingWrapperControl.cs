using UnityEngine;
using Game.Saving;

public class SavingWrapperControl : MonoBehaviour
{
    const string defaultSaveFile = "save";

    private void OnEnable() 
    {
        EventHandler.QuitPlayGameEvent += DeleteFile;
    }
    private void OnDisable() 
    {
        EventHandler.QuitPlayGameEvent -= DeleteFile;
    }

    public void Save()
    {
        GetComponent<SavingSystem>().Save(defaultSaveFile);
    }

    public void Load()
    {
        GetComponent<SavingSystem>().Load(defaultSaveFile);
    }
    private void DeleteFile()
    {
        GetComponent<SavingSystem>().Delete(defaultSaveFile);
        Debug.Log("Delete Save");
    }

}
