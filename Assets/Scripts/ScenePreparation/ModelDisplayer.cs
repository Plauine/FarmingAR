using FarmingVR.Event;
using FarmingVR.Interactions;
using GoogleARCore;
using UnityEngine;

namespace FarmingVR.ScenePreparation
{
    /// <summary>
    /// This class displays the scene on demand: when DisplaySceneEvent is triggered
    /// </summary>
    public class ModelDisplayer : MonoBehaviour
    {
        /// <summary>
        /// A ref to the Camera
        /// </summary>
        [SerializeField] private Camera _firstPersonCamera;

        /// <summary>
        /// A ref to the model
        /// </summary>
        [SerializeField] private Transform _farmModel;

        /// <summary>
        /// Boolean to true if the scene is displayed
        /// </summary>
        public static bool AtLeastOneIsDisplayed;

        /// <summary>
        /// Constant value to make sure the scene will be facing the user when placed
        /// </summary>
        private const float k_ModelRotation = 180.0f;

        // Use this for initialization
        void Start()
        {
            DisplayModelEvent.RegisterListener(PrepareModelDisplay);
        }

        void OnDisable()
        {
            // We unregister the listener on disable
            DisplayModelEvent.UnregisterListener(PrepareModelDisplay);
        }

        /// <summary>
        /// Callback function triggered by the DisplaySceneEvent
        /// It prepares the scene to display the model
        /// </summary>
        /// <param name="info"></param>
        private void PrepareModelDisplay(DisplayModelEvent info)
        {
            if (!AtLeastOneIsDisplayed)
            {
                // Look for a plane hitting the raycast from the touch point
                TrackableHit hit;
                TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                    TrackableHitFlags.FeaturePointWithSurfaceNormal;

                if (Frame.Raycast(info.PlaceOfClick.x, info.PlaceOfClick.y, raycastFilter, out hit))
                {
                    // Use hit pose and camera pose to check if hittest is from the
                    // back of the plane, if it is, no need to create the anchor.
                    if ((hit.Trackable is DetectedPlane) &&
                         Vector3.Dot(_firstPersonCamera.transform.position - hit.Pose.position,
                                     hit.Pose.rotation * Vector3.up) < 0)
                    {
                        Debug.Log("Hit at back of the current DetectedPlane");
                    }
                    else // If we hit an appropriate place
                    {
                        DisplayModel(hit);
                    }
                }
            }
            
        }

        /// <summary>
        /// This method displays the model 
        /// </summary>
        /// <param name="hit">The place where the user clicked/touched</param>
        /// <returns>The model</returns>
        private void DisplayModel(TrackableHit hit)
        {
            // Instanciate the model at the hit pose
            var farmObject = Instantiate(_farmModel, hit.Pose.position, Quaternion.Euler(0.0f, k_ModelRotation, 0.0f));

            // Create an anchor to allow ARCore to track the hitpoint as understanding to the physical world evolves
            var anchor = hit.Trackable.CreateAnchor(hit.Pose);

            // Make the model a child of the anchor
            farmObject.transform.parent = anchor.transform;

            // Create new event when new anchor and object are placed on the scene
            new ModelIsDisplayedEvent(farmObject);

            AtLeastOneIsDisplayed = true;
        }
    }
}

