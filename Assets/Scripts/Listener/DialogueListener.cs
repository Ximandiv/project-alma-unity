using System;
using System.Collections.Generic;
using Scripts.Events;
using Scripts.Scriptables;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Listener
{
    public class DialogueListener : MonoBehaviour 
    {
        #region Private Variables
            [SerializeField] private List<DialogueFilter> filters = new();
            private Dictionary<int, DialogueFilter> activeDialogueFilters;
            private Dictionary<string, MessageFilter> activeMessageFilters; 
    
        #endregion
        #region Monobehaviour implementation
         private void Start()
         {
             if (filters.Count == 0)
                 return;
         
             activeDialogueFilters = new Dictionary<int, DialogueFilter>();
             activeMessageFilters = new Dictionary<string,MessageFilter>();
 
             foreach (DialogueFilter filter in filters)
             {
                 //Register dialogue filters for listen dialogue started and dialogue ended events
                 if (filter.KeyDialogue == null) continue;
 
                 int dialogueInstanceID = filter.KeyDialogue.GetInstanceID();
 
                 if (!activeDialogueFilters.TryAdd(dialogueInstanceID,filter))
                 {
                     Debug.LogWarning("There is already a reference for this key dialogue.");
                     continue;
                 }
 
                 //Register message filters by dialogue if the current one has them
                 if (filter.MessageFilters == null || filter.MessageFilters.Count == 0) continue;
             
                 foreach (MessageFilter messageFilter in filter.MessageFilters)
                 {
                     int messageIndex = messageFilter.Index;
 
                     //Don't add message filters that points an out-of-range index.
                     if (messageIndex <= 0 || messageIndex >= filter.KeyDialogue.Messages.Length)
                     {
                         Debug.LogWarning("A Message filter has a index that is out-of-range.");
                         continue;
                     }
 
                     string messageFilterKey = $"{dialogueInstanceID}.{messageIndex}";
 
                     if (!activeMessageFilters.TryAdd(messageFilterKey,messageFilter))
                         Debug.LogWarning("Cannot register this message filter because already exists.");
                 }
 
             }
 
             //Subscribing methods to singleton
             GameEvents.Instance.OnDialogueStarted += onDialogueStarted;
             GameEvents.Instance.OnDialogueAdvance += onDialogueAdvance;
             GameEvents.Instance.OnDialogueEnded += onDialogueEnded;
         }
       #endregion

        #region Private Methods
            private void onDialogueAdvance(Dialogue dialogue, int messageIndex)
            {
                int dialogueInstanceID = dialogue.GetInstanceID();
                string messageFilterKey = $"{dialogueInstanceID}.{messageIndex}";
    
                if (activeMessageFilters.TryGetValue(messageFilterKey,out var matchedMessageFilter))
                    matchedMessageFilter.OnMessageShown?.Invoke();
            }
    
            private void onDialogueStarted(Dialogue dialogue)
            {
                int dialogueInstanceID = dialogue.GetInstanceID();
    
                if (activeDialogueFilters.TryGetValue(dialogueInstanceID,out var matchedFilter))
                    matchedFilter.OnDialogueStarted?.Invoke();
    
            }
    
            private void onDialogueEnded(Dialogue dialogue)
            {
                int dialogueInstanceID = dialogue.GetInstanceID();
    
                if (activeDialogueFilters.TryGetValue(dialogueInstanceID,out var matchedFilter))
                    matchedFilter.OnDialogueEnded?.Invoke();
            }
        #endregion
    }
}