using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField]
    private Unit unit;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();    
    }

    void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += SelectedUnitChanged;
        UpdateVisual();
    }

    private void SelectedUnitChanged()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        meshRenderer.enabled = UnitActionSystem.Instance.SelectedUnit == unit;
    }
}
