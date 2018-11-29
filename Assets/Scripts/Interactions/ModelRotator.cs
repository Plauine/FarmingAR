using FarmingVR.Event;
using FarmingVR.Interactions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotator : MonoBehaviour {

    // private float _rotationSpeed = 0.8f;

	// Use this for initialization
	void Start () {
        RotateModelEvent.RegisterListener(rotateModel);
	}

    private void OnDisable()
    {
        RotateModelEvent.UnregisterListener(rotateModel);
    }

    private void rotateModel(RotateModelEvent info)
    {
        Selector.CurrentlySelected.transform.Rotate(new Vector3(0, info.RotationY, 0));
    }
}
