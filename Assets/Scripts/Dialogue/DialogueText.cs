using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Control;
using Game.Enums;

namespace Game.Story 
{
    public class GameDialogue {
      Dictionary<string, string> dict = new Dictionary<string, string>();

      public GameDialogue() {
        setDialogue();
      }

      public string getDialogue(GameScenes scene, GameStages stage, CutSceneDestinationIdentifier npc, bool isMonster = false, DialogueVariant variant = DialogueVariant.DV_01) {
        //Useful for debugging
        // Debug.Log("Who is the NPC??: " + npc);
        // Debug.Log("Key: " + getKey(scene, stage, npc, isMonster, variant));
        
        //return dict[getKey(scene, stage, npc, variant, false)];
        return dict[getKey(scene, stage, npc, isMonster, variant)];
      }

      private string getKey(GameScenes scene, GameStages stage, CutSceneDestinationIdentifier npc, bool isMonster = false, DialogueVariant variant = DialogueVariant.DV_01) {
        return $"{scene.ToString()}-{stage.ToString()}-{npc.ToString()}-{isMonster.ToString()}-{variant.ToString()}";
      }

      /*
      Set the dialogue for the game here!
      */

      public void setDialogue() {
        dict.Add(getKey(
          GameScenes.Scene_01,
          GameStages.Stage_01,
          CutSceneDestinationIdentifier.Wizard),
        "It's been a while since I've had visitors!!");

        dict.Add(getKey(
          GameScenes.Scene_01,
          GameStages.Stage_02,
          CutSceneDestinationIdentifier.Wizard),
        "Hope you enjoy your first quest!!");

        dict.Add(getKey(
          GameScenes.Scene_02,
          GameStages.Stage_01,
          CutSceneDestinationIdentifier.Wizard),
        "I have new text cause its the second scene!!");

        //Useful for debugging
        /*Debug.Log("KEYS:");
        foreach (string key in dict.Keys) {
          Debug.Log(key);
        }*/
      }
    }
}
