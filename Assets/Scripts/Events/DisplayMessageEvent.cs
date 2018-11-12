using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCallbacks;

public class DisplayMessageEvent : Event<DisplayMessageEvent> {
    public readonly string Message;

    public DisplayMessageEvent(string Message) : base("This event trigger the display of any message with Android Toast")
    {
        this.Message = Message;

        FireEvent(this);
    }

}
