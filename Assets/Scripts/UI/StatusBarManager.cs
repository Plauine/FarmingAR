using System;
using FarmingVR.ScenePreparation;
using UnityEngine;

/// <summary>
/// This script handles the display of StatusBar
/// </summary>

namespace FarmingVR.UI
{
    public class StatusBarManager : MonoBehaviour {

        [SerializeField] private GameObject _StatusBar;

        private void Start()
        {
            FoundAPlaneEvent.RegisterListener(HideStatusBar);
        }

        private void HideStatusBar(FoundAPlaneEvent info)
        {
            _StatusBar.SetActive(false);
        }
    }
}

