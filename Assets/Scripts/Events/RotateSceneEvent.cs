using UnityEngine;

namespace FarmingVR.Event
{
    public class RotateSceneEvent : EventCallbacks.Event<RotateSceneEvent>
    {
        public readonly Vector3 Rotation;

        public RotateSceneEvent(float rotationY) : base("This event triggers the rotation of the scene")
        {
            Rotation = new Vector3(0, rotationY, 0);

            FireEvent(this);
        }
    }
}

