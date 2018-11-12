using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifecycleManagementDoneEvent : Event<LifecycleManagementDoneEvent>
{
    public LifecycleManagementDoneEvent() : base("This event is triggered when the Lifecycle manager has done its job")
    {
        FireEvent(this);
    }
}
