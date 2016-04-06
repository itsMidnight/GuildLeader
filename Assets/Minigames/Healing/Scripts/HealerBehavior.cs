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
    protected ProgressBar progressBar;
    protected const float MaxMp = 100.0f;
    protected const float SpellCost = 20.0f;
    protected float mpRefreshRate = 12.0f;
    protected float currentMp = MaxMp;

    void Start ()
    {
        transform.localScale = new Vector3(scaleFactor, scaleFactor);
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        healSound = GetComponent<AudioSource>();
        direction = HealerDirection.Stationary;

        progressBar = transform.FindChild("progressbar").FindChild("bar").GetComponent<ProgressBar>();
        progressBar.amountFilled = 1.0f;
    }

    protected void ShootHealSpell()
    {
        if (currentMp <= SpellCost)
        {
            // Heals don't work, not enough MP!
            return;
        }
        currentMp -= SpellCost;

        GameObject healSpellClone = (GameObject)Instantiate(healSpell, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);//, transform.position, Quaternion.identity);
        var rb = healSpellClone.GetComponent<Rigidbody2D>();
        
        // Speed of spell is 5 (shooting upward only)
        rb.velocity = new Vector2(0f, 10.0f);
        healSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        
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
            ShootHealSpell();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Go left");
            direction = HealerDirection.Left;
            body.AddForce(new Vector2(baseSpeed * -1.0f, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Go right");
            direction = HealerDirection.Right;
            body.AddForce(new Vector2(baseSpeed , 0));
        }
        else
        {
            Debug.Log("Stop");
            direction = HealerDirection.Stationary;
            body.velocity = Vector3.zero;
        }
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        currentMp += mpRefreshRate * Time.deltaTime;
        currentMp = currentMp > MaxMp ? MaxMp : currentMp;
        progressBar.amountFilled = currentMp / MaxMp;
        Debug.Log("Current MP: " + currentMp);
    }
}