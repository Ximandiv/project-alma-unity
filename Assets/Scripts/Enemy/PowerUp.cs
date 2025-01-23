using Scripts.Player;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Configuration")]
    public int powerType;
    public float increaseAmount;

    [Header("SFX")]
    public AudioClip sfxClip;
    public float sfxVolumen;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(sfxClip, sfxVolumen);
            switch (powerType)
            {
                case 1:
                    var movement = collision.GetComponent<Movement>();
                    movement.IncreaseSpeed(increaseAmount);
                    break;
                case 2:
                    //Llamar a la interfaz de poderes y luego aplicar el efecto sobre el player
                    break;

                case 3:
                    //Llamar a la interfaz de poderes y luego aplicar el efecto sobre el player
                    break;
            }

            Destroy(gameObject);
        }
    }
}
