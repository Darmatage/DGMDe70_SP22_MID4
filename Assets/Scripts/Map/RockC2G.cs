using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockC2G : MonoBehaviour
{
    // public GameObject gameHandler;
    private GameHandler gameHandlerObj;
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        if (GameObject.FindWithTag("GameHandler") != null){
            gameHandlerObj = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isMonster = gameHandlerObj.isMonster(); //gameHandler.GetComponent<GameHandler>().isMonster();

        if (isMonster) {
            mat.SetFloat("humanity", 1);
        } else {
            mat.SetFloat("humanity", 0);
        }
    }
}
