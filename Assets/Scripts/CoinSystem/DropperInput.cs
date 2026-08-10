using UnityEngine;
using UnityEngine.InputSystem;

namespace CoinTowerIdle.CoinSystem
{
    public class DropperInput : MonoBehaviour
    {
        public float Horizontal { get; private set; }

        public Vector3 MouseWorld { get; private set; }

        public bool MouseAvailable =>
            Mouse.current != null;

        [SerializeField]
        private Camera mainCamera;

        private void Awake()
        {
            if (mainCamera == null)
                mainCamera = Camera.main;

            if (mainCamera == null)
            {
                Debug.LogError(
                    "DropperInput: No camera found.",
                    this);
            }
        }

        private void Update()
        {
            ReadKeyboard();
            ReadMouse();
        }

        private void ReadKeyboard()
        {
            Horizontal = 0f;

            if (Keyboard.current == null)
                return;

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

        private void ReadMouse()
        {
            if (Mouse.current == null ||
                mainCamera == null)
            {
                return;
            }

            Vector2 mousePosition =
                Mouse.current.position.ReadValue();

            Ray ray =
                mainCamera.ScreenPointToRay(mousePosition);

            // Create a plane facing the camera.
            // The plane passes through the dropper.
            Plane plane = new Plane(
                -mainCamera.transform.forward,
                transform.position);

            if (plane.Raycast(ray, out float distance))
            {
                MouseWorld = ray.GetPoint(distance);
            }
        }
    }
}