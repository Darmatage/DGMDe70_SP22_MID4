using System.Collections.Generic;
using UnityEngine;
public class SavedFileSingleton : Singleton<SavedFileSingleton>
{
    public static Dictionary<string, object> saveState;

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

}
