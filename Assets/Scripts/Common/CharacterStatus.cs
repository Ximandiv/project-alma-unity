using Scripts.Scriptables;
using UnityEngine;

namespace Scripts.Common
{
    public class CharacterStatus : MonoBehaviour
    {
        #region Public Methods

        public void SetMovingStatus(bool status) => characterStats.IsMoving = status;
        public void SetCanMoveStatus(bool status) => characterStats.CanMove = status;

        public bool IsMoving() => characterStats.IsMoving;
        public bool IsCapableOfMovement() => characterStats.CanMove;

        #endregion

        #region Private Variables

        [SerializeField] private CharacterStats characterStats;

        #endregion
    }
}