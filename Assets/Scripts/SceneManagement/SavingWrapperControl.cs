using UnityEngine;
using Game.Saving;

public class SavingWrapperControl : MonoBehaviour
{
    const string defaultSaveFile = "save";

    public void Save()
    {
        GetComponent<SavingSystem>().Save();
    }

    public void Load()
    {
        GetComponent<SavingSystem>().Load();
    }

}
