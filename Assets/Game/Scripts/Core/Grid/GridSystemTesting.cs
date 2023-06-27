using UnityEngine;

namespace Game.Core.Grid
{
    public class GridSystemTesting : MonoBehaviour
    {
        [SerializeField]
        private Transform gridDebugObjectPrefab;

        private GridSystem gridSystem;

        void Start()
        {
            gridSystem = new GridSystem(10, 10, 2f);
            gridSystem.CreateDebugObjects(gridDebugObjectPrefab);

        }

        //private void Update()
        //{
        //    Debug.Log(gridSystem.GetGridPosition(MouseWorldHelper.GetPosition()));
        //}
    }
}