using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isGround : MonoBehaviour
{
    [SerializeField]  LayerMask layer;
    public  bool IsGround = true;
    [SerializeField] Rigidbody2D rb;
    public float jumpspeed = 10f;
    void Update()
    {
        if (!GameManager.isStart)
            return;

        RaycastHit2D iscollider = Physics2D.Raycast(transform.position, Vector2.down, 0.10f, layer);

        if (iscollider != null)
            IsGround = true;
        else 
            IsGround = false;

        if (IsGround == true && Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
    }
}

    
    



