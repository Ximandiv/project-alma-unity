using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "LevelStatus", menuName = "Scriptables/LevelStatus")]
    public class LevelStatus : ScriptableObject 
    {
        #region Public Variables
            public Vector3 playerPosition = Vector3.zero;
            public Dictionary<int,bool> enabledStatusList;
        #endregion

        #region Public Methods
            public void ClearLevelStatus()
            {
                playerPosition = Vector3.zero;
                enabledStatusList  = new ();
            }
        #endregion

        #region Unity API Methods
            private void OnEnable() => ClearLevelStatus();
        #endregion

    }
}