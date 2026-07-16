using CoinTowerIdle.Stats;
using UnityEngine;
using UnityEngine.InputSystem; // Added: Required for New Input System mouse tracking

namespace CoinTowerIdle.CoinSystem
{
    [RequireComponent(typeof(DropperInput))]
    [RequireComponent(typeof(DropperBounds))]
    public class DropperController : MonoBehaviour
    {
        [Header("Movement")]

        private float MoveSpeed =>
    StatManager.Instance.GetValue(StatType.MovementSpeed);

        [SerializeField]
        private bool useMouse = true;

        [SerializeField]
        private float mouseFollowSpeed = 18f;

        private DropperInput input;
        private DropperBounds bounds;
        private Camera mainCamera; // Added: Cached camera reference for performance

        private void Awake()
        {
            input = GetComponent<DropperInput>();
            bounds = GetComponent<DropperBounds>();
            mainCamera = Camera.main; // Caches the main camera
        }

        private void Update()
        {
            Vector3 position = transform.position;

            if (useMouse)
            {
                // FIX: Ensure both the keyboard/mouse hardware and the camera exist
                if (Mouse.current != null && mainCamera != null)
                {
                    // 1. Get raw screen pixel coordinates from the mouse
                    Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

                    // 2. CRITICAL: Calculate the exact distance between the camera and this dropper.
                    // This gives ScreenToWorldPoint the depth it needs to calculate world math accurately.
                    float cameraDepthOffset = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);

                    // 3. Convert screen pixels to actual 3D space vectors
                    Vector3 mouseWorldCoordinates = mainCamera.ScreenToWorldPoint(new Vector3(
                        mouseScreenPosition.x,
                        mouseScreenPosition.y,
                        cameraDepthOffset
                    ));

                    // 4. Smoothly interpolate to the newly calculated X axis coordinate
                    position.x = Mathf.Lerp(
                        position.x,
                        mouseWorldCoordinates.x,
                        mouseFollowSpeed * Time.deltaTime);
                }
            }
            else
            {
                position.x +=
                    input.Horizontal *
                    MoveSpeed *
                    Time.deltaTime;
            }

            position.x = bounds.Clamp(position.x);

            transform.position = position;
        }

    }
}