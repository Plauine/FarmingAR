using System;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

// This class is inspired by the HelloARController tutorial

public class Controller : MonoBehaviour {

    // Used to collect the frames from the camera hardware
    public Camera FirstPersonCamera;

    // A prefab for tracking and visualizing detected planes
    public GameObject DetectedPlanePrefab;

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
	void Update () {
        _UpdateApplicationLifeCycle();

        // Hide the StatusBar when currently tracking at least one plane
        // 1. Get all the tracked planes in m_AllPlanes
        Session.GetTrackables<DetectedPlane>(m_AllPlanes);

        // DisplayStatusBar indicated if we must display the status bar or not
        bool DisplayStatusBar = false;
        if(m_AllPlanes.Count == 0)
        {
            DisplayStatusBar = true;
        }

        UI.SetActive(DisplayStatusBar);


        // If the player has touched the screen, then stop the update
        //if (Input.touchCount == 1)
        //{
        //    return;
        //}

        //// What follows only happens if the user has touched the screen
        //// Look for a plane hitting the raycast from the touch point (TrackableHit, TrackableHitFlags, Frame.Raycast
        //TrackableHit hit;
        //TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
        //    TrackableHitFlags.FeaturePointWithSurfaceNormal;
        // Use hit pose and camera pose to check if hittest is from the
        // back of the plane, if it is, no need to create the anchor.
        // Instanciate the model at the hit pose
        // Compensate for the hitPose rotation facing away from the raycast (ie camera)
        // Create an anchor to allow ARCore to track the hitpoint as understanding to the physical world evolves
        // Make the model a child of the anchor
    }

    // Check and update the app lifecycle
    private void _UpdateApplicationLifeCycle()
    {
        //throw new NotImplementedException();

        // Exit the app when the back button is pressed
        // Only allow the screen to sleep when not tracking
        // Quit if ARCore was unable to connect and give Unity some time for the toast to appear
    }

    // Quit the app
    private void _DoQuit()
    {
        Application.Quit();
    }

    // Show an android toast message
    private void _ShowAndroidToastMessage(string message)
    {

    }
}
