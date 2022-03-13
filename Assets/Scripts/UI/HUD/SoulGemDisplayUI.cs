// using System;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using Game.Inventories;

// namespace Game.UI.HUD
// {
//     public class SoulGemDisplayUI : MonoBehaviour
//     {
//         [SerializeField] TextMeshProUGUI karmasValueField;

//         SoulGemManager playerSoulCount = null;

//         private void Awake() 
//         {
//             playerSoulCount = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<SoulGemManager>();
//         }

//         private void Start() {
//             if (playerSoulCount != null)
//             {
//                 playerSoulCount.onChange += RefreshUI;
//             }

//             RefreshUI();
//         }

//         private void RefreshUI()
//         {
//             karmasValueField.text = playerSoulCount.GetKarmaCount().ToString();
//         }

//     }
// }