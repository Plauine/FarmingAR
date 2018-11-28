using FarmingVR.Event;
using UnityEngine;

namespace FarmingVR.ScenePreparation
{
    public class SceneCleaner : MonoBehaviour {

        /// <summary>
        /// This callback method is called once the model has been displayed
        /// It hides all the detected plans and points
        /// </summary>
        /// <param name="info"></param>
        private void HidePlansAndPoints(ModelIsDisplayedEvent info)
        {
            // Hide Plane Generator
            GameObject.Find("Plane Generator").SetActive(false);

            // Hide Point Cloud
            GameObject.Find("Point Cloud").SetActive(false);
        }

        /// <summary>
        /// Set and unset the listener
        /// </summary>
        private void Start()
        {
            ModelIsDisplayedEvent.RegisterListener(HidePlansAndPoints);
        }

        private void OnDisable()
        {
            ModelIsDisplayedEvent.UnregisterListener(HidePlansAndPoints);
        }
    }
}

