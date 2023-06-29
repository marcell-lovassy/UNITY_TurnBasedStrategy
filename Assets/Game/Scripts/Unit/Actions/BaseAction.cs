using Game.Core.Grid;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UnitSystem.Actions 
{
    [RequireComponent(typeof(Unit))]
    public abstract class BaseAction : MonoBehaviour
    {
        protected Action actionCompletedCallback;
        protected Unit unit;
        protected bool isActive;
        protected string actionName = "Action";

        protected virtual void Awake()
        {
            unit = GetComponent<Unit>();
        }

        public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

        public virtual bool IsVaidActionGridPosition(GridPosition gridPosition) 
        {
            var validGridPositionList = GetValidActionGridPositions();
            return validGridPositionList.Contains(gridPosition);
        }

        public abstract List<GridPosition> GetValidActionGridPositions();

        public abstract string GetActionName();
    }

}