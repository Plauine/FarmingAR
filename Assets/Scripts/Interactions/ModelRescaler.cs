using FarmingVR.Event;
using UnityEngine;

namespace FarmingVR.Interactions
{
    /// <summary>
    /// This class rescales the model on demand
    /// </summary>
    public class ModelRescaler : MonoBehaviour
    {
        /// <summary>
        /// A reference to the model
        /// </summary>
        private Transform _modelTransform;

        private float _zoomSpeed = 0.01f;
        
        // Use this for initialization
        void Start ()
        {
            // Register listeners
            RescaleModelEvent.RegisterListener(RescaleScene);
            ModelIsDisplayedEvent.RegisterListener(RegisterModel);
        }

        void OnDisable()
        {
            RescaleModelEvent.UnregisterListener(RescaleScene);
            ModelIsDisplayedEvent.UnregisterListener(RegisterModel);
        }

        /// <summary>
        /// This function rescales the scene when the RescaleModelEvent is triggered 
        /// </summary>
        /// <param name="infoEvent">Information about the event especially the ratio of the rescaling</param>
        private void RescaleScene(RescaleModelEvent infoEvent)
        {
            var futureScale = _modelTransform.localScale - new Vector3(infoEvent.Modificator * _zoomSpeed, infoEvent.Modificator * _zoomSpeed, infoEvent.Modificator * _zoomSpeed);
            // If the future scale is not negative
            if (futureScale.x > 0.1f && futureScale.y > 0.1f && futureScale.z > 0.1f)
            {
                // Apply the modificator to the local Scale
                _modelTransform.localScale = futureScale;
            }

        }

        /// <summary>
        /// This function registers a reference to the model when it has been displayed
        /// </summary>
        /// <param name="infoEvent"></param>
        private void RegisterModel(ModelIsDisplayedEvent infoEvent)
        {
            _modelTransform = infoEvent.FarmTransform;
        }
    }
}
