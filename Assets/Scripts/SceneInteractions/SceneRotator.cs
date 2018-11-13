using FarmingVR.Event;
using UnityEngine;

namespace FarmingVR.SceneInteractions

{
    public class SceneRotator : MonoBehaviour
    {
        private Transform _ModelTransform;

	    // Use this for initialization
	    void Start ()
        {
            RotateSceneEvent.RegisterListener(RotateScene);
            SceneIsDisplayedEvent.RegisterListener(RegisterModel);
	    }

        private void OnDisable()
        {
            RotateSceneEvent.UnregisterListener(RotateScene);
            SceneIsDisplayedEvent.UnregisterListener(RegisterModel);
        }

        private void RotateScene(RotateSceneEvent infoEvent)
        {
            _ModelTransform.Rotate(infoEvent.Rotation);
        }

        private void RegisterModel(SceneIsDisplayedEvent infoEvent)
        {
            _ModelTransform = infoEvent.FarmTransform;
        }
    }
}
