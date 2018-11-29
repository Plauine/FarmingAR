using EventCallbacks;

public class FoundAPlaneEvent : Event<FoundAPlaneEvent>
{
    public FoundAPlaneEvent() : base("This event is triggered by PlaneDetector as soon as one plane is detected")
    {
        FireEvent(this);
    }
}
