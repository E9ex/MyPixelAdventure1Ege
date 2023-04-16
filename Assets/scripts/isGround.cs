using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isGround : MonoBehaviour
{
    [SerializeField]  LayerMask layer;
    [SerializeField]  bool IsGround = true;
    [SerializeField] Rigidbody2D rb;
    public float jumpspeed = 10f;
    
    

    // Update is called once per frame
    void Update()
    {
        if (!Player.isStart)
            return;
        
        RaycastHit2D iscollider=Physics2D.Raycast(transform.position,Vector2.down,0.10f,layer);
        if (iscollider != null)
            IsGround = true;
        else
            IsGround = false;
        if (IsGround == true && Input.GetKeyDown(KeyCode.Space))
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
        
    }

    
    


}
