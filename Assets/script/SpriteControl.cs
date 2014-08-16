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

    private float time_crush;
    bool is_crush;
    private float time_wing;
    bool is_wing;
    private float time_jumpup;
    bool is_jumpup;
    private int num_sheild;

    private float max_h;

    Transform groundCheck;
    private Transform cam;

    // Use this for initialization
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        groundCheck = transform.Find("GroundCheck");
        cam = Camera.main.transform;
        max_h = 0;
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
            anim.SetBool("inAir", false);
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
            anim.SetTrigger("fall");
            this.rigidbody2D.fixedAngle = false;
            speed = 5;
        }
        //check item
        CheckItem();

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
		if(is_falling){
			rigidbody2D.gravityScale = 0;
			rigidbody2D.velocity = new Vector2(0, -3);
		}
        if (is_wing)
        {
            rigidbody2D.gravityScale = 0;
            rigidbody2D.velocity = new Vector2(0, 15f);
        }
        if (time_wing < 0.0f)
        {
			rigidbody2D.gravityScale = 1;
		}
		

    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "tiles")
        {
            if (is_grounded)
            {
                max_h = coll.transform.position.y;
            }
        }
    }

    public void CheckItem()
    {
        if (is_crush)
        {
            time_crush -= Time.deltaTime;
            if (time_crush < 0.0f)
            {
                is_crush = false;
            }
            //Crush()
        }
        if (is_jumpup)
        {
            time_jumpup -= Time.deltaTime;
            if (time_jumpup < 0.0f)
            {
                is_jumpup = false;
            }
            jumpForce = 500.0f;
        }
        else
        {
            jumpForce = 400.0f;
        }
        if (is_wing)
        {
            time_wing -= Time.deltaTime;
            if (time_wing < 0.0f)
            {
                is_wing = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D item)
    {
        if (item.tag == "Crush")
        {
            is_crush = true;
            time_crush = 5.0f;
            Destroy(item.gameObject);
        }
        else
            if (item.tag == "Jump_up")
            {
                is_jumpup = true;
                time_jumpup = 5.0f;
                Destroy(item.gameObject);
			ParticleManager.manager.setEffect(5,transform.position);
				}
            else
                if (item.tag == "Wing")
                {
                    is_wing = true;
                    time_wing = 5.0f;

                    Destroy(item.gameObject);
                }
                else
                    if (item.tag == "Sheild")
                    {
                        num_sheild++;
                        Destroy(item.gameObject);
			ParticleManager.manager.setEffect(4,transform.position);
				}

        if (is_falling == true && item.tag == "tiles")
        {
            if (num_sheild > 0)
            {
                num_sheild--;
            }
            else
            {
                rigidbody2D.velocity -= new Vector2(0, 0.5f); //속도 늦춤
                //부숴지는 이펙트 설정 (?)
                Destroy(item.gameObject, 1); //진짜로 부숨
            }
        }
    }
}