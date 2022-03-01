using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using UnityEngine;

public class RockC2G : MonoBehaviour
{
    // public GameObject gameHandler;
    //private GameHandler gameHandlerObj;
    Material mat;

    private void OnEnable()
    {
        EventHandler.PlayerTransformStateEvent += TransformMapStat;
    }

    private void OnDisable()
    {
        EventHandler.PlayerTransformStateEvent -= TransformMapStat;
    }
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        // if (GameObject.FindWithTag("GameHandler") != null){
        //     gameHandlerObj = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        //}
    }

    // Update is called once per frame
    // void Update()
    // {
    //     bool isMonster = gameHandlerObj.isMonster(); //gameHandler.GetComponent<GameHandler>().isMonster();

    //     if (isMonster) {
    //         mat.SetFloat("humanity", 1);
    //     } else {
    //         mat.SetFloat("humanity", 0);
    //     }
    // }

    private void TransformMapStat(PlayerTransformState transformStat)
    {
        if (transformStat == PlayerTransformState.Monster)
        {
            mat.SetFloat("humanity", 1);
        }
        else if (transformStat == PlayerTransformState.Human)
        {
            mat.SetFloat("humanity", 0);
        }
    }


}
