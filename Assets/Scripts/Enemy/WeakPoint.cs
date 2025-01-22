using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    public string color;

    public void TakeDamage(string colorDano)
    {
        if (color == colorDano)
        {
            this.gameObject.SetActive(false);
        }
    }
}
