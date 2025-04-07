using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
public class PlayerController : NetworkBehaviour
{
    public Transform[] playerPositions;

    public override void OnNetworkSpawn()
    {
        playerPositions = GameObject.FindGameObjectWithTag("PlayerPositions").GetComponentsInChildren<Transform>();


    }
    void Start()
    {
        if (!IsOwner)
        {
            return;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerPositions[NetworkManager.Singleton.ConnectedClients.Count].transform.position;
    }

    void Update()
    {
        
    }
}
