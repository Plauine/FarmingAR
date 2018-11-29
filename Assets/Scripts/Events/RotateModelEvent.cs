using UnityEngine;

namespace FarmingVR.Event
{
    public class RotateModelEvent : EventCallbacks.Event<RotateModelEvent>
    {
        public readonly float RotationY;

        public RotateModelEvent(float rotationY) : base("This event triggers the rotation of the model")
        {
            RotationY = rotationY;
            FireEvent(this);
        }
    }
}

