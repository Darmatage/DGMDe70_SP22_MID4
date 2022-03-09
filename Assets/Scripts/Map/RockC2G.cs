using System.Collections;
using System.Collections.Generic;
using Game.Control;
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

        RestoreMapStat();
    }

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
      private void RestoreMapStat()
    {
        bool isMonster = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerTransformControl>().IsMonster;
        if (isMonster) {
            mat.SetFloat("humanity", 1);
        } else {
            mat.SetFloat("humanity", 0);
        }
    }


}
