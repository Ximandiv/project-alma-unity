using UnityEngine;

namespace Scripts.Common
{
    public class CharacterStatus : MonoBehaviour
    {
        #region Public Methods

        public void SetMovingStatus(bool status) => isMoving = status;
        public void SetCanMoveStatus(bool status) => canMove = status;

        public bool IsMoving() => isMoving;
        public bool IsCapableOfMovement() => canMove;

        #endregion

        #region Private Variables

        [Header("Status")]

        [SerializeField] private bool isMoving = false;
        [SerializeField] private bool canMove = true;

        #endregion
    }
}