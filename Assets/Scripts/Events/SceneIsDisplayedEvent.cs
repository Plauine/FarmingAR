using EventCallbacks;
using GoogleARCore;
using UnityEngine;

namespace FarmingVR.Event
{
    public class SceneIsDisplayedEvent : Event<SceneIsDisplayedEvent>
    {
        public readonly Transform FarmTransform;

        public SceneIsDisplayedEvent(Transform farmTransform) : base ("This event is triggered when the model has been placed on the scene. It allows other scripts to register the model")
        {
            this.FarmTransform = farmTransform;

            FireEvent(this);
        }
    }
}

