
using FarmingVR.Event;
using UnityEngine;

#if UNITY_EDITOR
public class InputsCollectorEditor : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            new DisplayModelEvent(Input.mousePosition);
        }
        else if (FarmingVR.ScenePreparation.ModelDisplayer.SceneIsDisplayed)
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                new RescaleModelEvent(-Input.GetAxis("Vertical") * 0.8f);
            }
            if (Input.GetAxis("Horizontal") != 0)
            {
                new RotateModelEvent(Input.GetAxis("Horizontal"));
            }
        }
    }
}
    
#endif