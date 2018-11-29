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

    private Animator _panelAnimator;
    private Animator _buttonAnimator;


    private void Start()
    {
        ModelIsDisplayedEvent.RegisterListener(ShowButton);
        _panelAnimator = _infoPanel.GetComponent<Animator>();
        _buttonAnimator = _button.GetComponent<Animator>();
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
        _panelAnimator.SetTrigger("DisplayInfo");
        _buttonAnimator.SetTrigger("HideButton"); ;

    }
}
