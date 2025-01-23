using System;
using System.Collections.Generic;
using Scripts.Scriptables;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Scripts.Listener
{
    [Serializable]
    public class DialogueFilter 
    {
        public Dialogue KeyDialogue;
        public List<MessageFilter> MessageFilters;
        public UnityEvent OnDialogueStarted;
        public UnityEvent OnDialogueEnded;

    
    }
    [Serializable]
    public class MessageFilter
    {
        public int Index;
        public UnityEvent OnMessageShown;
    }
}