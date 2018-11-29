using FarmingVR.Event;
using FarmingVR.Interactions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelController : MonoBehaviour {

    private string _modelName;
    private Vector3 _modelPosition;
    private Vector3 _modelScale;
    private Quaternion _modelRotation;

    [SerializeField]
    private GameObject _infoPanel;


    // Update is called once per frame
    void UpdateDisplay () {
        // If a model is currently selected
        if (_modelName != null)
        {
            // Display its information
            _infoPanel.GetComponent<Text>().text =
            "Model name     " + _modelName +
            "Model position " + _modelPosition +
            "Model scale    " + _modelScale +
            "Model rotation " + _modelRotation
            ;
        } else // If no model is selected
        {
            _infoPanel.GetComponent<Text>().text = "Please select a model";
        }
    }
    // Use this for initialization
    void Awake()
    {
        ModelSelectedEvent.RegisterListener(SaveSelectedModel);
        RotateModelEvent.RegisterListener(UpdateDisplayRotate);
        RescaleModelEvent.RegisterListener(UpdateDisplayRescale);
        //MoveModelEvent.RegisterListener(UpdateDisplayMove);

        _infoPanel.GetComponent<Text>().text = "Please select a model";

    }

    private void OnDisable()
    {
        ModelSelectedEvent.UnregisterListener(SaveSelectedModel);
        RotateModelEvent.UnregisterListener(UpdateDisplayRotate);
        RescaleModelEvent.UnregisterListener(UpdateDisplayRescale);
        //MoveModelEvent.UnregisterListener(UpdateDisplayMove);
    }

    /// <summary>
    /// Callback method called when the user rotates the currently selected model
    /// </summary>
    /// <param name="info"></param>
    private void UpdateDisplayRotate(RotateModelEvent info)
    {
        _modelRotation = Selector.CurrentlySelected.transform.rotation;
        UpdateDisplay();
    }

    /// <summary>
    /// Callback method called when the user rescales the currently selected model
    /// </summary>
    /// <param name="info"></param>
    private void UpdateDisplayRescale(RescaleModelEvent info)
    {
        _modelScale = Selector.CurrentlySelected.transform.localScale;
        UpdateDisplay();
    }

    ///// <summary>
    ///// Callback method called when the user moves the currently selected model
    ///// </summary>
    ///// <param name="info"></param>
    //private void UpdateDisplayMove(RescaleModelEvent info)
    //{
    //    _modelPosition = currentTransform.position;
    //    UpdateDisplay();
    //}

    /// <summary>
    /// Callback method called when the user selects a model
    /// The method saves the useful information
    /// </summary>
    /// <param name="info"></param>
    private void SaveSelectedModel(ModelSelectedEvent info)
    {
        if (Selector.CurrentlySelected != null)
        {
            var currentTransform = Selector.CurrentlySelected.transform;
            _modelName = currentTransform.name;
            _modelPosition = currentTransform.position;
            _modelScale = currentTransform.localScale;
            _modelRotation = currentTransform.rotation;

        }
        else
        {
            _modelName = null;
        }

        UpdateDisplay();
    }
}
