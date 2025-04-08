
using UnityEngine;

using UnityEngine.InputSystem;

using Unity.Netcode;

public class PlayerActions : NetworkBehaviour
{
    //public GameObject objectToSpawn; // Prefab to spawn
    //public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rayInteractor; // XR Ray Interactor from the XR Controller
    //public InputActionProperty triggerAction; // XR Controller trigger input
    //public Transform spawnParent; // Optional parent for organization
    public GameObject canvas; // Assign the Canvas in Inspector
    private bool isCanvasVisible = false;

    public InputActionProperty menuButtonAction;
    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("ControllerUI").gameObject;
        if (!IsServer)
        {
            canvas.SetActive(false);

        }
        //canvas = FindFirstObjectByType<Canvas>().gameObject;
    }
    private void Update()
    {
        //if (rayInteractor == null || objectToSpawn == null || triggerAction.action == null)
        //    return;

        //if (triggerAction.action.WasPressedThisFrame()) // Detect trigger press
        //{
        //    TrySpawnObject();
        //}
        if (IsServer && menuButtonAction.action.WasPressedThisFrame())
        {
            ToggleCanvas();
        }
    }

    //private void TrySpawnObject()
    //{
    //    if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit)) // Check if ray hits a collider
    //    {
    //        if (hit.collider != null) // Ensure we hit an actual collider
    //        {
    //            Vector3 spawnPosition = hit.point;
    //            Quaternion spawnRotation = Quaternion.LookRotation(hit.normal); // Aligns with the hit surface

    //            Instantiate(objectToSpawn, spawnPosition, spawnRotation, spawnParent);
    //        }
    //    }
    //}

    void ToggleCanvas()
    {
        isCanvasVisible = !isCanvasVisible;
        canvas.SetActive(isCanvasVisible);
    }
}
