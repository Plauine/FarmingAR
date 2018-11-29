using EventCallbacks;

public class ModelSelectedEvent : Event<ModelSelectedEvent>
{
    public ModelSelectedEvent() : base("This event informs when a model has been selected")
    {
        FireEvent(this);
    }
}
