using FarmingVR.Event;
using FarmingVR.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRescaler : MonoBehaviour {



    // Use this for initialization
    void Start()
    {
        RescaleModelEvent.RegisterListener(rotateModel);
    }

    private void OnDisable()
    {
        RescaleModelEvent.UnregisterListener(rotateModel);
    }

    /// <summary>
    /// This function takes care of rescaling the currently selected model
    /// </summary>
    /// <param name="info"></param>
    private void rotateModel(RescaleModelEvent info)
    {
        var futureScale = Selector.CurrentlySelected.transform.localScale + new Vector3(info.ScaleModificator, info.ScaleModificator, info.ScaleModificator);
        if(futureScale.x > 0 && futureScale.y > 0 && futureScale.z > 0)
        {
            Selector.CurrentlySelected.transform.localScale = futureScale;
        }
    }
}
