//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Unity.Netcode;
//using UnityEngine.XR;

//public class SpawnTank : NetworkBehaviour
//{
//    public GameObject tankPrefab;
//    bool isInstantiate;
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)&& !isInstantiate)
//        {
//            isInstantiate = true;
//            GameObject newTank = Instantiate(tankPrefab,transform.position,Quaternion.identity);

//        }
//    }
//}
