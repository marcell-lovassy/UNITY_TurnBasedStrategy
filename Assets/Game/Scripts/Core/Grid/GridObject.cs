using Game.UnitSystem;
using System.Collections.Generic;

namespace Game.Core.Grid
{
    public class GridObject
    {
        private GridPosition gridPosition;
        private GridSystem gridSystem;
        private List<Unit> unitList;

        public GridObject(GridSystem gridSystem, GridPosition gridPosition)
        {
            this.gridSystem = gridSystem;
            this.gridPosition = gridPosition;
            unitList = new List<Unit>();
        }

        public string GetDebugInfo()
        {
            string units = "";
            unitList.ForEach(unit => { units += unit.name + "\n"; });
            return units + gridPosition.ToString();
        }

        public void AddUnit(Unit unit)
        {
            unitList.Add(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            unitList.Remove(unit);
        }

        public List<Unit> GetUnitList()
        {
            return unitList;
        }

    }
}