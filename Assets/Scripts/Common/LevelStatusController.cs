using System.Collections.Generic;
using Scripts.Events;
using Scripts.Scriptables;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Common
{
    public class LevelStatusController : MonoBehaviour
    {
        #region Private Variables
            [SerializeField] private LevelStatus  status;
            [SerializeField] private Transform player;
            [SerializeField] private Behaviour[] saveableComponents;
            [SerializeField] private GameObject[] saveableGameObjects;
            [SerializeField] private AutoSave autoSave = AutoSave.OnlyObjects;

            private enum AutoSave {
                None,
                OnlyPosition,
                OnlyObjects,
                All
            }

        #endregion

        #region Private Methods
            private void onSceneChanged(string sceneName) => SaveStatus();
    
        #endregion
        #region Unity API Methods
            private void Start()
            {
                if (status == null) return;
    
                RestoreStatus();
            }

            private void OnEnable() 
            {
                 if (autoSave != AutoSave.None)
                    GameEvents.Instance.OnSceneChanged += onSceneChanged;
            }
            private void OnDisable() 
            {
                GameEvents.Instance.OnSceneChanged -=  onSceneChanged;
            }
        #endregion

        #region Public Methods
            public void RestoreStatus()
            {
                if (player != null)
                    player.position = status.playerPosition;
    
    
                var enabledStatusList = status.enabledStatusList;
    
                int objectsCount = saveableGameObjects.Length;
                int componentsCount = saveableComponents.Length;
    
                if (enabledStatusList.Count < (objectsCount + componentsCount)) return;
    
                //Restoring "activeSelf" boolean for saveableGameObjects
                for (int i = 0; i < objectsCount;i++)
                {
                    saveableGameObjects[i].SetActive(enabledStatusList[i]);
                }
    
                //Restoring enabled boolean for saveableComponents
                for (int j = 0; j < componentsCount;j++)
                {
                    saveableComponents[j].enabled = enabledStatusList[objectsCount + j];
                }
    
    
            }
            public void SaveStatus()
            {
                switch (autoSave)
                {
                    case AutoSave.OnlyPosition:
                        SavePlayerPosition(player);
                        break;
                    case AutoSave.OnlyObjects:
                        SaveEnabledStatus();
                        break;
                    case AutoSave.All:
                        SaveEnabledStatus();
                        SavePlayerPosition(player);
                        break;
                }
                
            }
    
            public void ClearLevelStatus() 
            {
                 if (status != null) 
                    status.ClearLevelStatus();
            }
    
            public void SaveEnabledStatus()
            {
                var enabledStatusList = status.enabledStatusList;
    
                int objectsCount = saveableGameObjects.Length;
                int componentsCount = saveableComponents.Length;
    
                //Saving activeSelf boolean from each saveableGameObject
                for (int i = 0; i < objectsCount;i++)
                {
                    enabledStatusList[i] = saveableGameObjects[i].activeSelf;
                }
    
                //Saving enabled boolean from each saveableComponents
                for (int j = 0; j < componentsCount;j++)
                {
                    enabledStatusList[objectsCount + j] = saveableComponents[j].enabled;
                }
    
            }
            public void SavePlayerPosition(Transform tr) => status.playerPosition = tr.position;
        #endregion
    }
}
