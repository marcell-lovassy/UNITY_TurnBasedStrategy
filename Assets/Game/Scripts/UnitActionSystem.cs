using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{

    [SerializeField]
    private Unit selectedUnit;
    [SerializeField]
    private LayerMask unitLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(HandleUnitSelection())
            {
                return;
            }
            SetUnitMovement();
        }
    }

    private bool HandleUnitSelection()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, unitLayer))
        {
            if(hitInfo.transform.TryGetComponent<Unit>(out Unit unit)) 
            {
                selectedUnit = unit;
                return true;
            }
        }
        return false;
    }

    private void SetUnitMovement() 
    {
        Vector3 target = MouseController.GetPosition();
        selectedUnit.Move(target);
    }
}
