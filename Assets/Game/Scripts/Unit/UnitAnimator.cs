using Game.UnitSystem.Actions;
using UnityEngine;
using static Game.UnitSystem.Actions.ShootAction;

namespace Game.UnitSystem 
{
    public class UnitAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        [Space]
        [Header("Effect references")]
        [SerializeField]
        private BulletProjectile bulletProjectilePrefab;
        [SerializeField]
        private Transform shootPointTransform;

        private void Awake()
        {
            if(TryGetComponent(out MoveAction moveAction))
            {
                moveAction.OnStartMoving += MoveAction_OnStartMoving;
                moveAction.OnStopMoving += MoveAction_OnStopMoving;
            }

            if (TryGetComponent(out ShootAction shootAction))
            {
                shootAction.OnShoot += ShootAction_OnShoot;
            }
        }

        private void ShootAction_OnShoot(object sender, ShootEventArgs args)
        {
            animator.SetTrigger("Shoot");
            BulletProjectile bulletProjectile = Instantiate(bulletProjectilePrefab, shootPointTransform.position, Quaternion.identity);

            Vector3 targetUnitShootAtPosition = args.targetUnit.WorldPosition;
            targetUnitShootAtPosition.y = shootPointTransform.position.y;
            bulletProjectile.Setup(targetUnitShootAtPosition);
        }

        private void MoveAction_OnStopMoving()
        {
            animator.SetBool("IsMoving", false);
        }

        private void MoveAction_OnStartMoving()
        {
            animator.SetBool("IsMoving", true);
        }
    }
}