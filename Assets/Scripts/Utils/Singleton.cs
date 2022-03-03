using UnityEngine;
public class Singleton<T> : MonoBehaviour where T : Singleton<T> {

    static T instance;
    public static T Instance { get { return instance; } }

    protected virtual void Awake () {
        if (instance != null) {
            Destroy(this.gameObject);
        } else {
            instance = (T)this;
        }
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void OnDestroy () {
        if (instance == this) {
            instance = null;
        }
    }
}