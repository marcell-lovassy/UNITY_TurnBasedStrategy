using UnityEngine;

namespace Game.Core
{
    public class MouseWorldHelper : MonoBehaviour
    {
        private static MouseWorldHelper instance;

        [SerializeField]
        LayerMask hitableLayers;

        private void Awake()
        {
            instance = this;
        }

        public static Vector3 GetPosition()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, instance.hitableLayers);
            return hitInfo.point;
        }
    }
}
