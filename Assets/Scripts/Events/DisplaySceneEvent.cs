using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCallbacks;

public class DisplaySceneEvent : Event<DisplaySceneEvent> {

    public readonly Vector3 PlaceOfClick;

    public DisplaySceneEvent(Vector3 PlaceOfClick) : base("This event triggers the display of the scene on a plan")
    {
        this.PlaceOfClick = PlaceOfClick;

        FireEvent(this);
    }
}
