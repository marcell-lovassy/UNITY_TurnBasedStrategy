using Game.UnitSystem.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UnitSystem 
{
    public class UnitAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

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

        private void ShootAction_OnShoot()
        {
            animator.SetTrigger("Shoot");
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