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

      System.Action[] nextAction = new System.Action[] {() => {}};

      public GameDialogue() {
        initDialogue();
      }

      public System.Action[] getActions(GameScenes scene, GameStages stage, CutSceneDestinationIdentifier npc, bool isMonster = false, DialogueVariant variant = DialogueVariant.DV_01) {
        //Useful for debugging
        // Debug.Log("Who is the NPC??: " + npc);
        // Debug.Log("Action Key: " + getKey(scene, stage, npc, isMonster, variant));
  
        try {
          return actions[getKey(scene, stage, npc, isMonster, variant)];
        }
        catch {
          return null;
        }
      }

      public string[] getDialogue(GameScenes scene, GameStages stage, CutSceneDestinationIdentifier npc, bool isMonster = false, DialogueVariant variant = DialogueVariant.DV_01) {
        //Useful for debugging
        // Debug.Log("Who is the NPC??: " + npc);
        // Debug.Log("Dialogue Key: " + getKey(scene, stage, npc, isMonster, variant));

        try {
          return text[getKey(scene, stage, npc, isMonster, variant)];
        }
        catch {
          return null;
        }
      }

      public string getKey(GameScenes scene, GameStages stage, CutSceneDestinationIdentifier npc, bool isMonster = false, DialogueVariant variant = DialogueVariant.DV_01) {
        string key = $"{scene.ToString()}-{stage.ToString()}-{npc.ToString()}-{isMonster.ToString()}-{variant.ToString()}";

        // Debug.Log("Get Key: " + key);

        return key;
      }

      /*
      Set the dialogue for the game here!
      */

      private void initDialogue() {
        scene01();

        testDialogue();

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

      private void scene01() {
        // GUARD

        setDialogue(getKey(
          GameScenes.Scene_01,
          GameStages.Stage_01,
          CutSceneDestinationIdentifier.Guard),
          new string[] {"Welcome to Goldfleece, traveler! We aren't a large town, or a safe one, or a rich one, but you're here now!"},
          nextAction // Optionally advance the conversation to the next stage.
        );

        setDialogue(getKey(
          GameScenes.Scene_01,
          GameStages.Stage_02,
          CutSceneDestinationIdentifier.Guard),
          new string[] {
            "Why are you here?", // First index is the queston / title
            "I'm an enlisted footman sent here.",
            "I don't know why I'm here"
          },
          new System.Action[] {
            () => Debug.Log("Chose 1"),
            () => Debug.Log("Chose 2")
          }
        );

        setDialogue(getKey(
          GameScenes.Scene_01,
          GameStages.Stage_03,
          CutSceneDestinationIdentifier.Guard),
          new string[] {"Well, we'll see about that won't we?"}
        );

        // WIZARD
      }

      private void testDialogue() {
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
      }
    }
}
