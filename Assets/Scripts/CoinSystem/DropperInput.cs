using UnityEngine;
using UnityEngine.InputSystem;

namespace CoinTowerIdle.CoinSystem
{
    public class DropperInput : MonoBehaviour
    {
        public float Horizontal { get; private set; }

        public Vector3 MouseWorld { get; private set; }

        [SerializeField]
        private Camera mainCamera;

        [SerializeField]
        private LayerMask groundMask;

        private void Awake()
        {
            if (mainCamera == null)
                mainCamera = Camera.main;
        }

        private void Update()
        {
            Horizontal = 0f;

            if (Keyboard.current != null)
            {
                if (Keyboard.current.aKey.isPressed ||
                    Keyboard.current.leftArrowKey.isPressed)
                {
                    Horizontal = -1f;
                }

                if (Keyboard.current.dKey.isPressed ||
                    Keyboard.current.rightArrowKey.isPressed)
                {
                    Horizontal = 1f;
                }
            }

            if (Mouse.current == null || mainCamera == null)
                return;

            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            // 1. Try your original physics raycast setup
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundMask))
            {
                MouseWorld = hit.point;
            }
            else
            {
                // 2. FIX/FALLBACK: If the physics raycast misses or layers aren't configured,
                // project the mouse ray onto an invisible mathematical plane passing through this dropper.
                // This makes the mouse system bulletproof without needing physical colliders.
                Plane fallbackPlane = new Plane(-mainCamera.transform.forward, transform.position);

                if (fallbackPlane.Raycast(ray, out float distance))
                {
                    MouseWorld = ray.GetPoint(distance);
                }
            }
        }
    }
}