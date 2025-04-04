
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public GameObject objectToSpawn; // Prefab to spawn
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rayInteractor; // XR Ray Interactor from the XR Controller
    public InputActionProperty triggerAction; // XR Controller trigger input
    public Transform spawnParent; // Optional parent for organization

    private void Update()
    {
        if (rayInteractor == null || objectToSpawn == null || triggerAction.action == null)
            return;

        if (triggerAction.action.WasPressedThisFrame()) // Detect trigger press
        {
            TrySpawnObject();
        }
    }

    private void TrySpawnObject()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit)) // Check if ray hits a collider
        {
            if (hit.collider != null) // Ensure we hit an actual collider
            {
                Vector3 spawnPosition = hit.point;
                Quaternion spawnRotation = Quaternion.LookRotation(hit.normal); // Aligns with the hit surface

                Instantiate(objectToSpawn, spawnPosition, spawnRotation, spawnParent);
            }
        }
    }
}
