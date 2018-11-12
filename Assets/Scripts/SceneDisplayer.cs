using GoogleARCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDisplayer : MonoBehaviour {

    [SerializeField] private Camera FirstPersonCamera;
    [SerializeField] private GameObject FarmScene;

    // The position of the touch 
    private Vector3 _clickPos;

    // Constant value to make sure the scene will be facing the user when placed
    private const float k_ModelRotation = 180.0f;

    // Use this for initialization
    void Start () {
        DisplaySceneEvent.RegisterListener(OnEventFired);

    }

    void OnDisable()
    {
        // We unregister the listener on disable
        DisplaySceneEvent.UnregisterListener(OnEventFired);
    }

    private void OnEventFired(DisplaySceneEvent info)
    {
        _clickPos = info.PlaceOfClick;
        Debug.Log("DisplayScene at: " + info.PlaceOfClick);
        // Look for a plane hitting the raycast from the touch point (TrackableHit, TrackableHitFlags, Frame.Raycast
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (Frame.Raycast(_clickPos.x, _clickPos.y, raycastFilter, out hit))
        {
            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                // Instanciate the model at the hit pose
                var farmObject = Instantiate(FarmScene, hit.Pose.position, hit.Pose.rotation);

                // Compensate for the hitPose rotation facing away from the raycast (ie camera)
                farmObject.transform.Rotate(0, k_ModelRotation, 0, Space.Self);

                // Create an anchor to allow ARCore to track the hitpoint as understanding to the physical world evolves
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                // Make the model a child of the anchor
                farmObject.transform.parent = anchor.transform;
            }
        }
    }
}
