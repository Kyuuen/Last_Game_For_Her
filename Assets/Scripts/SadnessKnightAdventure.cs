using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class SadnessKnightAdventure : Architecture<SadnessKnightAdventure>
{
    protected override void Init()
    {
        RegisterSystem<IEventCenterSystem>(new EventCenterSystem());
    }
}
