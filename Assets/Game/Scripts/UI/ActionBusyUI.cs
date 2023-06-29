using Game.UnitSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.UI 
{
    public class ActionBusyUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject busyUI;

        private void Start()
        {
            busyUI.SetActive(false);
            UnitActionSystem.Instance.OnBusyChanged += UpdateBusyUI;
        }

        private void UpdateBusyUI(bool isBusy)
        {
            busyUI.SetActive(isBusy);
        }
    }
}