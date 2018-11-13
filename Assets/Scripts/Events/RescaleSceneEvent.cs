
namespace FarmingVR.Event
{
    public class RescaleSceneEvent : EventCallbacks.Event<RescaleSceneEvent>
    {
        public readonly float Ratio;

        public RescaleSceneEvent(float ratio) : base("This event triggers the rescaling of the scene")
        {
            this.Ratio = ratio;

            FireEvent(this);
        }
    }
}