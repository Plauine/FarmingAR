using FarmingVR.Event;
using UnityEngine;

namespace FarmingVR.Interactions

{
    /// <summary>
    /// This class handles the model rotation
    /// </summary>
    public class ModelRotator : MonoBehaviour
    {
        /// <summary>
        /// A reference to the model
        /// </summary>
        private Transform _modelTransform;

	    // Use this for initialization
	    void Start ()
        {
            RotateModelEvent.RegisterListener(RotateScene);
            ModelIsDisplayedEvent.RegisterListener(RegisterModel);
	    }

        private void OnDisable()
        {
            RotateModelEvent.UnregisterListener(RotateScene);
            ModelIsDisplayedEvent.UnregisterListener(RegisterModel);
        }

        /// <summary>
        /// This function rotates the model accordingly the to rotation value of the event when the latter is triggered
        /// </summary>
        /// <param name="infoEvent"></param>
        private void RotateScene(RotateModelEvent infoEvent)
        {
            _modelTransform.Rotate(-infoEvent.Rotation);
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
