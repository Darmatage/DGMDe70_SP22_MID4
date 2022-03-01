// using UnityEngine;
// using Game.Crafting;

// namespace Game.UI.Crafting
// {
//     public class ShowCraftingUI : MonoBehaviour//, IRaycastable
//     {
//         [SerializeField] SO_CraftingRecipe craftingRecipe = null;
//         [SerializeField] CraftingUI craftingItems = null;
//         [SerializeField] GameObject craftingUI = null;
//         [SerializeField] float minimumCraftingDistance = 2.5f;
        
//         PlayerController playerController;
//         CharacterMovement characterMovement;
//         GameObject[] craftingTables;

//         private void Awake()
//         {
//             // Find the player gameobject using the tag "Player", and get its PlayerController component.
//             playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
//             // Find the CharacterMovement component from the playerController.
//             characterMovement = playerController.GetComponent<CharacterMovement>();
        
//             // Find all game objects with the tag "CraftingTable".
//             craftingTables = GameObject.FindGameObjectsWithTag("CraftingTable");
//         }

//         private void Start()
//         {
//             // Disable the crafting UI gameobject.
//             craftingUI.SetActive(false);
//         }

//         private bool IsWithinDistance(PlayerController playerController)
//         {
//             // Iterate through the craftingTables GameObjects array.
//             foreach(GameObject craftingTable in craftingTables)
//             {
//                 // Using Vector3.Distance check the distance between the player and the current iteration.
//                 // We use the cached PlayerController component to get the position of the gameobject.
//                 // Return true if yes.
//                 if(Vector3.Distance(playerController.transform.position, craftingTable.transform.position) < craftingRange) return true;
//             }
        
//             // Return false by default.
//             return false;
//         }

//         private void Update()
//         {
//             // Check if IsWithinDistance returns false.
//             if(!IsWithinDistance(playerController) && craftingUI.activeSelf)
//             {
//                 // Disable the crafting UI gameobject.
//                 craftingUI.SetActive(false);
//             }
//         }

//         public CursorType GetCursorType()
//         {
//             // Return the desired CursorType enum member.
//             return CursorType.Crafting;
//         }

//         public bool HandleRaycast(PlayerController callingController)
//         {    
//             // Check if the player clicked the left mouse button.
//             if(Input.GetMouseButtonDown(0))
//             {
//                 // If the left mouse button was clicked, check if the player is not within the minimum distance to the crafting table.
//                 if(!IsWithinDistance(callingController))
//                 {
//                     // If outside minimum distance:
//                     // Move player character to the crafting table position.
//                     characterMovement.MoveTo(transform.position, 1f);
//                 }
//                 else
//                 {
//                     // If within minimum distance:
//                     // Cancel character movement using the navmesh isStopped property. 
//                     characterMovement.Cancel();
        
//                     // Setup the recipes in the crafting UI.
//                     // The SetupRecipes function simply assigns craftingRecipe to the local variable in CraftingUI component (craftingItems).
//                     // We don’t do this in Awake or Start for example, because there might be more than one crafting tables that offer different recipes.
//                     craftingItems.SetupRecipes(craftingRecipe);
        
//                     // Enable the crafting UI gameobject.
//                     craftingUI.SetActive(!craftingUI.activeSelf);
//                 }
//             }
//             return true;
//         }
//     }
// }