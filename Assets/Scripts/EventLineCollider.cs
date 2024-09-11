using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLineCollider : MonoBehaviour
{
    public EventLine EventLine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            int i = Game.Battle.player.transform.position.x < 0 ? 0 : 1;
            EventLine.EventLineTouch(i);
        }
    }




}
