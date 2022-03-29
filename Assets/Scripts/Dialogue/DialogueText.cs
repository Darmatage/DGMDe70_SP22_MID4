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
        scene02();
        scene03();
        scene04();
        scene05();

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
          new string[] {"Welcome, fellow soldier! Sir Walter is expecting you by the barracks! Go see him immediately!"},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_01,
          GameStages.Stage_04,
          CutSceneDestinationIdentifier.Guard),
          new string[] {"Well, we're drafting able bodied souls to help us fight local monsters. Go see Sir Walter by the barracks!"},
          nextAction
        );

        // SIR WALTER

        setDialogue(getKey(
          GameScenes.Scene_01,
          GameStages.Stage_05,
          CutSceneDestinationIdentifier.SirWalter),
          new string[] {
            "Hey there- you look like a strong fighter! Are you looking for me?",
            "I'm an enlisted footman sent here",
            "I don't know why I'm here"
          },
          new System.Action[] {
            () => Debug.Log("Chose 1"),
            () => Debug.Log("Chose 2")
          }
        );

        setDialogue(getKey(
          GameScenes.Scene_01,
          GameStages.Stage_06,
          CutSceneDestinationIdentifier.SirWalter),
          new string[] {"Well you'll need a job soon! As captain of the Goldfleece guard, I hereby draft you into military service. Congratulations!"},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_01,
          GameStages.Stage_07,
          CutSceneDestinationIdentifier.SirWalter),
          new string[] {"Welcome, recruit! You'll be a hero in no time. Take this sword."},
          nextAction
        );

        // GUARD

        setDialogue(getKey(
          GameScenes.Scene_01,
          GameStages.Stage_08,
          CutSceneDestinationIdentifier.Guard),
          new string[] {"Guard: Sir Walter! The wolves are here again!"}
        );

        // SIR WALTER

        setDialogue(getKey(
          GameScenes.Scene_01,
          GameStages.Stage_09,
          CutSceneDestinationIdentifier.SirWalter),
          new string[] {"Go defend us!"},
          nextAction
        );

      }

      private void scene02() {

        // NARRATOR

        setDialogue(getKey(
          GameScenes.Scene_02,
          GameStages.Stage_01,
          CutSceneDestinationIdentifier.Narrator),
          new string[] {"As you fight the wolves, you feel a strange power awaken inside you…"},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_02,
          GameStages.Stage_02,
          CutSceneDestinationIdentifier.Narrator),
          new string[] {"You transform into a monster, with powerful attacks, but more vulnerable!"},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_02,
          GameStages.Stage_03,
          CutSceneDestinationIdentifier.Narrator),
          new string[] {"What secrets await?"}
        );
      }

      private void scene03() {

        // NARRATOR

        setDialogue(getKey(
          GameScenes.Scene_03,
          GameStages.Stage_01,
          CutSceneDestinationIdentifier.Narrator),
          new string[] {"You return to human form, with a terrible headache."},
          nextAction
        );

        // HERO

        setDialogue(getKey(
          GameScenes.Scene_03,
          GameStages.Stage_02,
          CutSceneDestinationIdentifier.Hero),
          new string[] {"What just happened? Where are the wolves, or townsfolk?"},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_03,
          GameStages.Stage_03,
          CutSceneDestinationIdentifier.Hero),
          new string[] {"Was I a monster?"},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_03,
          GameStages.Stage_04,
          CutSceneDestinationIdentifier.Hero),
          new string[] {"Look at these tracks out of town… My first clue…!"},
          nextAction
        );

        // NARRATOR

        setDialogue(getKey(
          GameScenes.Scene_03,
          GameStages.Stage_05,
          CutSceneDestinationIdentifier.Narrator),
          new string[] {"As the adrenaline from your fight subsides, you notice a bloodstained note on a tree, near the end of the tracks."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_03,
          GameStages.Stage_06,
          CutSceneDestinationIdentifier.Narrator),
          new string[] {
            "Do you want to read the note?",
            "Yes",
            "No"
          },
          new System.Action[] {
            () => Debug.Log("Chose 1"),
            () => Debug.Log("Chose 2")
          }
        );

        setDialogue(getKey(
          GameScenes.Scene_03,
          GameStages.Stage_07,
          CutSceneDestinationIdentifier.Narrator),
          new string[] {"The note is from your father, Gerald: \"Find me at Crystal Cove! I'll explain everything!\""},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_03,
          GameStages.Stage_08,
          CutSceneDestinationIdentifier.Narrator),
          new string[] {"You ignore the note and leave it behind, never to find it again."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_03,
          GameStages.Stage_09,
          CutSceneDestinationIdentifier.Narrator),
          new string[] {"Maybe I should return to the barracks?"},
          nextAction
        );
      }

      private void scene04() {

        // SIR WALTER

        setDialogue(getKey(
          GameScenes.Scene_04,
          GameStages.Stage_01,
          CutSceneDestinationIdentifier.SirWalter),
          new string[] {
            "Glad to see you’re still with us! That attack was strange!",
            "Nothing strange about a few wolves",
            "Yes, there was… a monster there… not a wolf"
          },
          new System.Action[] {
            () => Debug.Log("Chose 1"),
            () => Debug.Log("Chose 2")
          }
        );

        setDialogue(getKey(
          GameScenes.Scene_04,
          GameStages.Stage_02,
          CutSceneDestinationIdentifier.SirWalter),
          new string[] {"Ha! You must have been knocked out cold. There was a massive monster that quickly killed the wolves. We called our men back and stood in awe."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_04,
          GameStages.Stage_03,
          CutSceneDestinationIdentifier.SirWalter),
          new string[] {"Yes, you saw it! There was a massive monster that quickly killed the wolves. We called our men back and stood in awe."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_04,
          GameStages.Stage_04,
          CutSceneDestinationIdentifier.SirWalter),
          new string[] {
            "So, do you know anything about the monster? Don't be shy, now.",
            "I think… that monster was me…",
            "[Lie] I don't know anything more than you do"
          },
          new System.Action[] {
            () => Debug.Log("Chose 1"),
            () => Debug.Log("Chose 2")
          }
        );

        setDialogue(getKey(
          GameScenes.Scene_04,
          GameStages.Stage_05,
          CutSceneDestinationIdentifier.SirWalter),
          new string[] {"Well now! We've heard of cursed humans before- I'm going to have to ask you to leave the village, and only return for necessities."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_04,
          GameStages.Stage_06,
          CutSceneDestinationIdentifier.SirWalter),
          new string[] {"But take this healing gift, as a reward for solving our wolf problem."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_04,
          GameStages.Stage_07,
          CutSceneDestinationIdentifier.SirWalter),
          new string[] {"You're welcome to be a part of our society if you can cure your curse. Your father is in the area- perhaps he can help you find your cure?"},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_04,
          GameStages.Stage_08,
          CutSceneDestinationIdentifier.SirWalter),
          new string[] {"Hmm, You were missing for a long while. I suppose it can take time to recover from battle."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_04,
          GameStages.Stage_09,
          CutSceneDestinationIdentifier.SirWalter),
          new string[] {"As a new recruit, we must send you on a proving quest, to earn your place here."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_04,
          GameStages.Stage_10,
          CutSceneDestinationIdentifier.SirWalter),
          new string[] {"Your orders are to investigate this monster sighting, and find it's cause. Your father should be in the area- he may have the answers we seek. Go find him!"},
          nextAction
        );
      }

      private void scene05() {

        // NARRATOR

        setDialogue(getKey(
          GameScenes.Scene_05,
          GameStages.Stage_01,
          CutSceneDestinationIdentifier.Narrator),
          new string[] {
            "You see what looks like a friendly.. Lizard man? Being harassed by wolves.",
            "Help him",
            "Don't commit to help"
          },
          new System.Action[] {
            () => Debug.Log("Chose 1"),
            () => Debug.Log("Chose 2")
          }
        );

        // STUART

        setDialogue(getKey(
          GameScenes.Scene_05,
          GameStages.Stage_02,
          CutSceneDestinationIdentifier.Stuart),
          new string[] {"Thank you, hero! I'm Stuart. I used to be human like you, but I was experimented on by Queen Esther."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_05,
          GameStages.Stage_03,
          CutSceneDestinationIdentifier.Stuart),
          new string[] {"She said that I'll live over 100 years and that I'm immune to diseases, but I'm the only one of my kind- so far, anyway."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_05,
          GameStages.Stage_04,
          CutSceneDestinationIdentifier.Stuart),
          new string[] {"Sir Walter asked me to limit my contact with the village, unless I find some sort of cure."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_05,
          GameStages.Stage_05,
          CutSceneDestinationIdentifier.Stuart),
          new string[] {"I can… smell it on you. You're cursed too. You should be able to switch between your human and cursed form at will- but at a cost."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_05,
          GameStages.Stage_06,
          CutSceneDestinationIdentifier.Stuart),
          new string[] {
            "My fellow cursed, please accept this rusty sword- It has magic properties- if only you could repair it.",
            "Accept sword",
            "Turn down gift"
          },
          new System.Action[] {
            () => Debug.Log("Chose 1"),
            () => Debug.Log("Chose 2")
          }
        );

        setDialogue(getKey(
          GameScenes.Scene_05,
          GameStages.Stage_07,
          CutSceneDestinationIdentifier.Stuart),
          new string[] {"No, you need it more than me."}
        );

      }

      private void scene06() {

        // WIZARD

        setDialogue(getKey(
          GameScenes.Scene_06,
          GameStages.Stage_01,
          CutSceneDestinationIdentifier.Wizard),
          new string[] {"A human! I hadn't seen your kind for years, but you're the fourth I've seen today!"},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_06,
          GameStages.Stage_02,
          CutSceneDestinationIdentifier.Wizard),
          new string[] {
            "I specialize in restoration magic. If you help me, I'll give you a valuable scroll. Will you help?",
            "Yes",
            "No"
          },
          new System.Action[] {
            () => Debug.Log("Chose 1"),
            () => Debug.Log("Chose 2")
          }
        );

        setDialogue(getKey(
          GameScenes.Scene_06,
          GameStages.Stage_03,
          CutSceneDestinationIdentifier.Wizard),
          new string[] {"Great! I'm hungry for bacon, and there's a were-pig nearby. Their bacon is the juiciest, tastiest thing. I know you'll be tempted to eat it yourself, but give me at least 3 pieces of it."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_06,
          GameStages.Stage_04,
          CutSceneDestinationIdentifier.Wizard),
          new string[] {
            "Oh, well that's too bad. Would you consider my offer later?",
            "Yes",
            "No"
          },
          new System.Action[] {
            () => Debug.Log("Chose 1"),
            () => Debug.Log("Chose 2")
          }
        );

        setDialogue(getKey(
          GameScenes.Scene_06,
          GameStages.Stage_05,
          CutSceneDestinationIdentifier.Wizard),
          new string[] {"Fine, take it! Younguns these days have no respect!"},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_06,
          GameStages.Stage_06,
          CutSceneDestinationIdentifier.Wizard),
          new string[] {"Well done! Here, take this scroll- use it on any rusty magic item, and it will be good as new!"},
          nextAction
        );
      }

      private void scene07() {

        // ALEX

        setDialogue(getKey(
          GameScenes.Scene_07,
          GameStages.Stage_01,
          CutSceneDestinationIdentifier.Alex),
          new string[] {
            "What on earth are you doing here? This isn't safe!",
            "I'm here to save our dad!",
            "I'm here to cure my curse"
          },
          new System.Action[] {
            () => Debug.Log("Chose 1"),
            () => Debug.Log("Chose 2")
          }
        );

        setDialogue(getKey(
          GameScenes.Scene_07,
          GameStages.Stage_02,
          CutSceneDestinationIdentifier.Alex),
          new string[] {"Good! He's at the spring trying to stop Esther, I had to leave because one of those disgusting monsters broke my arm."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_07,
          GameStages.Stage_03,
          CutSceneDestinationIdentifier.Alex),
          new string[] {"Esther is trying to poison the water supply with a curse that will turn everyone into lizard people. She says it will make the world better, and we will all live longer. I don't believe her."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_07,
          GameStages.Stage_04,
          CutSceneDestinationIdentifier.Alex),
          new string[] {"Please, stop her from poisoning the water supply!"},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_07,
          GameStages.Stage_05,
          CutSceneDestinationIdentifier.Alex),
          new string[] {"You've been cursed! Don't tell me that you're turning into a monster! I don't have time to ask you- I have to get to the doctor in town- my arm is badly broken."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_07,
          GameStages.Stage_06,
          CutSceneDestinationIdentifier.Alex),
          new string[] {"Esther is trying to poison the water supply with a curse that will turn everyone into lizard people. She says it will make the world better, and we will all live longer. You know not to believe her."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_07,
          GameStages.Stage_07,
          CutSceneDestinationIdentifier.Alex),
          new string[] {"Please, stop her from poisoning the water supply! Maybe you can find a cure with dad!"},
          nextAction
        );
      }

      private void scene08() {
        setDialogue(getKey(
          GameScenes.Scene_08,
          GameStages.Stage_01,
          CutSceneDestinationIdentifier.Narrator),
          new string[] {
            "Who do you side with?",
            "Gerald",
            "Esther"
          },
          new System.Action[] {
            () => Debug.Log("Chose 1"),
            () => Debug.Log("Chose 2")
          }
        );
      }

      private void scene09() {

        // TROLL

        setDialogue(getKey(
          GameScenes.Scene_09,
          GameStages.Stage_01,
          CutSceneDestinationIdentifier.Troll),
          new string[] {"HOLD UP. You don't get to pass MY BRIDGE for free."},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_09,
          GameStages.Stage_02,
          CutSceneDestinationIdentifier.Troll),
          new string[] {
            "You'll have to solve my riddle or fight me!",
            "Attempt Riddle",
            "Slay Troll"
          },
          new System.Action[] {
            () => Debug.Log("Chose 1"),
            () => Debug.Log("Chose 2")
          }
        );

        // HERO

        setDialogue(getKey(
          GameScenes.Scene_09,
          GameStages.Stage_03,
          CutSceneDestinationIdentifier.Hero),
          new string[] {"Prepare to die, troll!"},
          nextAction
        );

        // TROLL

        setDialogue(getKey(
          GameScenes.Scene_09,
          GameStages.Stage_04,
          CutSceneDestinationIdentifier.Troll),
          new string[] {
            "I have a tail but I'm not a mouse I have wings but I'm not a bird",
            "Bat",
            "Fairy",
            "Unladen Swallow",
            "Dragon"
          },
          new System.Action[] {
            () => Debug.Log("Chose 1"),
            () => Debug.Log("Chose 2"),
            () => Debug.Log("Chose 3"),
            () => Debug.Log("Chose 4")
          }
        );

        // TROLL

        setDialogue(getKey(
          GameScenes.Scene_09,
          GameStages.Stage_05,
          CutSceneDestinationIdentifier.Troll),
          new string[] {"Wrong! Go away!"},
          nextAction
        );

        setDialogue(getKey(
          GameScenes.Scene_09,
          GameStages.Stage_06,
          CutSceneDestinationIdentifier.Troll),
          new string[] {"Right! Come on through, worthy adventurer! "},
          nextAction
        );

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
