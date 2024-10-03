using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGroundPoint : MonoBehaviour, IController
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.SendCommand<RestartCommand>();
        }
    }

IArchitecture IBelongToArchitecture.GetArchitecture()
    {
        return SadnessKnightAdventure.Interface;
    }
}
