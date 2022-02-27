using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject monsterObject;
    public GameObject currentObject;

    private bool isMonster;

    // Start is called before the first frame update
    void Start() {
        currentObject = Instantiate(playerObject, transform.position, Quaternion.identity);
        currentObject.transform.SetParent(gameObject.transform);
        isMonster = false;    
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("m")) {
            isMonster = !isMonster;
            Destroy(currentObject);
            if (isMonster) {
                currentObject = Instantiate(monsterObject, transform.position, Quaternion.identity);
            } else {
                currentObject = Instantiate(playerObject, transform.position, Quaternion.identity);
            }
            currentObject.transform.SetParent(gameObject.transform);
        }
    }

    public bool hasIsMonster() {
        return isMonster;
    }
}
