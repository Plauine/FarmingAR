using UnityEngine;
using EventCallbacks;

namespace FarmingVR.Event
{
    public class DisplayModelEvent : Event<DisplayModelEvent> {

        public readonly Vector3 PlaceOfClick;

        public DisplayModelEvent(Vector3 placeOfClick) : base("This event triggers the display of the scene on a plan")
        {
            this.PlaceOfClick = placeOfClick;

            FireEvent(this);
        }
    }
}


