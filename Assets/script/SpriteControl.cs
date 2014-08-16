using UnityEngine;
using System.Collections;

public class SpriteControl : MonoBehaviour
{

    Animator anim;
    public int speed = 2;
	private Vector3 currentSpeed;
    public float bareFoot = 3f;
	public float rocketBoots = 5f;
	private float jumpForce;

	public float slip = 500f;

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
    Collider2D[] PlayerColliders = null;
    Collider2D[] TilesColliders = null;
    Collider2D current_tile = null;
    // Use this for initialization
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        groundCheck = transform.Find("GroundCheck");
        cam = Camera.main.transform;
        max_h = 0;
    }
    void Start() {
        PlayerColliders = gameObject.GetComponents<Collider2D>();
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
        if ((max_h - 0.4 > transform.position.y) && is_falling == false)
        {
            TilesColliders = GameObject.Find("2-Midground").GetComponentsInChildren<Collider2D>();
            foreach (Collider2D colls in TilesColliders){
                colls.isTrigger = true;
                switch (colls.tag) {
                    case "Crush": case "Jump_up":
                    case "Wing": case "Sheild":
                        SpriteRenderer rend = colls.GetComponentInChildren<SpriteRenderer>();
                        rend.enabled = false;
                        break;
                }
            }

            is_falling = true;
            anim.SetTrigger("fall");
            this.rigidbody2D.fixedAngle = false;
            speed = 5;
        }
        //check item
        CheckItem();

        //move
		if(moveDir!=Vector3.zero){
			currentSpeed = speed * moveDir;
	        transform.position += (Time.deltaTime * currentSpeed);
		}else{
			currentSpeed *= 1-(slip * Time.deltaTime);
			transform.position += (Time.deltaTime * currentSpeed);
		}

        //cam move
        cam.transform.position = new Vector3(0,Mathf.Clamp(transform.position.y,3f,Mathf.Infinity), -1);
    }

    void FixedUpdate()
    {
        if (is_jumping)
        {
            rigidbody2D.velocity = (new Vector2(0f, jumpForce));
            if (is_crush)
            {
                if (current_tile != null)
                {
                    Destroy(current_tile.gameObject);
                    current_tile = null;
                }
            }
        }
        is_jumping = false;

		if(is_falling){
			rigidbody2D.gravityScale = 0;
			rigidbody2D.velocity = new Vector2(0, -3);
		}
        if (is_wing)
        {
            //rigidbody2D.gravityScale = 0;
            rigidbody2D.velocity = new Vector2(0, 30f);
        }
        if (time_wing < 0.0f)
        {
<<<<<<< HEAD
            foreach (Collider2D colls in Colliders) {
=======
            foreach (Collider2D colls in PlayerColliders) {
>>>>>>> origin/master
                if (is_grounded == true && rigidbody2D.velocity.y < 0)
                    colls.enabled = true;
            }
            if (rigidbody2D.velocity.y < 0)
                anim.SetBool("flying", false);
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "tiles")
        {
            if (is_grounded)
            {
                max_h = coll.transform.position.y;
                if (is_crush)
                {
                    current_tile = coll.collider;
<<<<<<< HEAD
                }            
=======
                }
>>>>>>> origin/master
            }
        }
        if((coll.gameObject.tag == "main_ground") && is_falling){
            SendMessage("kill", 3);

            /////////////////////GAME OVER EFFECT HERE//////////////////////////
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
            jumpForce = rocketBoots;
        }
        else
        {
            jumpForce = bareFoot;
        }
        if (is_wing)
        {
            time_wing -= Time.deltaTime;
            anim.SetBool("flying", true);
            speed = 5;
            if (time_wing < 0.0f)
            {
                is_wing = false;
<<<<<<< HEAD

=======
                anim.SetBool("flying", false);
>>>>>>> origin/master
                speed = 2;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (is_falling == false)
        {
            switch (coll.tag)
            {
                case "Crush":
                    is_crush = true;
                    time_crush = 5.0f;
                    Destroy(coll.gameObject);
					SoundEffectsHelper.Instance.MakeItemGetSound();
					break;
                case "Jump_up":
                    is_jumpup = true;
                    time_jumpup = 5.0f;
                    Destroy(coll.gameObject);
                	SoundEffectsHelper.Instance.MakeItemGetSound();
                    break;
                case "Wing":
                    is_wing = true;
                    time_wing = 1.5f;

                    foreach (Collider2D colls in PlayerColliders)
                    {
                        colls.enabled = false;
                    }
                	SoundEffectsHelper.Instance.MakeItemGetSound();
                    Destroy(coll.gameObject);
                    break;
                case "Sheild":
                    num_sheild++;
                    Destroy(coll.gameObject);
	                SoundEffectsHelper.Instance.MakeItemGetSound();
                    break;
            }        }

        if (coll.tag == "tiles") {
            if (is_falling == true) {
                if (num_sheild > 0) {
                    num_sheild--;
                }
                else {
                    rigidbody2D.velocity -= new Vector2(0, 0.5f); //속도 늦춤
                    //부숴지는 이펙트 설정 (?)
                    Destroy(coll.gameObject, 1); //진짜로 부숨
                    SoundEffectsHelper.Instance.MakeCrashSound();
                }
            }
        }
    }
}
