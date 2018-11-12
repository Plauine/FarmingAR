using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanDetector : MonoBehaviour {

    public List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Session.GetTrackables<DetectedPlane>(m_AllPlanes);
    }
}
