using CoinTowerIdle.Stats;
using UnityEngine;

namespace CoinTowerIdle.CoinSystem
{
    [RequireComponent(typeof(DropperInput))]
    [RequireComponent(typeof(DropperBounds))]
    public class DropperController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        private bool useMouse = true;

        [SerializeField]
        private float mouseFollowSpeed = 12f;

        [SerializeField]
        private float keyboardMoveSpeed = 1f;

        [Header("References")]
        [SerializeField]
        private DropperInput input;

        [SerializeField]
        private DropperBounds bounds;

        private float MoveSpeed
        {
            get
            {
                if (StatManager.Instance == null)
                    return 1f;

                return StatManager.Instance.GetValue(
                    StatType.MovementSpeed);
            }
        }

        private void Awake()
        {
            if (input == null)
                input = GetComponent<DropperInput>();

            if (bounds == null)
                bounds = GetComponent<DropperBounds>();
        }

        private void Update()
        {
            if (input == null ||
                bounds == null)
            {
                return;
            }

            if (useMouse)
            {
                HandleMouseMovement();
            }
            else
            {
                HandleKeyboardMovement();
            }
        }

        private void HandleMouseMovement()
        {
            if (!input.MouseAvailable)
            {
                HandleKeyboardMovement();
                return;
            }

            float targetDistance =
                bounds.GetDistance(
                    input.MouseWorld);

            targetDistance =
                bounds.ClampDistance(
                    targetDistance);

            float currentDistance =
                bounds.GetDistance(
                    transform.position);

            float smoothSpeed =
                mouseFollowSpeed * MoveSpeed;

            float newDistance =
                Mathf.Lerp(
                    currentDistance,
                    targetDistance,
                    1f - Mathf.Exp(
                        -smoothSpeed *
                        Time.deltaTime));

            transform.position =
                bounds.GetPosition(
                    newDistance);
        }

        private void HandleKeyboardMovement()
        {
            float inputValue =
                input.Horizontal;

            if (Mathf.Approximately(
                inputValue,
                0f))
            {
                return;
            }

            float currentDistance =
                bounds.GetDistance(
                    transform.position);

            float newDistance =
                currentDistance +
                inputValue *
                MoveSpeed *
                keyboardMoveSpeed *
                Time.deltaTime;

            newDistance =
                bounds.ClampDistance(
                    newDistance);

            transform.position =
                bounds.GetPosition(
                    newDistance);
        }
    }
}