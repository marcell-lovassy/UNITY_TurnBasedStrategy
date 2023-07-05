using System;
using UnityEngine;

namespace Game.UnitSystem
{
    public class RagdollSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform ragdollPrefab;
        [SerializeField]
        private Transform originalRootBone;

        private void Awake()
        {
            GetComponent<HealthSystem>().OnDead += HealthSystem_OnDead;
        }

        private void HealthSystem_OnDead()
        {
            SpawnRagdoll();
        }

        private void SpawnRagdoll()
        {
            Transform ragdollTransform = Instantiate(ragdollPrefab, transform.position, transform.rotation);
            var unitRagdoll = ragdollTransform.GetComponent<UnitRagdoll>();
            unitRagdoll.Setup(originalRootBone);
        }
    }
}