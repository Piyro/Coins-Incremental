using System;
using UnityEngine;

namespace CoinTowerIdle.CoinSystem
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Coin : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody rb;

        [Header("Visual")]
        [SerializeField] private MeshRenderer meshRenderer;

        private float lifeTimer;

        public Rigidbody Rigidbody => rb;

        public double Value { get; private set; }

        public CoinType Type { get; private set; }

        public bool IsActive { get; private set; }


        private void Reset()
        {
            rb = GetComponent<Rigidbody>();
            meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        public void Initialize(CoinData data)
        {
            Value = data.Value;
            Type = data.Type;

            lifeTimer = data.Lifetime;

            rb.mass = data.Mass;

            IsActive = true;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }


        private void Update()
        {
            if (!IsActive)
                return;

            lifeTimer -= Time.deltaTime;

            if (lifeTimer <= 0f)
            {
                Destroy(gameObject);
            }
        }

        public void Launch(Vector3 position, Vector3 impulse)
        {
            transform.position = position;

            rb.WakeUp();

            rb.AddForce(impulse, ForceMode.Impulse);
        }
        public void ReturnToPool()
        {
            IsActive = false;
            Destroy(gameObject);
        }
    }
}