using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public float Speed;
    private Rigidbody2D rig;
    public float JumpForce;
    public bool IsJumping;
    public bool DoubleJump;
    private Animator anim;
    bool isBlowing;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();        
    }
 
    // Update is called once per frame
    void Update()
    {
       Move();
       Jump();
    }
    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
        if (Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        if (Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("walk", false);
        }
        
    }
    void Jump()
    {
        if(Input.GetButtonDown("Jump") && !isBlowing)

        {
            if (!IsJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                DoubleJump = true;
            }
            else
            {
                if(DoubleJump)
                {
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    DoubleJump = false;
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            IsJumping = false;
            anim.SetBool("jump", false);
        }   
        if(collision.gameObject.tag == "Spike")
        {
            GameControler.instance.ShowGameOver();
            Destroy(gameObject);
        }   
        if(collision.gameObject.tag == "Saw")
        {
            GameControler.instance.ShowGameOver();
            Destroy(gameObject);
        }

    }
    void OnCollisionExit2D(Collision2D collision)
    {   
    if(collision.gameObject.layer == 6)
        {
        IsJumping = true;
        }   
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 9)
        {
            isBlowing = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 9)
        {
            isBlowing = false;
        }
    }
}
