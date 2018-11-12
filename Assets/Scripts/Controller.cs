using System;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

// This class is inspired by the HelloARController tutorial

public class Controller : MonoBehaviour {

    // Used to collect the frames from the camera hardware
    public Camera FirstPersonCamera;

    // A prefab for tracking and visualizing detected planes
    //public GameObject DetectedPlanePrefab;

    // A model to place when the user touches a plane or a feature point
    public GameObject FarmScene;

    // A gameObject parenting and controlling the display of the StatusBar
    public GameObject UI;

    // Constant value to make sure the scene will be facing the user when placed
    private const float k_ModelRotation = 180.0f;

    // A list of planes ARCore is tracking in the current frame
    private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();
    
    // Is true when the app is quitting due to an ARCore connection error
    private bool m_IsQuitting = false;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        
        _UpdateApplicationLifeCycle();

        Touch touch;

        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        } else { 

            // What follows only happens on the first frame of the touch
            // Look for a plane hitting the raycast from the touch point (TrackableHit, TrackableHitFlags, Frame.Raycast
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
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

    // Check and update the app lifecycle
    private void _UpdateApplicationLifeCycle()
    {
        // Exit the app when the back button is pressed
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Only allow the screen to sleep when not tracking
        if (Session.Status != SessionStatus.Tracking)
        {
            const int lostTrackingSleepTimeout = 15;
            Screen.sleepTimeout = lostTrackingSleepTimeout;
        }
        else
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        if (m_IsQuitting)
        {
            return;
        }

        // Quit if ARCore was unable to connect and give Unity some time for the toast to appear
        if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
        {
            _ShowAndroidToastMessage("Camera permission is needed to run this application.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
        else if (Session.Status.IsError())
        {
            _ShowAndroidToastMessage("ARCore encountered a problem connecting.  Please start the app again.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
    }

    // Quit the app
    private void _DoQuit()
    {
        Application.Quit();
    }

    // Show an android toast message
    private void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
                    message, 0);
                toastObject.Call("show");
            }));
        }
    }

}
