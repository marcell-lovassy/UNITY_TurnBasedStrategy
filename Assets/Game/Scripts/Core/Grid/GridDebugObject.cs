using TMPro;
using UnityEngine;

namespace Game.Core.Grid 
{
    public class GridDebugObject : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text gridText;

        private GridObject gridObject;

        public void SetGridObject(GridObject gridObject)
        {
            this.gridObject = gridObject;
            //gridText.text = gridObject.GetDebugCoordinates();
        }

        private void Update()
        {
            gridText.text = gridObject.GetDebugInfo();
        }
    }
}