using FarmingVR.Event;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FarmingVR.Interactions
{
    /// <summary>
    /// This script handles the selection of a model
    /// It is given a click or touch position and decides which (if there is any) model has been selected
    /// </summary>
    public class Selector : MonoBehaviour
    {
        public static Transform CurrentlySelected;

        private static Camera _camera;

        [SerializeField] private Camera _cam2;

        private void Start()
        {
            _camera = _cam2;
        }

        /// <summary>
        /// This method calculates the model hit by the user's input and fires the ModelSelectedEvent
        /// </summary>
        /// <param name="clickPosition"></param>
        public static void SelectModel(Vector3 clickPosition)
        {
            // Check if the hit encountered a model
            Ray ray = _camera.ScreenPointToRay(clickPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) // If the user clicked on something
            {
                // If the user clicked on a new model
                if (CurrentlySelected != hit.transform)
                {
                    CurrentlySelected = hit.transform;
                    new ModelSelectedEvent();
                }
            }
            else // If the user didnt click on anything
            {
                // Reset the currently selected transform and fire the event anyway
                CurrentlySelected = null;
                new ModelSelectedEvent();
            }

        }
    }
}
