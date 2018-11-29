using FarmingVR.Event;
using FarmingVR.ScenePreparation;
using GoogleARCore;
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

        /// <summary>
        /// Movement threshold
        /// </summary>
        private float _threshold = 0.2f;



        // Update is called once per frame
        void Update()
        {
            // Switch between number of touch on the screen
            switch (Input.touchCount)
            {
                case 1:
                    // If the model is not yet displayed
                    if (!ModelDisplayer.AtLeastOneIsDisplayed)
                    {
                        // If it is the beginning of the touch phase
                        if (Input.GetTouch(0).phase == TouchPhase.Began)
                        {
                            // Display the model
                            new DisplayModelEvent(Input.GetTouch(0).position);
                        }
                    } else
                    {
                        // If the user hits a model 
                        // Look for a plane hitting the raycast from the touch point
                        TrackableHit hit;
                        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                            TrackableHitFlags.FeaturePointWithSurfaceNormal;

                        if (Frame.Raycast(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, raycastFilter, out hit))
                        {
                            Debug.Log(hit);
                        }
                        // Fire event
                        // If the finger is moving on the screen and a model is already selected
                        if (Input.GetTouch(0).deltaPosition.x >= _threshold && Selector.CurrentlySelected != null)
                        {
                            // Rotate the model
                            new RotateModelEvent(Input.GetTouch(0).deltaPosition.x);
                        }
                    }
                    break;
                case 2:
                    var phase1 = Input.GetTouch(0).phase;
                    var phase2 = Input.GetTouch(1).phase;

                    Debug.Log("phase1 null" + phase1==null);
                    Debug.Log("phase2 null" + phase2 == null);

                    if ( // If none of the Touch is ending nor beginning and one of them is moving
                        (phase1 != TouchPhase.Ended && phase2 != TouchPhase.Ended) && 
                        (phase1 != TouchPhase.Began && phase2 != TouchPhase.Began) && 
                        (phase1 == TouchPhase.Moved || phase2 == TouchPhase.Moved))
                    {
                        // Find the position in the previous frame of each touch.
                        var touchZeroPrevPos = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;
                        var touchOnePrevPos = Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition;

                        // Find the magnitude of the vector (the distance) between the touches in each frame.
                        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                        float touchDeltaMag = (Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude;

                        // Create new event with the difference in the distances between each frame.
                        new RescaleModelEvent((prevTouchDeltaMag - touchDeltaMag) * 0.05f);
                    }
                    break;
            }
        }
    }
}
