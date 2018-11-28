using FarmingVR.ScenePreparation;
using UnityEngine;

/// <summary>
/// This script handles the display of StatusBar
/// </summary>

namespace FarmingVR.UI
{
    public class StatusBarManager : MonoBehaviour {

        [SerializeField] private GameObject _StatusBar;

	    // Update is called once per frame
	    void Update () {

             // Set the display to true
             var displayStatusBar = GetComponent<PlanDetector>().m_AllPlanes.Count == 0;

            _StatusBar.SetActive(displayStatusBar);
        }
    }
}

