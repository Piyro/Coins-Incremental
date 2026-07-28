using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioClip[] footstepClips;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float acceleration = 12f;

    [Header("Jumping / Gravity")]
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float groundedStickForce = -2f;

    [Header("Mouse Look")]
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float minPitch = -85f;
    [SerializeField] private float maxPitch = 85f;

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 5f;
    [SerializeField] private float staminaDrainRate = 1f;
    [SerializeField] private float staminaRegenRate = 0.75f;
    [SerializeField] private float staminaRegenDelay = 1f;
    private float currentStamina;
    private float staminaRegenTimer;

    [Header("Head Bob")]
    [SerializeField] private float bobFrequencyWalk = 8f;
    [SerializeField] private float bobFrequencySprint = 12f;
    [SerializeField] private float bobAmplitude = 0.05f;
    private float bobTimer;
    private Vector3 camDefaultLocalPos;

    [Header("Footsteps")]
    [SerializeField] private float footstepIntervalWalk = 0.5f;
    [SerializeField] private float footstepIntervalSprint = 0.3f;
    private float footstepTimer;

    private CharacterController controller;
    private Vector3 velocity;
    private float currentSpeed;
    private float pitch;
    private bool isSprinting;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentStamina = maxStamina;
        if (cameraHolder != null)
            camDefaultLocalPos = cameraHolder.localPosition;
    }

    private void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleHeadBobAndFootsteps();
    }

    private void HandleMouseLook()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float mouseX = mouseDelta.x * mouseSensitivity;
        float mouseY = mouseDelta.y * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        cameraHolder.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private void HandleMovement()
    {
        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0f)
            velocity.y = groundedStickForce;

        Keyboard kb = Keyboard.current;

        float x = 0f, z = 0f;
        if (kb.dKey.isPressed) x += 1f;
        if (kb.aKey.isPressed) x -= 1f;
        if (kb.wKey.isPressed) z += 1f;
        if (kb.sKey.isPressed) z -= 1f;

        Vector3 inputDir = (transform.right * x + transform.forward * z).normalized;

        bool wantsSprint = kb.leftShiftKey.isPressed && z > 0f;
        isSprinting = wantsSprint && currentStamina > 0f;

        float targetSpeed = isSprinting ? sprintSpeed : walkSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        Vector3 move = inputDir * currentSpeed;

        if (kb.spaceKey.wasPressedThisFrame && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move((move + Vector3.up * velocity.y) * Time.deltaTime);

        HandleStamina(isSprinting, inputDir.sqrMagnitude > 0f);
    }

    private void HandleStamina(bool sprinting, bool moving)
    {
        if (sprinting && moving)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Max(currentStamina, 0f);
            staminaRegenTimer = staminaRegenDelay;
        }
        else
        {
            if (staminaRegenTimer > 0f)
            {
                staminaRegenTimer -= Time.deltaTime;
            }
            else
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
                currentStamina = Mathf.Min(currentStamina, maxStamina);
            }
        }
    }

    private void HandleHeadBobAndFootsteps()
    {
        bool isGrounded = controller.isGrounded;
        Vector3 horizontalVel = new Vector3(controller.velocity.x, 0f, controller.velocity.z);
        bool isMoving = horizontalVel.magnitude > 0.1f && isGrounded;

        if (isMoving)
        {
            float frequency = isSprinting ? bobFrequencySprint : bobFrequencyWalk;
            bobTimer += Time.deltaTime * frequency;

            float bobOffsetY = Mathf.Sin(bobTimer) * bobAmplitude;
            float bobOffsetX = Mathf.Cos(bobTimer * 0.5f) * bobAmplitude * 0.5f;

            cameraHolder.localPosition = camDefaultLocalPos + new Vector3(bobOffsetX, bobOffsetY, 0f);

            float interval = isSprinting ? footstepIntervalSprint : footstepIntervalWalk;
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= interval)
            {
                footstepTimer = 0f;
                PlayFootstep();
            }
        }
        else
        {
            bobTimer = 0f;
            cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, camDefaultLocalPos, Time.deltaTime * 8f);
            footstepTimer = footstepIntervalWalk;
        }
    }

    private void PlayFootstep()
    {
        if (footstepSource == null || footstepClips == null || footstepClips.Length == 0)
            return;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        footstepSource.PlayOneShot(clip);
    }

    public float GetStaminaNormalized() => currentStamina / maxStamina;
}