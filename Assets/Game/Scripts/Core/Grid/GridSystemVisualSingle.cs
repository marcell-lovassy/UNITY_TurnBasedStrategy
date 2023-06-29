using UnityEngine;

namespace Game.Core.Grid 
{
    [RequireComponent(typeof(MeshRenderer))]
    public class GridSystemVisualSingle : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer meshRenderer;

        public void Show()
        {
            meshRenderer.enabled = true;
        }

        public void Hide()
        {
            meshRenderer.enabled = false;
        }
    }
}