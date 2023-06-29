using Game.UnitSystem;
using Game.UnitSystem.Actions;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Grid 
{
    public class GridSystemVisual : MonoBehaviour
    {
        public static GridSystemVisual Instance { get; private set; }

        [SerializeField]
        private Transform gridSystemVisualSinglePrefab;

        private GridSystemVisualSingle[,] gridCells;


        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one GridSystemVisual! " + transform + " - " + Instance);
                Destroy(gameObject);
            }
            Instance = this;
        }

        private void Start()
        {
            gridCells = new GridSystemVisualSingle[LevelGrid.Instance.Width, LevelGrid.Instance.Height];

            for(int x = 0; x < LevelGrid.Instance.Width; x++)
            {
                for(int z = 0; z < LevelGrid.Instance.Height; z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    Transform cell = Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity, transform);
                    gridCells[x, z] = cell.GetComponent<GridSystemVisualSingle>();
                }
            }
        }

        private void Update()
        {
            UpdateGridVisual();
        }

        public void HideAllGridPositions()
        {
            for (int x = 0; x < LevelGrid.Instance.Width; x++)
            {
                for (int z = 0; z < LevelGrid.Instance.Height; z++)
                {
                    gridCells[x, z].Hide();
                }
            }
        }

        public void ShowgridPositionList(List<GridPosition> gridPositions)
        {
            foreach (GridPosition gp in gridPositions)
            {
                gridCells[gp.x, gp.z].Show();
            }

        }

        private void UpdateGridVisual()
        {
            HideAllGridPositions();
            BaseAction selectedAction = UnitActionSystem.Instance.SelectedAction;
            ShowgridPositionList(selectedAction.GetValidActionGridPositions());
        }
    }
}