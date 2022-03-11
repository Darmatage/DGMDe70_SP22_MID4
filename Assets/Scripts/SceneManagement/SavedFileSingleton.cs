using System.Collections.Generic;
using UnityEngine;
using Game.Enums;
public class SavedFileSingleton : Singleton<SavedFileSingleton>
{
    public static Dictionary<string, object> saveState;
    public CurseTypes selectedCurse;

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

    public void SetCurseType(CurseTypes chosenCurse)
    {
        selectedCurse = chosenCurse;
    }
    public CurseTypes GetCurseType()
    {
        return selectedCurse;
    }

}
