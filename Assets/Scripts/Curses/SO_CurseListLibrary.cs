using System.Collections.Generic;
using Game.Enums;
using UnityEngine;

namespace Game.Curses
{

    [CreateAssetMenu(fileName = "CurseLibrary", menuName = ("Game/Player/Curses/New Curse Library"))]
    public class SO_CurseListLibrary : ScriptableObject
    {
        [SerializeField] CurseTypeList[] curseTypeList = null;

        Dictionary<CurseTypes, Dictionary<PlayerTransformState, SO_Curse>> curseLookupTable = null;

        public SO_Curse GetCurseSO(CurseTypes curseType, PlayerTransformState formState)
        {
            BuildLookup();

            if (!curseLookupTable[curseType].ContainsKey(formState))
            {
                return null;
            }

            return curseLookupTable[curseType][formState];
        }


        private void BuildLookup()
        {
            if (curseLookupTable != null) return;

            curseLookupTable = new Dictionary<CurseTypes, Dictionary<PlayerTransformState, SO_Curse>>();

            foreach (CurseTypeList curseListItem in curseTypeList)
            {
                var formLookupTable = new Dictionary<PlayerTransformState, SO_Curse>();

                foreach (CurseFormPairs curseFormPairItem in curseListItem.curseFormPairSet)
                {
                    formLookupTable[curseFormPairItem.formState] = curseFormPairItem.curseSO;
                }

                curseLookupTable[curseListItem.curseType] = formLookupTable;
            }
        }

        [System.Serializable]
        class CurseTypeList
        {
            public CurseTypes curseType;
            public CurseFormPairs[] curseFormPairSet;
        }

        [System.Serializable]
        class CurseFormPairs
        {
            public PlayerTransformState formState;
            public SO_Curse curseSO;
        }

       
    }
}
