using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifecycleManager : MonoBehaviour {
    // Is true when the app is quitting due to an ARCore connection error
    private bool m_IsQuitting;

    // Use this for initialization
    void Start () {
        Debug.Log("Starting");
        m_IsQuitting = false;
    }
	
	// Update is called once per frame
	void Update () {
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
            // Trigger the event listened by ShowAndroidToastMessage with the message "Camera permission is needed to run this application."
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
        else if (Session.Status.IsError())
        {
            // Trigger the event listened by ShowAndroidToastMessage with the message "ARCore encountered a problem connecting.  Please start the app again."
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }

        new LifecycleManagementDoneEvent();
    }

    // Quit the app
    private void _DoQuit()
    {
        Application.Quit();
    }
}
