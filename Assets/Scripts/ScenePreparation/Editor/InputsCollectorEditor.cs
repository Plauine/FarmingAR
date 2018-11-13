
using FarmingVR.Event;
using UnityEngine;

public class InputsCollectorEditor : MonoBehaviour {
#if UNITY_EDITOR

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            new DisplaySceneEvent(Input.mousePosition);
        }
        else if (FarmingVR.ScenePreparation.SceneDisplayer.SceneIsDisplayed)
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                new RescaleSceneEvent(Input.GetAxis("Vertical"));
            }
            if (Input.GetAxis("Horizontal") != 0)
            {
                new RotateSceneEvent(Input.GetAxis("Horizontal"));
            }
        }
    }
}
#endif