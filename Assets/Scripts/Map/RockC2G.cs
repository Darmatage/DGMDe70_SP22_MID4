using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockC2G : MonoBehaviour
{
    public GameObject gameHandler;
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        bool isMonster = gameHandler.GetComponent<GameHandler>().isMonster();

        if (isMonster) {
            mat.SetFloat("humanity", 1);
        } else {
            mat.SetFloat("humanity", 0);
        }
    }
}
