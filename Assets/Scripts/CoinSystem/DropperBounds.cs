using UnityEngine;

namespace CoinTowerIdle.CoinSystem
{
    public class DropperBounds : MonoBehaviour
    {
        [SerializeField]
        private float minX = -4f;

        [SerializeField]
        private float maxX = 4f;

        public float Clamp(float x)
        {
            return Mathf.Clamp(x, minX, maxX);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Vector3 left = new(minX, transform.position.y, transform.position.z);
            Vector3 right = new(maxX, transform.position.y, transform.position.z);

            Gizmos.DrawSphere(left, 0.15f);
            Gizmos.DrawSphere(right, 0.15f);
            Gizmos.DrawLine(left, right);
        }
#endif
    }
}