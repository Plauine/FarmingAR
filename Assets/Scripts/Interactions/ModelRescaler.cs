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

        /// <summary>
        /// The rescale speed
        /// It depends on the model's size
        /// </summary>
        private float _rescaleSpeed;
        
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
            if (infoEvent != null)
            {
                Debug.Log("Modificator: " + infoEvent.Modificator);
                var futureScale = _modelTransform.localScale - new Vector3(infoEvent.Modificator * _rescaleSpeed, infoEvent.Modificator * _rescaleSpeed, infoEvent.Modificator * _rescaleSpeed);
                // If the future scale is not negative
                if (futureScale.x > 0.1f && futureScale.y > 0.1f && futureScale.z > 0.1f)
                {
                    // Apply the modificator to the local Scale
                    _modelTransform.localScale = futureScale;
                }
            }

        }

        /// <summary>
        /// This function registers a reference to the model when it has been displayed
        /// </summary>
        /// <param name="infoEvent"></param>
        private void RegisterModel(ModelIsDisplayedEvent infoEvent)
        {
            _modelTransform = infoEvent.FarmTransform;

            // Find and register the biggest value of the model's size
            var modelSize = _modelTransform.GetComponent<MeshRenderer>().bounds.size;
            _rescaleSpeed = Mathf.Max(Mathf.Max(modelSize.x, modelSize.y), modelSize.z) / 100.0f;
        }
    }
}
