using UnityEngine;

public class VillageAudio : MonoBehaviour
{
    [Header("SFX")]
    public AudioClip villageClip;
    public float sfxVolumen;

    void Start()
    {
        AudioManager.Instance.PlayMusic(villageClip, true, sfxVolumen);
    }
}
