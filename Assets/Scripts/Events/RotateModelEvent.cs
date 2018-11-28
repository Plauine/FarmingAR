using UnityEngine;

namespace FarmingVR.Event
{
    public class RotateModelEvent : EventCallbacks.Event<RotateModelEvent>
    {
        public Vector3 Rotation;

        public RotateModelEvent(float rotationY) : base("This event triggers the rotation of the model")
        {
            Rotation = new Vector3(0, rotationY, 0);

            FireEvent(this);
        }
    }
}

