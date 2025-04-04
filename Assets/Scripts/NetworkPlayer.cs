using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkPlayer : NetworkBehaviour
{

    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public Renderer[] meshToDisable;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            foreach (var item in meshToDisable)
            {
                item.enabled = false;
            }
        }
    }
   
    // Start is called before the first frame update
    void Start()
    {
       
         
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            root.position = VRRigRefrence.singlton.root.position;
            root.rotation = VRRigRefrence.singlton.root.rotation;

            head.position = VRRigRefrence.singlton.head.position;
            head.rotation = VRRigRefrence.singlton.head.rotation;

            leftHand.position = VRRigRefrence.singlton.leftHand.position;
            leftHand.rotation = VRRigRefrence.singlton.leftHand.rotation;

            rightHand.position = VRRigRefrence.singlton.rightHand.position;
            rightHand.rotation = VRRigRefrence.singlton.rightHand.rotation;
        }
       
    }
}
