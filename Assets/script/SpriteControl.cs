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

    public float initFallSpeed;
    private float FallSpeed;
    public float woodSlower;
    bool is_gameover;
	private GameObject shieldEffect;
	private GameObject shoeEffect;
    private Transform groundCheck;

    bool is_jumping = false;
    bool is_grounded = false;

    bool is_falling = false;

    private float time_crush;
    bool is_crush;
    private float time_wing;
    bool is_wing;
    private float time_jumpup;
    bool is_jumpup;
    public static int num_sheild;

    private float max_h;
    private float wing_speed;

    private bool Gameover;
    private Transform cam;
    Collider2D[] PlayerColliders = null;
    Collider2D[] TilesColliders = null;
    Collider2D current_tile = null;
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        groundCheck = transform.Find("GroundCheck");
        cam = Camera.main.transform;
        max_h = 0;
        FallSpeed = initFallSpeed;
        is_gameover = false;
    }
    void Start() {
        PlayerColliders = gameObject.GetComponents<Collider2D>();
		shieldEffect = transform.Find("shieldEffect").gameObject;
		shoeEffect = transform.Find("shoeEffect").gameObject;
	}
    void Update()
    {
        Vector3 moveDir = Vector3.zero;

        is_grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("ground"));

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

        if (is_grounded == true)
            anim.SetBool("inAir", false);
        if (inputJump != 0)
            if (is_jumping == false && is_grounded == true)
            {
                anim.SetBool("inAir", true);
                is_jumping = true;
            }
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
            speed = 5;
        }
        //check item
        CheckItem();

        //move
        if (!is_gameover)
        {
            if (moveDir != Vector3.zero)
            {
                currentSpeed = speed * moveDir;
                transform.position += (Time.deltaTime * currentSpeed);
            }
            else
            {
                currentSpeed *= 1 - (slip * Time.deltaTime);
                transform.position += (Time.deltaTime * currentSpeed);
            }
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -2.2f, 2.2f), transform.position.y);
        }
        //cam move
        if (Gameover == false)
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
                    SoundEffectsHelper.Instance.MakeCrashSound();
                    current_tile = null;
                }
            }
        }
        is_jumping = false;

		if(is_falling){
			rigidbody2D.gravityScale = 0;
            FallSpeed += Time.deltaTime;
            rigidbody2D.velocity = new Vector2(0, - FallSpeed);
		}
        if (is_wing)
        {
            wing_speed -= Time.deltaTime * 10;
            rigidbody2D.velocity = new Vector2(0, wing_speed);
            if (0.0f > wing_speed)
            {
                foreach (Collider2D colls in PlayerColliders)
                {
                        colls.enabled = true;
                        
                   
                }
                anim.SetBool("flying", false);
                is_wing = false;
                speed = 2;
            }
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
                }
            }
        }
        if((coll.gameObject.tag == "main_ground") && is_falling){
            if (!is_gameover)
            {
                is_gameover = true;
                SoundEffectsHelper.Instance.MakeSplatSound();
                if (FallSpeed < 4)
                    SendMessage("kill", 0);
                else if (FallSpeed < 6)
                    SendMessage("kill", 1);
                else if (FallSpeed < 8)
                    SendMessage("kill", 2);
                else if (FallSpeed < 10)
                    SendMessage("kill", 3);
                else
                    SendMessage("kill", 4);
            }
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
            anim.SetBool("flying", true);
            speed = 5;
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
					shoeEffect.GetComponent<ParticleSystem>().Play();
					SoundEffectsHelper.Instance.MakeItemGetSound();
                    break;
                case "Wing":
                    is_wing = true;
                    time_wing = 1.5f;
                    wing_speed = 30.0f;
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
					shieldEffect.GetComponent<ParticleSystem>().Play();
					SoundEffectsHelper.Instance.MakeItemGetSound();
                    break;
            }        
		}

        if (coll.tag == "tiles") {
            if (is_falling == true) {
                if (num_sheild > 0) {
                    num_sheild--;
					shieldEffect.GetComponent<ParticleSystem>().Play();
                }
                else {
                    rigidbody2D.velocity -= new Vector2(0, 0.5f); //속도를 늦춤
                    Destroy(coll.gameObject);
                    SoundEffectsHelper.Instance.MakeCrashSound();
                    FallSpeed -= woodSlower;
                    if (FallSpeed < initFallSpeed)
                        FallSpeed = initFallSpeed;
                }
            }
        }
    }

    public static int GetNumSheild()
    {
        int num = num_sheild;
        return num;
    }
}
