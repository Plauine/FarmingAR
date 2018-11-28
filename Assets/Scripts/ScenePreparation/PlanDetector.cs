using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingVR.ScenePreparation
{
    public class PlanDetector : MonoBehaviour {
        /// <summary>
        /// A reference to all the detected plans
        /// </summary>
        public List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();
    
	    // Update is called once per frame
	    void Update () {
            Session.GetTrackables(m_AllPlanes);
        }
    }
}


