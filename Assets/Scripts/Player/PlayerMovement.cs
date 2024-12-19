using Scripts.Common;
using UnityEngine;

[RequireComponent(typeof(CharacterVariables))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private CharacterVariables characterVariables;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 movement = Vector2.zero;
    private Vector2 lastDirection;

    #endregion

    #region Unity API Methods

    private void Awake()
    {
        characterVariables = GetComponent<CharacterVariables>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (characterVariables.IsCapableOfMovement())
        {
            getInputMovement();

            characterVariables.SetMovingStatus(isMoving());
        }
        else
            characterVariables.SetMovingStatus(false);
    }

    private void FixedUpdate()
    {
        if (characterVariables.IsCapableOfMovement())
            move();
    }

    #endregion

    #region Private Methods

    private bool isMoving() => (movement.normalized).sqrMagnitude > 0.01f;

    private void getInputMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void move()
    {
        Vector2 normalizedMovement = movement.normalized;

        rb.linearVelocity = normalizedMovement * characterVariables.GetCurrentSpeed();

        if (isMoving())
        {
            lastDirection = normalizedMovement;
            rotate();
        }
    }

    private void rotate()
    {
        float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;

        rb.rotation = angle;
    }

    #endregion
}
