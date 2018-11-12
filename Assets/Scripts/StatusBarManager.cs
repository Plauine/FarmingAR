using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles the display of StatusBar
/// </summary>

public class StatusBarManager : MonoBehaviour {

    [SerializeField] private GameObject _statusBar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // DisplayStatusBar indicated if we must display the status bar or not
        bool DisplayStatusBar = false;
        if (GetComponent<PlanDetector>().m_AllPlanes.Count == 0)
        {
            DisplayStatusBar = true;
        }

        _statusBar.SetActive(DisplayStatusBar);
    }
}
