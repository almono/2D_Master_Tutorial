using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : DestructibleItems
{
    public override void OnHit()
    {
        itemAnim.SetTrigger("hit");
    }

    public override void OnItemDestroy()
    {
        itemAnim.SetTrigger("destroy");
    }
}
