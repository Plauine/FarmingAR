//-----------------------------------------------------------------------
// <copyright file="DetectedPlaneGenerator.cs" company="Google">
//
// Copyright 2018 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.Common
{
    using System;
    using System.Collections.Generic;
    using FarmingVR.Event;
    using GoogleARCore;
    using UnityEngine;

    /// <summary>
    /// Manages the visualization of detected planes in the scene.
    /// </summary>
    public class DetectedPlaneGenerator : MonoBehaviour
    {
        /// <summary>
        /// A prefab for tracking and visualizing detected planes.
        /// </summary>
        public GameObject DetectedPlanePrefab;

        /// <summary>
        /// A list to hold new planes ARCore began tracking in the current frame. This object is used across
        /// the application to avoid per-frame allocations.
        /// </summary>
        private List<DetectedPlane> m_NewPlanes = new List<DetectedPlane>();

        /// <summary>
        /// Boolean which allows or not the Update method to look for new plans
        /// </summary>
        private bool _check;

        /// <summary>
        /// The Unity Update method.
        /// </summary>
        public void Update()
        {
            // If motion tracking is tracking and can check 
            if (Session.Status == SessionStatus.Tracking && _check)
            {
                // Iterate over planes found in this frame and instantiate corresponding GameObjects to visualize them.
                Session.GetTrackables<DetectedPlane>(m_NewPlanes, TrackableQueryFilter.New);
                for (int i = 0; i < m_NewPlanes.Count; i++)
                {
                    // Instantiate a plane visualization prefab and set it to track the new plane. The transform is set to
                    // the origin with an identity rotation since the mesh for our prefab is updated in Unity World
                    // coordinates.
                    GameObject planeObject = Instantiate(DetectedPlanePrefab, Vector3.zero, Quaternion.identity, transform);
                    planeObject.GetComponent<DetectedPlaneVisualizer>().Initialize(m_NewPlanes[i]);
                }
            }

        }


        /// <summary>
        /// This callback method is called once the model has been displayed
        /// It hides all the detected plans and stops the Update method from detecting further plans
        /// </summary>
        /// <param name="info"></param>
        private void HidePlans(ModelIsDisplayedEvent info)
        {
            _check = false;
            foreach(Transform child in GameObject.Find("Plane Generator").transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Set and unset the listener
        /// </summary>
        private void Start()
        {
            _check = true;
            ModelIsDisplayedEvent.RegisterListener(HidePlans);
        }

        private void OnDisable()
        {
            ModelIsDisplayedEvent.UnregisterListener(HidePlans);
        }
    }
}
