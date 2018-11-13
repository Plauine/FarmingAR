using UnityEngine;
using EventCallbacks;

namespace FarmingVR.Event
{
    public class DisplaySceneEvent : Event<DisplaySceneEvent> {

        public readonly Vector3 PlaceOfClick;

        public DisplaySceneEvent(Vector3 placeOfClick) : base("This event triggers the display of the scene on a plan")
        {
            this.PlaceOfClick = placeOfClick;

            FireEvent(this);
        }
    }
}


