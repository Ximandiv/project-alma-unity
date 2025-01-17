using UnityEngine;

namespace Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "Character Stats", menuName = "Scriptables/CharacterStats")]
    public class CharacterStats : ScriptableObject
    {
        #region Speed

        [Header("Speed")]

        [SerializeField] private float currentSpeed = 5f;
        public float CurrentSpeed
        {
            get { return currentSpeed; }
            set { currentSpeed = value; }
        }

        [SerializeField] private float minSpeed = 1f;
        public float MinSpeed { get { return minSpeed; } }

        [SerializeField] private float maxSpeed = 10f;
        public float MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        #endregion

        #region Hitpoints

        [Header("Hitpoints")]

        [SerializeField] private int currentHitPoints = 2;
        public int CurrentHitPoints
        {
            get { return currentHitPoints; }
            set { currentHitPoints = value; }
        }

        [SerializeField] private int minHitPoints = 1;
        public int MinHitPoints { get { return minHitPoints; } }

        [SerializeField] private int maxHitPoints = 2;
        public int MaxHitPoints
        {
            get { return maxHitPoints; }
            set { maxHitPoints = value; }
        }

        #endregion

        [Header("Status")]

        [SerializeField] private bool isMoving = false;
        public bool IsMoving
        {
            get { return isMoving; }
            set { isMoving = value; }
        }

        [SerializeField] private bool canMove = true;
        public bool CanMove
        {
            get { return canMove; }
            set { canMove = value; }
        }

        [SerializeField] private bool isDead = false;
        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }
    }
}