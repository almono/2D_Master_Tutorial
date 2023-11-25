using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Box : DestructibleItems
{
    [SerializeField] private PlayableDirector playDirector;
    public override void OnHit()
    {
        itemAnim.SetTrigger("hit");
    }

    public override void OnItemDestroy()
    {
        itemAnim.SetTrigger("destroy");

        if(playDirector != null )
        {
            playDirector.Play();
        }
    }


}
