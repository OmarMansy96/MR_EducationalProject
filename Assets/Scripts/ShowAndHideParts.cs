using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System.Collections.Generic;
using System.Globalization;

public class ShowAndHideParts : NetworkBehaviour
{

    public GameObject part; // The part you want to show/hide

    private Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();

        // Only the owner should listen for toggle changes
        if (true)
        {
            toggle.onValueChanged.AddListener(OnToggleChanged);
        }
    }

    void OnToggleChanged(bool isOn)
    {
        // Send the toggle state to the server
        UpdateVisibilityServerRpc(isOn);
    }

    [ServerRpc]
    void UpdateVisibilityServerRpc(bool isVisible)
    {
        // Server tells everyone to update
        UpdateVisibilityClientRpc(isVisible);
    }

    [ClientRpc]
    void UpdateVisibilityClientRpc(bool isVisible)
    {
        // Apply visibility and sync toggle state on all clients
        part.SetActive(isVisible);

        if (!IsOwner)
        {
            toggle.isOn = isVisible;
        }
    }

}
