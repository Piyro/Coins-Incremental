using CoinTowerIdle.CoinSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArcadeMachine : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private FirstPersonController player;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform cameraHolder;

    [Header("Machine")]
    [SerializeField] private Transform cameraPoint;
    [SerializeField] private GameObject machineUI;
    [SerializeField] private DropperController dropper;

    [Header("Transition")]
    [SerializeField] private float transitionTime = 0.5f;

    private Vector3 originalCameraPos;
    private Quaternion originalCameraRot;

    private bool playerInside;
    private bool playing;
    private bool transitioning;

    private void Start()
    {
        machineUI.SetActive(false);
        dropper.enabled = false;
    }

    private void Update()
    {
        if (transitioning)
            return;

        if (!playing)
        {
            if (playerInside &&
                Keyboard.current.eKey.wasPressedThisFrame)
            {
                StartCoroutine(EnterMachine());
            }
        }
        else
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                StartCoroutine(ExitMachine());
            }
        }
    }

    private IEnumerator EnterMachine()
    {
        transitioning = true;
        playing = true;

        originalCameraPos = cameraHolder.position;
        originalCameraRot = cameraHolder.rotation;

        player.enabled = false;
        characterController.enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / transitionTime;

            cameraHolder.position = Vector3.Lerp(
                originalCameraPos,
                cameraPoint.position,
                t);

            cameraHolder.rotation = Quaternion.Slerp(
                originalCameraRot,
                cameraPoint.rotation,
                t);

            yield return null;
        }

        machineUI.SetActive(true);
        dropper.enabled = true;

        transitioning = false;
    }

    private IEnumerator ExitMachine()
    {
        transitioning = true;

        machineUI.SetActive(false);
        dropper.enabled = false;

        Vector3 startPos = cameraHolder.position;
        Quaternion startRot = cameraHolder.rotation;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / transitionTime;

            cameraHolder.position = Vector3.Lerp(
                startPos,
                originalCameraPos,
                t);

            cameraHolder.rotation = Quaternion.Slerp(
                startRot,
                originalCameraRot,
                t);

            yield return null;
        }

        player.enabled = true;
        characterController.enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playing = false;
        transitioning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FirstPersonController>() != null)
            playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FirstPersonController>() != null)
            playerInside = false;
    }
}