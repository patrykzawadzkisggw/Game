using UnityEngine;

public class PlayerMovement2 : PlayerMovement
{

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        
            anim = GetComponent<Animator>();
          boxCollider = GetComponent<BoxCollider2D>();
       
    }

    
}