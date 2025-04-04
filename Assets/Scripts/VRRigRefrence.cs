using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class VRRigRefrence : NetworkBehaviour
{
    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public static VRRigRefrence singlton;

    public Transform[] playerPositions;
    int positionsIndex;

    private void Awake()
    {
        singlton = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = playerPositions[positionsIndex].position;
        positionsIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
