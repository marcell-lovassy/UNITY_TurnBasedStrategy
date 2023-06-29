using System;
using UnityEngine;

namespace Game.UnitSystem.Actions 
{
    public class SpinAction : BaseAction
    {
        private float totalSpinDegrees;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            if (!isActive) return;
                
            float spinDegrees = 360f * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, spinDegrees, 0);

            totalSpinDegrees += spinDegrees;

            if(totalSpinDegrees >= 360f)
            {
                isActive = false;
                actionCompletedCallback?.Invoke();
            }
        }

        public void Spin(Action onSpinComplete)
        {
            actionCompletedCallback = onSpinComplete;
            totalSpinDegrees = 0;
            isActive = true;
        }
    }
}