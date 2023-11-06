using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    public float speed = 4f;

    private GatherInput gI;
    private Rigidbody2D rB;
    // Start is called before the first frame update
    void Start()
    {
        gI = GetComponent<GatherInput>();
        rB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rB.velocity = new Vector2(speed * gI.valueX, rB.velocity.y);
    }
}
