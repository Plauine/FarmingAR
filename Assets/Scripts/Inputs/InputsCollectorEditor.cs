
using FarmingVR.Event;
using FarmingVR.Interactions;
using UnityEngine;

public class InputsCollectorEditor : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            // If the user left-clicked 
            if (Input.GetMouseButtonDown(0))
            {
                // If the model has not been displayed yet
                if (!FarmingVR.ScenePreparation.ModelDisplayer.SceneIsDisplayed)
                {
                    // Fire event to display the model
                    new DisplayModelEvent(Input.mousePosition);
                }
                else // If the model has already been displayed
                {
                    Selector.SelectModel(Input.mousePosition);

                }

            }
            else // If the user performed another action
            {
                // If a model is currently selected
                if (Selector.CurrentlySelected != null)
                {
                    if (Input.GetAxis("Vertical") != 0)
                    {
                        var scaleModifier = -Input.GetAxis("Vertical") * 0.8f;
                        new RescaleModelEvent(scaleModifier);
                    }
                    if (Input.GetAxis("Horizontal") != 0)
                    {
                        new RotateModelEvent(Input.GetAxis("Horizontal"));
                    }
                }
            }
        }
    }
}