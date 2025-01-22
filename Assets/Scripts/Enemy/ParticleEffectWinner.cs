using UnityEngine;

public class ParticleEffectWinner : MonoBehaviour
{
    [Header("Particle System")]
    [SerializeField] private GameObject Particles1;
    [SerializeField] private GameObject Particles2;

    [Header("SFX")]
    public AudioClip winnerClip;
    public float sfxVolumen;

    public void SpawnWinnerParticle(Vector2 position) 
    {
        AudioManager.Instance.PlayMusic(winnerClip, true, sfxVolumen);

        GameObject particles = Instantiate(Particles1, position, Quaternion.identity);
        float particleDuration = particles.GetComponent<ParticleSystem>().main.duration;
        Destroy(particles, particleDuration);

        GameObject particles2 = Instantiate(Particles2, position, Quaternion.identity);
    }
}
