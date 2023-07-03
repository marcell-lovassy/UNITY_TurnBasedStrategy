using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UnitSystem.Enemy 
{
    public class EnemyAI : MonoBehaviour
    {
        private float timer;

        private void Start()
        {
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChenged;
        }

        private void Update()
        {
            if (TurnSystem.Instance.IsPlayerTurn) return;

            timer -= Time.deltaTime;

            if(timer <= 0f) 
            {
                TurnSystem.Instance.NextTurn();
            }

        }

        private void TurnSystem_OnTurnChenged()
        {
            timer = 3f;
        }
    }
}