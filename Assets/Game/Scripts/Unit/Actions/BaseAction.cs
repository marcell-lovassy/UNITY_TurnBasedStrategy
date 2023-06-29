using System;
using UnityEngine;

namespace Game.UnitSystem.Actions 
{
    [RequireComponent(typeof(Unit))]
    public abstract class BaseAction : MonoBehaviour
    {
        protected Action actionCompletedCallback;
        protected Unit unit;
        protected bool isActive;

        protected virtual void Awake()
        {
            unit = GetComponent<Unit>();
        }
    }
}