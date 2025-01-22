using UnityEngine;

namespace Scripts.Player { 
    public class AnimationEvents : MonoBehaviour
    {
        [Header("SFX")]
        [SerializeField] private AudioClip stepClip;
        [SerializeField] private float stepSFXVolumen = 1f;

        public void PlayStep() {
            AudioManager.Instance.PlaySFX(stepClip, stepSFXVolumen);
        }
        
    }
}
