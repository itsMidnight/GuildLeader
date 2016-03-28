using UnityEngine;
using System.Collections;

public enum HealerDirection
{
    Stationary = 0,
    Left,
    Right,
}

public class HealerBehavior : MonoBehaviour {
    protected SpriteRenderer sprite;
    protected Rigidbody2D body;
    // How much this dude scales.
    protected float scaleFactor = 10f;
    public float baseSpeed = 10;
    protected HealerDirection direction;
    public GameObject healSpell;
    public AudioSource healSound;
	// Use this for initialization

	void Start ()
    {
        transform.localScale = new Vector3(scaleFactor, scaleFactor);
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        healSound = GetComponent<AudioSource>();
        direction = HealerDirection.Stationary;
    }

    protected void ShootHealSpell()
    {
        //Rigidbody2D healSpellClone = (Rigidbody2D)Instantiate(healSpell, transform.position, Quaternion.identity);
        GameObject healSpellClone = (GameObject)Instantiate(healSpell, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);//, transform.position, Quaternion.identity);
        var rb = healSpellClone.GetComponent<Rigidbody2D>();
        //rectTrans = new_card.GetComponent<RectTransform>();

        // Speed of spell is 5 (shooting upward only)
        rb.velocity = new Vector2(0f, 15.0f);
        healSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        //anim.SetFloat("speed", h);
        //Debug.Log(h);
        if (h > 0.1f)
        {
            sprite.flipX = false;
        }
        else if (h < -0.1f)
        {
            sprite.flipX = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            //rb2d.AddForce(new Vector2(1, jumpPower));
            ShootHealSpell();
        }

        //if (direction == HealerDirection.Right && Input.GetKey(KeyCode.LeftArrow) ||
        //    direction == HealerDirection.Left && Input.GetKey(KeyCode.RightArrow))
        //{
        //    // Stop, turnaround
        //    Debug.Log("Stop");
        //    direction = HealerDirection.Stationary;
        //    body.velocity = Vector3.zero;
        //}
        //else if (direction != HealerDirection.Stationary && (!Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.RightArrow)))
        //{
        //    // Stop!
        //    Debug.Log("Stop");
        //    direction = HealerDirection.Stationary;
        //    body.velocity = Vector3.zero;
        //}

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Go left");
            direction = HealerDirection.Left;
            //body.AddForce(new Vector2(h * baseSpeed, 0));
            body.AddForce(new Vector2(baseSpeed * -1.0f, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Go right");
            direction = HealerDirection.Right;
            //body.AddForce(new Vector2(h * baseSpeed, 0));
            body.AddForce(new Vector2(baseSpeed , 0));
        }
        else
        {
            Debug.Log("Stop");
            direction = HealerDirection.Stationary;
            body.velocity = Vector3.zero;
            //var move = new Vector3(h, 1);
            //transform.position += move * baseSpeed * Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        //body.AddForce(new Vector2(h * baseSpeed, 0));

        
    }
}
