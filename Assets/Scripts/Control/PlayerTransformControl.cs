using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.Inventories;
using UnityEngine;

namespace Game.Control
{
    public class PlayerTransformControl : MonoBehaviour
    {
        [SerializeField] GameObject thumbnail;
        [SerializeField] GameObject playerObject;
        [SerializeField] GameObject monsterObject;
        [SerializeField] GameObject currentObject;

        private bool _isMonster;
        public bool IsMonster {get { return _isMonster; }}
        private PlayerTransformState playerTransformState;

        Equipment playerEquipment;
        [SerializeField] SO_EquipableItem equipableItem;

        private void Awake() 
        {
            var player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
            playerEquipment = player.GetComponent<Equipment>();
        }
        
        private void OnEnable()
        {
            EventHandler.TransformEvent += TransformAction;
            EventHandler.PlayerTransformStateEvent += TransformPlayer;
            EventHandler.TransformEvent += AddCurse;
            EventHandler.TransformEvent += RemoveCurse;
        }

        private void OnDisable()
        {
            EventHandler.TransformEvent -= TransformAction;
            EventHandler.PlayerTransformStateEvent -= TransformPlayer;
            EventHandler.TransformEvent -= AddCurse;
            EventHandler.TransformEvent -= RemoveCurse;
        }

        void Start() {
            // thumbnail is only used so we have a reference in the scene editor
            Destroy(thumbnail);

            // player starts as a human
            playerTransformState = PlayerTransformState.Human;
            currentObject = Instantiate(playerObject, transform.position, Quaternion.identity);
            currentObject.transform.SetParent(gameObject.transform);
  
        }

        private void AddCurse()
        {
            if(_isMonster) playerEquipment.AddItem(EquipLocation.Head, equipableItem);
        }

        public void RemoveCurse()
        {
            if(!_isMonster) playerEquipment.RemoveItem(EquipLocation.Head);
        }

        // Update is called once per frame
        // void Update() {
        //     if (Input.GetKeyDown("m")) {
        //         isMonster = !isMonster;
        //         Destroy(currentObject);

        //         if (isMonster) {
        //             currentObject = Instantiate(monsterObject, transform.position, Quaternion.identity);
        //         } else {
        //             currentObject = Instantiate(playerObject, transform.position, Quaternion.identity);
        //         }
        //         currentObject.transform.SetParent(gameObject.transform);
        //     }
        // }

        // public bool hasIsMonster() {
        //     return isMonster;
        // }

        private void TransformAction()
        { 
            if (playerTransformState == PlayerTransformState.Monster) 
            {
                EventHandler.CallPlayerTransformStateEvent(PlayerTransformState.Human);
                return;
            }
            if (playerTransformState == PlayerTransformState.Human)
            {
                EventHandler.CallPlayerTransformStateEvent(PlayerTransformState.Monster);
                return;
            }
        }

        private void TransformPlayer(PlayerTransformState transformState)
        {
            Destroy(currentObject);
            Debug.Log("Tranform: " + transformState);

            if (transformState == PlayerTransformState.Monster && playerTransformState == PlayerTransformState.Human) 
            {
                _isMonster = true;
                currentObject = Instantiate(monsterObject, transform.position, Quaternion.identity);
            }
            if (transformState == PlayerTransformState.Human && playerTransformState == PlayerTransformState.Monster)
            {
                _isMonster = false;
                currentObject = Instantiate(playerObject, transform.position, Quaternion.identity);
            }
            currentObject.transform.SetParent(gameObject.transform);
            playerTransformState = transformState;
        }
    }
    
}

