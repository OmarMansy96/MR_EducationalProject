using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using System.Linq;

public class PlayerActions : NetworkBehaviour
{
    public GameObject canvas;
    private bool isCanvasVisible = false;
    public InputActionProperty menuButtonAction;

    [Header("Spawning Settings")]
    public InputActionProperty spawningTrigger;
    public int cubePrefabIndex = 0; // Index of cube prefab in NetworkManager's prefab list

    public override void OnNetworkSpawn()
    {
        HideControllers();
    }
    private void Start()
    {
        //if (!IsOwner)
        //    return;
        canvas = GameObject.FindGameObjectWithTag("ControllerUI").gameObject;
        canvas.transform.parent = GetComponentInChildren<ControllerInputActionManager>(true).transform;
        canvas.transform.position = canvas.transform.parent.position;
        //if (IsClient)
        //{
        canvas.SetActive(false);
        //}
    }

    private void Update()
    {
        if ((IsServer ||IsHost) && menuButtonAction.action.WasPressedThisFrame())
        {
            ToggleCanvas();
        }

        if (spawningTrigger.action.WasPressedThisFrame())
        {
            //SpawnCube();
        }
    }

    void ToggleCanvas()
    {
        isCanvasVisible = !isCanvasVisible;
        canvas.SetActive(isCanvasVisible);
    }

    void SpawnCube()
    {
        if (!IsOwner) return; // Only owner can request spawning

        if (IsServer)
        {
            SpawnCubeServerRpc(NetworkManager.LocalClientId);
        }
        else
        {
            SpawnCubeServerRpc(NetworkManager.LocalClientId);
        }
    }

    [ServerRpc]
    private void SpawnCubeServerRpc(ulong clientId)
    {
        // Get the prefab from NetworkManager
        var networkManager = NetworkManager.Singleton;
        if (networkManager == null || networkManager.NetworkConfig.Prefabs.Prefabs.Count <= cubePrefabIndex)
        {
            Debug.LogError("Prefab not found in NetworkManager");
            return;
        }

        var cubePrefab = networkManager.NetworkConfig.Prefabs.Prefabs[cubePrefabIndex].Prefab;

        // Spawn the cube
        GameObject cubeInstance = Instantiate(cubePrefab, transform.position + transform.forward * 7f, Quaternion.identity);
        NetworkObject networkObject = cubeInstance.GetComponent<NetworkObject>();
        networkObject.Spawn();

        // Optional: Give ownership to the player who spawned it
        networkObject.ChangeOwnership(clientId);
    }
    void HideControllers()
    {
        if (!IsServer||!IsHost)
        {
            ControllerInputActionManager[] controllersList = FindObjectsOfType<ControllerInputActionManager>();
            foreach (var item in controllersList)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}