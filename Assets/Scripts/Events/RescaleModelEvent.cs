
namespace FarmingVR.Event
{
    public class RescaleModelEvent : EventCallbacks.Event<RescaleModelEvent>
    {
        public readonly float Modificator;

        public RescaleModelEvent(float modificator) : base("This event triggers the rescaling of the model")
        {
            this.Modificator = modificator;

            FireEvent(this);
        }
    }
}