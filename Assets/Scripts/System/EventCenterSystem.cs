using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public interface IEventCenterSystem : ISystem 
{
    public void SendRestartEvent();
}

public class EventCenterSystem : AbstractSystem, IEventCenterSystem
{
    protected override void OnInit()
    {
        
    }

    public void SendRestartEvent()
    {
        this.SendEvent(new RestartEvent());
    }
}
