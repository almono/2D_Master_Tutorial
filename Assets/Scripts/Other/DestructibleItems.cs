using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleItems : MonoBehaviour
{
    public int hitsToDestroy;
    public int currentHealth;
    protected Animator itemAnim;

    // Start is called before the first frame update

    private void Awake()
    {
        itemAnim = GetComponent<Animator>();
    }

    public void HitDestructible()
    {
        hitsToDestroy -= 1;
        OnHit();

        if(hitsToDestroy <= 0 )
        {
            OnItemDestroy();
        }
    }

    public virtual void OnHit()
    {

    }

    public virtual void OnItemDestroy() 
    { 

    }

    public void ItemCleanUp()
    {
        Destroy(gameObject);
    }
}
