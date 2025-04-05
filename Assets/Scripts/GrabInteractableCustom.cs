//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;
//using UnityEngine.XR.Interaction.Toolkit.Interactables;

//public class GrabInteractableCustom : XRGrabInteractable
//{
//    public VRControllerManager controllerManager;

//    protected override void OnSelectEntered(SelectEnterEventArgs args)
//    {
//        base.OnSelectEntered(args);
//        // Tell the manager which object is currently grabbed.
//        if (controllerManager != null)
//        {
//            controllerManager.SetGrabbedObject(gameObject);
//        }
//    }

//    protected override void OnSelectExited(SelectExitEventArgs args)
//    {
//        base.OnSelectExited(args);
//        // Clear the grabbed object reference when released.
//        if (controllerManager != null)
//        {
//            controllerManager.ClearGrabbedObject();
//        }
//    }
//}
