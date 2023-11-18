using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private GatherInput gI;
    private PlayerMoveControls moveControls;

    private void OnTriggerEnter2D(Collider2D other)
    {
        gI = other.GetComponent<GatherInput>();
        moveControls = other.GetComponent<PlayerMoveControls>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(gI.tryingToClimb)
        {
            moveControls.onLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        moveControls.ExitLadder();
    }
}
