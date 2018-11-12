using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCallbacks;

public class ShowAndroidToastMessage : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DisplayMessageEvent.RegisterListener(OnEventFired);
	}

    private void OnDisable()
    {
        DisplayMessageEvent.UnregisterListener(OnEventFired);
    }

    // Update is called once per frame
    void OnEventFired (DisplayMessageEvent message) {
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
                    "message", 0);
                toastObject.Call("show");
            }));
        }
	}
}
