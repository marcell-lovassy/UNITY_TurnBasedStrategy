using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private static MouseController instance;

    [SerializeField]
    LayerMask hitableLayers;

    private void Awake()
    {
        instance = this;
    }

    //private void Update()
    //{
    //    transform.position = MouseController.GetPosition();
    //}

    public static Vector3 GetPosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, instance.hitableLayers);
        return hitInfo.point;
    }
}
