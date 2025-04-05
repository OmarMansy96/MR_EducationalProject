using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class VRControllerManager : MonoBehaviour
{
    public XRRayInteractor rightHandRay;
    public InputActionProperty spawnButton;
    public GameObject objectToSpawn;
    public Transform spawnPoint;

    public InputActionProperty menuButton;
    public GameObject canvas;

    public XRGrabInteractable grabbedObject;
    public InputActionProperty rotateJoystick;

    private GameObject currentSpawnedObject;
    private bool isCanvasVisible = false;

    void Update()
    {
        // Spawn Object at Ray End
        if (spawnButton.action.WasPressedThisFrame())
        {
            if (rightHandRay.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                if (currentSpawnedObject == null)
                {
                    currentSpawnedObject = Instantiate(objectToSpawn, hit.point, Quaternion.identity);
                }
                else
                {
                    currentSpawnedObject.transform.position = hit.point;
                }
            }
        }

        // Toggle Canvas Visibility
        if (menuButton.action.WasPressedThisFrame())
        {
            isCanvasVisible = !isCanvasVisible;
            canvas.SetActive(isCanvasVisible);
        }

        // Rotate Grabbed Object using Joystick
        if (grabbedObject && grabbedObject.isSelected)
        {
            Vector2 rotationInput = rotateJoystick.action.ReadValue<Vector2>();
            grabbedObject.transform.Rotate(Vector3.up, rotationInput.x * 50f * Time.deltaTime, Space.World);
        }
    }
}
