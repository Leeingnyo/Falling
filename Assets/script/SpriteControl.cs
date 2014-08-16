using UnityEngine;
using System.Collections;

public class SpriteControl : MonoBehaviour
{

    Animator anim;
    public int speed = 2;
    public float jumpForce = 400.0f;

    bool is_jumping = false;
    bool is_grounded = false;
    bool is_falling = false;

    private float max_h = 0;

    Transform groundCheck;
    private Transform cam;
    private float Height;

    // Use this for initialization
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        groundCheck = transform.Find("GroundCheck");
        cam = Camera.main.transform;
        Camera.main.orthographicSize = 8;
        Height = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //init
        Vector3 moveDir = Vector3.zero;

        //check the grounded
        is_grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("ground"));
        

        //input the keys
        int inputHor;
        if (Input.GetKey(KeyCode.LeftArrow))
            inputHor = -1;
        else if (Input.GetKey(KeyCode.RightArrow))
            inputHor = 1;
        else
            inputHor = 0;

        int inputJump;
        if (Input.GetKey(KeyCode.Space))
            inputJump = 1;
        else
            inputJump = 0;

        //reflect the keys
        if (inputHor != 0)
        {
            anim.SetInteger("dirc", inputHor / Mathf.Abs(inputHor));
            anim.SetBool("moving", true);
            if (inputHor == -1)
                moveDir += Vector3.left;
            else if (inputHor == 1)
                moveDir += Vector3.right;
        }
        else
        {
            anim.SetBool("moving", false);
        }

        //air_
        if (is_grounded == true)
        {
           // Debug.Log("IS GROUND");
            max_h = groundCheck.position.y;
            anim.SetBool("inAir", false);
        }
        else
        {
           // Debug.Log("IS NOT GROUND");
        }

        if (inputJump != 0)
            if (is_jumping == false && is_grounded == true)
            {
                
                anim.SetBool("inAir", true);
                is_jumping = true;
            }
        //check fall
        if (max_h - 0.4 > transform.position.y)
        {
            is_falling = true;
            this.rigidbody2D.fixedAngle = false;
            speed = 5;
        }

        //move
        transform.position += (moveDir * Time.deltaTime * speed);

        //cam move
        cam.transform.position = new Vector3(0, transform.position.y, -1);
       
    }

    void FixedUpdate()
    {
        if (is_jumping)
        {
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
        is_jumping = false;
        if (is_falling)
        {
            rigidbody2D.gravityScale = 0;
            rigidbody2D.velocity = new Vector3(0, -3, 0);
        }

    }

    void OnCollisionEnter(Collision coll)
    {
        if (is_grounded)
        {
            max_h = coll.transform.position.y;
        }
    }
}