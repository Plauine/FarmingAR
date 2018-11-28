using FarmingVR.Event;
using FarmingVR.ScenePreparation;
using UnityEngine;

namespace FarmingVR.Interactions
{

    /// <summary>
    /// This class collects the imputs, decides in which case the user is and triggers the appropriate events
    /// </summary>
    public class InputsCollector : MonoBehaviour
    {
        /// <summary>
        /// The first distance between the two touches
        /// It is the reference
        /// </summary>
        private float _refDistance;

        // private float _threshold = 0.2f;

        // Update is called once per frame
        void Update()
        {
            // Switch between number of touch on the screen
            switch (Input.touchCount)
            {
                case 1:
                    // If the model is not yet displayed
                    if (!ModelDisplayer.SceneIsDisplayed)
                    {
                        // If it is the beginning of the touch phase
                        if (Input.GetTouch(0).phase == TouchPhase.Began)
                        {
                            // Display the model
                            new DisplayModelEvent(Input.GetTouch(0).position);
                        }
                    } else
                    {
                        // If the finger is moving on the screen
                        if (Input.GetTouch(0).deltaPosition.x != 0)
                        {
                            // Rotate the model
                            new RotateModelEvent(Input.GetTouch(0).deltaPosition.x);
                        }
                    }
                    break;
                case 2:
                    var phase1 = Input.GetTouch(0).phase;
                    var phase2 = Input.GetTouch(1).phase;

                    // If at least one of the two touch is ending
                    if (phase1 == TouchPhase.Ended || phase2 == TouchPhase.Ended)
                    {
                        // Reset referenceDistance
                        _refDistance = -1.0f;
                    }
                    else if (phase1 == TouchPhase.Began || phase2 == TouchPhase.Began) // Else if at least one is beginning 
                    {
                        // set referenceDistance in absolute values
                        _refDistance = System.Math.Abs(Input.GetTouch(0).position.x - Input.GetTouch(1).position.x);

                    }
                    else if (phase1 == TouchPhase.Moved || phase2 == TouchPhase.Moved) // Else if at least one of the touches is moving
                    {
                        // Find the position in the previous frame of each touch.
                        var touchZeroPrevPos = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;
                        var touchOnePrevPos = Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition;

                        // Find the magnitude of the vector (the distance) between the touches in each frame.
                        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                        float touchDeltaMag = (Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude;

                        // Create new event with the difference in the distances between each frame.
                        new RescaleModelEvent(prevTouchDeltaMag - touchDeltaMag);
                    }
                    break;
            }
        }
    }
}
