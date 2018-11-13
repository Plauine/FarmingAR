using FarmingVR.Event;
using GoogleARCore;
using UnityEngine;

namespace FarmingVR.ScenePreparation
{
    public class SceneDisplayer : MonoBehaviour
    {

        [SerializeField] private Camera _FirstPersonCamera;
        [SerializeField] private Transform _FarmScene;

        public static bool SceneIsDisplayed;

        // Constant value to make sure the scene will be facing the user when placed
        private const float k_ModelRotation = 180.0f;

        // Use this for initialization
        void Start()
        {
            DisplaySceneEvent.RegisterListener(OnEventFired);
        }

        void OnDisable()
        {
            // We unregister the listener on disable
            DisplaySceneEvent.UnregisterListener(OnEventFired);
        }

        private void OnEventFired(DisplaySceneEvent p_info)
        {
            // Look for a plane hitting the raycast from the touch point (TrackableHit, TrackableHitFlags, Frame.Raycast
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (Frame.Raycast(p_info.PlaceOfClick.x, p_info.PlaceOfClick.y, raycastFilter, out hit))
            {
                // Use hit pose and camera pose to check if hittest is from the
                // back of the plane, if it is, no need to create the anchor.
                if ((hit.Trackable is DetectedPlane) &&
                     Vector3.Dot(_FirstPersonCamera.transform.position - hit.Pose.position,
                                 hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else if (!SceneIsDisplayed)
                {
                    new SceneIsDisplayedEvent(DisplayModel(hit));
                }
            }
        }

        private Transform DisplayModel(TrackableHit hit)
        {
            // Instanciate the model at the hit pose
            var farmObject = Instantiate(_FarmScene, hit.Pose.position, Quaternion.Euler(0.0f, k_ModelRotation, 0.0f));

            // Create an anchor to allow ARCore to track the hitpoint as understanding to the physical world evolves
            var anchor = hit.Trackable.CreateAnchor(hit.Pose);

            // Make the model a child of the anchor
            farmObject.transform.parent = anchor.transform;

            // TODO : Create new event when new anchor and object is placed on the scene

            SceneIsDisplayed = true;

            return farmObject.transform;
        }
    }
}

