using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UnitSystem;

namespace Game.Core.Grid
{
    public class LevelGrid : MonoBehaviour
    {
        public static LevelGrid Instance { get; private set; }

        public int Width => width;
        public int Height => height;
        public float CellSize => cellSize;

        [SerializeField]
        private Transform gridDebugObjectPrefab;
        [SerializeField]
        private int width;
        [SerializeField]
        private int height;
        [SerializeField]
        private float cellSize;


        private GridSystem gridSystem;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one LevelGrid! " + transform + " - " + Instance);
                Destroy(gameObject);
            }
            Instance = this;

            gridSystem = new GridSystem(width, height, cellSize);
            gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
        }

        public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
        {
            gridSystem.GetGridObject(gridPosition).AddUnit(unit);
        }

        public IReadOnlyCollection<Unit> GetUnitsAtGridPosition(GridPosition gridPosition) 
        {
            return gridSystem.GetGridObject(gridPosition).GetUnitList();
        }

        public void ClearUnitAtGridPosition(Unit unit, GridPosition gridPosition)
        {
            gridSystem.GetGridObject(gridPosition).RemoveUnit(unit);
        }


        public int GetWidth() => gridSystem.Width;
        public int GetHeight() => gridSystem.Height;
        public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);
        public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);
        public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

        public bool HasUnitOnGridPosition(GridPosition gridPosition) 
        {
            return gridSystem.GetGridObject(gridPosition).HasAnyUnit();
        }

        public void UnitMovedGridPosition(Unit unit, GridPosition from, GridPosition to) 
        {
            ClearUnitAtGridPosition(unit, from);
            AddUnitAtGridPosition(to, unit);
        }
    }
}