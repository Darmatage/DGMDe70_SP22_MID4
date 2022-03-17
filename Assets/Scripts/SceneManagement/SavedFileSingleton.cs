using System.Collections.Generic;
using UnityEngine;
using Game.Enums;
public class SavedFileSingleton : Singleton<SavedFileSingleton>
{
    public static Dictionary<string, object> saveState;
    public static float volumeLevel = 0.5f;
    public CurseTypes selectedCurse = CurseTypes.Werewolf;

    protected override void Awake() {
        base.Awake();
    }
    private void Start() 
    {
        saveState = new Dictionary<string, object>();
    }

    public Dictionary<string, object> GetSaveState()
    {
        //Debug.Log("Get Save: " + saveState);
        return saveState;
    }

    public void SetSaveState(Dictionary<string, object> newSave)
    {
        saveState = newSave;
        //Debug.Log("Set Save: " + saveState);
    }

    public void SetVolumeLevel(float level)
    {
        volumeLevel = level;
    }

    public float GetVolumeLevel()
    {
        return volumeLevel;
    }

    public void SetCurseType(CurseTypes chosenCurse)
    {
        selectedCurse = chosenCurse;
    }
    public CurseTypes GetCurseType()
    {
        return selectedCurse;
    }

}
