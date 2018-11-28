using FarmingVR.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private GameObject _infoPanel;

    [SerializeField]
    private GameObject _button;

    private Transform _model;

    private Animator _animator;

    private Text _textHolder;

    private void Update()
    {
        if (_model != null)
        {
            // Get values of the model's rotation and scale
            var textToDisplay = "Model's scale: " + _model.transform.localScale;
            textToDisplay += "\nModel's rotation: " + _model.transform.rotation;

            _textHolder.text = textToDisplay;
        }
    }

    private void Start()
    {
        ModelIsDisplayedEvent.RegisterListener(ShowButton);
        _animator = _infoPanel.GetComponent<Animator>();
        _textHolder = GameObject.Find("InfoDisplay").GetComponent<Text>();
    }

    private void OnDisable()
    {
        ModelIsDisplayedEvent.UnregisterListener(ShowButton);
    }

    private void ShowButton(ModelIsDisplayedEvent info)
    {
        _model = info.FarmTransform;
        _button.SetActive(true);
    }

    public void DisplayPanel()
    {
        _button.SetActive(false);
        _animator.SetTrigger("DisplayInfo");
    }
}
