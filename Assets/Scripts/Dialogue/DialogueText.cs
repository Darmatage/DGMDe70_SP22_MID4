using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Control;
using Game.Enums;

namespace Game.Story 
{
    public class GameDialogue {
      Dictionary<string, System.Action[]> actions = new Dictionary<string, System.Action[]>();
      Dictionary<string, string[]> text = new Dictionary<string, string[]>();

      public GameDialogue() {
        initDialogue();
      }

      public System.Action[] getActions(GameScenes scene, GameStages stage, CutSceneDestinationIdentifier npc, bool isMonster = false, DialogueVariant variant = DialogueVariant.DV_01) {
        return actions[getKey(scene, stage, npc, isMonster, variant)];
      }

      public string[] getDialogue(GameScenes scene, GameStages stage, CutSceneDestinationIdentifier npc, bool isMonster = false, DialogueVariant variant = DialogueVariant.DV_01) {
        //Useful for debugging
        // Debug.Log("Who is the NPC??: " + npc);
        // Debug.Log("Key: " + getKey(scene, stage, npc, isMonster, variant));
        
        //return dict[getKey(scene, stage, npc, variant, false)];
        return text[getKey(scene, stage, npc, isMonster, variant)];
      }

      public string getKey(GameScenes scene, GameStages stage, CutSceneDestinationIdentifier npc, bool isMonster = false, DialogueVariant variant = DialogueVariant.DV_01) {
        return $"{scene.ToString()}-{stage.ToString()}-{npc.ToString()}-{isMonster.ToString()}-{variant.ToString()}";
      }

      /*
      Set the dialogue for the game here!
      */

      private void initDialogue() {
        setDialogue(getKey(
          GameScenes.Scene_01,
          GameStages.Stage_01,
          CutSceneDestinationIdentifier.Wizard),
          new string[] {"It's been a while since I've had visitors!!"}
        );

        setDialogue(getKey(
          GameScenes.Scene_01,
          GameStages.Stage_02,
          CutSceneDestinationIdentifier.Wizard),
          new string[] {"Hope you enjoy your first quest!!"}
        );

        setDialogue(getKey(
          GameScenes.Scene_01,
          GameStages.Stage_03,
          CutSceneDestinationIdentifier.Wizard),
          new string[] {
            "First Choice",
            "Second Choice",
            "Third Choice",
            "Four Choice"
          },
          new System.Action[] {
            () => Debug.Log("Chose 1"),
            () => Debug.Log("Chose 2"),
            () => Debug.Log("Chose 3"),
            () => Debug.Log("Chose 4")
          }
        );

        setDialogue(getKey(
          GameScenes.Scene_01,
          GameStages.Stage_04,
          CutSceneDestinationIdentifier.Wizard),
          new string[] {"Interesting, I guess we will see."}
        );

        setDialogue(getKey(
          GameScenes.Scene_02,
          GameStages.Stage_01,
          CutSceneDestinationIdentifier.Wizard),
          new string[] {"I have new text cause its the second scene!!"}
        );

        //Useful for debugging
        /*Debug.Log("KEYS:");
        foreach (string key in text.Keys) {
          Debug.Log(key);
        }*/
      }

      private void setDialogue(string key, string[] dialogue, System.Action[] dialogueActions = null) {
        text.Add(key, dialogue);

        if (dialogueActions != null) {
          actions.Add(key, dialogueActions);
        }
      }
    }
}
