using FarmingVR.Event;
using UnityEngine;

namespace FarmingVR.SceneInteractions
{
    public class SceneRescaler : MonoBehaviour
    {
        private Transform _ModelTransform;
        
        // Use this for initialization
        void Start ()
        {
            RescaleSceneEvent.RegisterListener(RescaleScene);
            SceneIsDisplayedEvent.RegisterListener(RegisterModel);
        }

        void OnDisable()
        {
            RescaleSceneEvent.UnregisterListener(RescaleScene);
            SceneIsDisplayedEvent.UnregisterListener(RegisterModel);
        }

        private void RescaleScene(RescaleSceneEvent infoEvent)
        {
            Vector3 futureScale = _ModelTransform.localScale + new Vector3(infoEvent.Ratio, infoEvent.Ratio, infoEvent.Ratio);
            
            if (futureScale.x > 0.1f && futureScale.y > 0.1f && futureScale.z > 0.1f)
            
            {
                _ModelTransform.localScale = futureScale;
            }
        }

        private void RegisterModel(SceneIsDisplayedEvent infoEvent)
        {
            _ModelTransform = infoEvent.FarmTransform;
        }
    }
}
