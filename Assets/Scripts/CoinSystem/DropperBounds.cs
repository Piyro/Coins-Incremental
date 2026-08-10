using UnityEngine;

namespace CoinTowerIdle.CoinSystem
{
    public class DropperBounds : MonoBehaviour
    {
        [Header("Movement Direction")]
        [SerializeField]
        private Transform movementDirection;

        [Header("Rail Limits")]
        [SerializeField]
        private float minDistance = -4f;

        [SerializeField]
        private float maxDistance = 4f;

        public Vector3 Direction
        {
            get
            {
                if (movementDirection != null)
                    return movementDirection.right.normalized;

                return Vector3.right;
            }
        }

        public Vector3 Origin
        {
            get
            {
                if (movementDirection != null)
                    return movementDirection.position;

                return transform.position;
            }
        }

        public float MinDistance => minDistance;

        public float MaxDistance => maxDistance;

        public float GetDistance(Vector3 position)
        {
            return Vector3.Dot(
                position - Origin,
                Direction);
        }

        public float ClampDistance(float distance)
        {
            return Mathf.Clamp(
                distance,
                minDistance,
                maxDistance);
        }

        public Vector3 GetPosition(float distance)
        {
            distance = ClampDistance(distance);

            return Origin + Direction * distance;
        }

        public Vector3 Clamp(Vector3 position)
        {
            float distance = GetDistance(position);

            return GetPosition(distance);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Vector3 origin = Origin;
            Vector3 direction = Direction;

            Vector3 left =
                origin + direction * minDistance;

            Vector3 right =
                origin + direction * maxDistance;

            Gizmos.color = Color.yellow;

            Gizmos.DrawLine(
                left,
                right);

            Gizmos.DrawSphere(
                left,
                0.15f);

            Gizmos.DrawSphere(
                right,
                0.15f);

            Gizmos.color = Color.cyan;

            Gizmos.DrawLine(
                origin,
                origin + direction * 1.5f);
        }
#endif
    }
}