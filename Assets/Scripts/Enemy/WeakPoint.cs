using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    public string color;
    public Transform parentObject;
    public float orbitSpeed = 10f;
    public Vector3 rotationAxis = Vector3.up;

    void Update()
    {
        if (parentObject != null)
        {
            // Rotates around the parent object
            transform.RotateAround(parentObject.position, rotationAxis, orbitSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(string colorDano)
    {
        if (color == colorDano)
        {
            this.gameObject.SetActive(false);
        }
    }
}
