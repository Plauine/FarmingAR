
namespace FarmingVR.Event
{
    public class RescaleModelEvent : EventCallbacks.Event<RescaleModelEvent>
    {   
        /// <summary>
        /// The intensity of the rescaling directly coming from the input 
        /// </summary>
        public readonly float ScaleModificator;

        /// <summary>
        /// Controls the scaleSpeed
        /// </summary>
        private readonly float _scaleSpeed = 0.01f;

        /// <summary>
        /// Constructor of the event
        /// </summary>
        /// <param name="scaleModificator">We expect a value between -1 and 1</param>
        public RescaleModelEvent(float scaleModificator) : base("This event triggers the rescaling of the model")
        {
            ScaleModificator = scaleModificator * _scaleSpeed;
            FireEvent(this);
        }
    }
}