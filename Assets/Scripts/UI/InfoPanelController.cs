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

    [SerializeField]
    private GameObject _infoPanel;


    // Use this for initialization
    void Start () {
        ModelSelectedEvent.RegisterListener(SaveSelectedModel);
    }

    private void OnDisable()
    {
        ModelSelectedEvent.UnregisterListener(SaveSelectedModel);
    }

    private void SaveSelectedModel(ModelSelectedEvent info)
    {
        if (Selector.CurrentlySelected != null)
        {
            var currentTransform = Selector.CurrentlySelected.transform;
            _modelName = currentTransform.name;
            _modelPosition = currentTransform.position;
            _modelScale = currentTransform.localScale;

        } else
        {
            _modelName = null;
        }

    }

    // Update is called once per frame
    void Update () {
        // If a model is currently selected
        if (_modelName != null)
        {
            // Display its information
            _infoPanel.GetComponent<Text>().text =
            "Model name:        " + _modelName +
            "\nModel position   " + _modelPosition +
            "\nModel scale      " + _modelScale
            ;
        } else // If no model is selected
        {
            _infoPanel.GetComponent<Text>().text = "Please select a model";
        }
        

    }
}
