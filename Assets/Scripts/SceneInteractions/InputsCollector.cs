using FarmingVR.Event;
using UnityEngine;

namespace FarmingVR.SceneInteractions
{
    public class InputsCollector : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            // Check if touchCount < 2 for rescale for example
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                new DisplaySceneEvent(Input.GetTouch(0).position);
               
            }
            else if (ScenePreparation.SceneDisplayer.SceneIsDisplayed)
            {
                // TODO : continue triggering the events on touch
                Debug.Log("delta finger 1: " + Input.GetTouch(0).deltaPosition);
                Debug.Log("delta finger 2: " + Input.GetTouch(1).deltaPosition);
                //// TO MODIFY FOR TOUCH
                //if (Input.GetAxis("Vertical") != 0)
                //{
                //    new RescaleSceneEvent(Input.GetAxis("Vertical"));
                //}
                //// TO MODIFY FOR TOUCH : deltaPosition
                //if (Input.GetAxis("Horizontal") != 0)
                //{
                //    new RotateSceneEvent(Input.GetAxis("Horizontal"));
                //}
            }
        }
    }
}
