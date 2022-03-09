using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.Saving;
using UnityEngine;

namespace Game.Control
{
    public class PlayerTransformControl : MonoBehaviour, ISaveable
    {
        [SerializeField] GameObject thumbnail;
        [SerializeField] GameObject humanObject;
        [SerializeField] GameObject monsterObject;
        [SerializeField] GameObject currentObject;

        private bool _isMonster;
        public bool IsMonster {get { return _isMonster; }}
        private PlayerTransformState playerTransformState;

        
        private void OnEnable()
        {
            EventHandler.TransformEvent += TransformAction;
            EventHandler.PlayerTransformStateEvent += TransformPlayer;
        }

        private void OnDisable()
        {
            EventHandler.TransformEvent -= TransformAction;
            EventHandler.PlayerTransformStateEvent -= TransformPlayer;
        }

        private void Awake() 
        {
            // thumbnail is only used so we have a reference in the scene editor
            //Destroy(thumbnail);

            // player starts as a human
            _isMonster = false;
            playerTransformState = PlayerTransformState.Human;
        }

        void Start() 
        {
            RestoreTransformPlayer(playerTransformState);
            // currentObject = Instantiate(playerObject, transform.position, Quaternion.identity);
            // currentObject.transform.SetParent(gameObject.transform);
        }

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
            // Destroy(currentObject);
            Debug.Log("Tranform: " + transformState);

            if (transformState == PlayerTransformState.Monster && playerTransformState == PlayerTransformState.Human) 
            {
                _isMonster = true;
                humanObject.SetActive(false);
                monsterObject.SetActive(true);
                // currentObject = Instantiate(monsterObject, transform.position, Quaternion.identity);
            }
            if (transformState == PlayerTransformState.Human && playerTransformState == PlayerTransformState.Monster)
            {
                _isMonster = false;
                monsterObject.SetActive(false);
                humanObject.SetActive(true);
                // currentObject = Instantiate(playerObject, transform.position, Quaternion.identity);
            }
            // currentObject.transform.SetParent(gameObject.transform);
            playerTransformState = transformState;
        }

        private void RestoreTransformPlayer(PlayerTransformState transformStateRestore)
        {
            if (transformStateRestore == PlayerTransformState.Monster && _isMonster) 
            {
                humanObject.SetActive(false);
                monsterObject.SetActive(true);
                
                // currentObject = Instantiate(monsterObject, transform.position, Quaternion.identity);
            }
            if (transformStateRestore == PlayerTransformState.Human && !_isMonster)
            {
                monsterObject.SetActive(false);
                humanObject.SetActive(true);
                // currentObject = Instantiate(playerObject, transform.position, Quaternion.identity);
            }
            // currentObject.transform.SetParent(gameObject.transform);
        }

        private struct PlayerTransformRecord
        {
            public PlayerTransformState playerTransformStateRecord;
            public bool isMonsterRecord;
        }

        object ISaveable.CaptureState()
        {
            var playerTransformRecord = new PlayerTransformRecord();

            playerTransformRecord.playerTransformStateRecord = playerTransformState;
            playerTransformRecord.isMonsterRecord = _isMonster;

            return playerTransformRecord;
        }

        void ISaveable.RestoreState(object state)
        {
            var playerTransformRecord = (PlayerTransformRecord)state;
            playerTransformState = playerTransformRecord.playerTransformStateRecord;
            _isMonster = playerTransformRecord.isMonsterRecord;

            Debug.Log("playerTransformState: " + playerTransformState);
            Debug.Log("Is Moster: " + _isMonster);

            RestoreTransformPlayer(playerTransformState);
        }
    }
    
}

