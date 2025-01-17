using Scripts.Common;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(CharacterHitpoints))]
    [RequireComponent (typeof(CharacterStatus))]
    public class Health : MonoBehaviour
    {
        public event OnDamaged OnPlayerDamaged;
        public delegate void OnDamaged();

        public event OnDead OnPlayerDead;
        public delegate void OnDead();

        private PlayerController controller;
        private CharacterHitpoints hitpoints;
        private CharacterStatus status;

        public void Initialize(
            PlayerController playerController,
            CharacterHitpoints playerHitpoints,
            CharacterStatus playerStatus)
        {
            controller = playerController;
            hitpoints = playerHitpoints;
            status = playerStatus;

            if(hitpoints.GetCurrentHitPoints() <= hitpoints.GetMaxHitPoints())
                hitpoints.SetCurrentHitpoints(hitpoints.GetMaxHitPoints());
        }

        public void Damage(int dmgAmount = -1)
        {
            if (dmgAmount > 0)
                return;

            hitpoints.SumCurrentHitpoints(dmgAmount);
            OnPlayerDamaged?.Invoke();

            if (hitpoints.GetCurrentHitPoints() <= hitpoints.GetMinHitPoints())
                processDeath();
        }

        public void Regenerate(int healAmount = 1)
        {
            if (healAmount < 0)
                return;

            hitpoints.SumCurrentHitpoints(healAmount);
            OnPlayerDamaged?.Invoke();
        }

        private void processDeath()
        {
            OnPlayerDead?.Invoke();

            status.SetCanMoveStatus(false);

            controller.ExitCombat();

            status.SetIsDead(true);

            transform.parent.gameObject.SetActive(false);
        }
    }
}