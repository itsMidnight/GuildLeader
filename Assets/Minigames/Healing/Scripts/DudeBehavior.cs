using UnityEngine;
using System.Collections;

public class DudeBehavior : Character
{
    public float maxHealth = 100f;
    public float currentHealth = 20f;
    public Transform startMarker;
    public Transform endMarker;

    // How fast this dude moves.
    public float baseSpeed = 1f;

    // How much this dude scales.
    protected float scaleFactor = 10f;
    protected int healCount = 0;
    protected int damagedCount = 0;
    protected bool isDead = false;
    protected static object isDeadLock = new object();
    protected ProgressBar progressBar;

    // Change this with increase in difficulty. 1.0 is easy mode.
    protected float difficultyModifier = 1.0f;

    private int updateCounter = 0;
    private float startTime;
    public delegate void DeathEventHandler(DudeBehavior dude);
    // Difficulty 1-3
    // Output: win/lose
    // Input: difficulty (1-3)
    // Optional Input: 

    public event DeathEventHandler CharacterDied;

    public bool IsDead
    {
        get
        {
            lock(isDeadLock)
            {
                Debug.Log("isDead: " + isDead);
                return isDead;
            }
        }
        set
        {
            lock (isDeadLock)
            {
                isDead = value;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        Init();
    }
    
    public override void Init()
    {
        base.Init();

        difficultyModifier = Random.Range(1.0f, 1.0f);

        rigidBody = GetComponent<Rigidbody2D>();
        progressBar = transform.FindChild("progressbar").FindChild("bar").GetComponent<ProgressBar>();

        startTime = Time.time;
        transform.localScale = new Vector3(scaleFactor, scaleFactor);
        // Fill-up health bar by default.
        progressBar.amountFilled = 1.0f;

        base.InitializeLookAndFeel(GetRandomSkinTone(), GetRandomSuit(), GetRandomWeapon(), GetRandomEyes());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Uncomment for random movement for testing.
        updateCounter++;
        // Depending on health, scale x,y of dude
        if (updateCounter % 30 == 0)
        {
            if (0.5 < Random.Range(0f, 1f))
            {
                //Healed();
            }
            else
            {
                Damaged();
            }

            updateCounter = 0;
        }

        ////if (updateCounter % 10 == 0)
        ////{
        ////    Move(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(5f, 15f));
        ////}


        // Draw health bar
        progressBar.amountFilled = currentHealth / maxHealth;
    }

    public void Healed(int healAmount = 10)
    {
        Debug.Assert(healAmount > 0);
        Debug.Log("Healed");
        // Got me some heals!
        // Scale up
        healCount++;

        ChangeHealth(healAmount);
    }

    public void Dead()
    {
        IsDead = true;
        Debug.Log("Dead!");
        rigidBody.velocity = Vector3.zero;
        progressBar.enabled = false;
        var deathHandlerCopy = CharacterDied;
        if (null != deathHandlerCopy)
        {
            deathHandlerCopy.Invoke(this);
        }
    }

    public void Move(float x, float y, float force)
    {
        // Can't move if dead, if you want to reposition this dead character, be explicit.
        if (!IsDead)
        {
            rigidBody.AddForce(force * new Vector2(x, y) * Time.deltaTime * baseSpeed * difficultyModifier);
        }
    }

    public void Damaged(int damageAmount = 5)
    {
        Debug.Assert(damageAmount > 0);
        Debug.Log("Damaged");
        ChangeHealth(-damageAmount);
    }

    protected void ChangeHealth(float changeAmt, bool ignoreDeath = false)
    {
        // Health can't change if dead unless overridden.
        if (!IsDead || ignoreDeath)
        {
            currentHealth += changeAmt;
            currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
            currentHealth = currentHealth < 0 ? 0 : currentHealth;

            if (currentHealth <= 0)
            {                
                // Dead! FUCK!
                Dead();
            }
            else if (currentHealth >= maxHealth)
            {
                // Full health! WOO
                // TODO: Do something cool
            }
            else
            {
                
                //StartCoroutine(LerpUp(pctOfMax * scaleFactor, 2.0f));

                //var rend = healthBar.GetComponent<SpriteRenderer>();
                //rend.transform.localScale = new Vector3(pctOfMax, rend.transform.localScale.y);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Healed(10);
    }

    IEnumerator LerpUp(float growthScale, float timeScale)
    {
        Vector3 initialScale = transform.localScale;
        Vector3 finalScale = new Vector3(growthScale, growthScale, timeScale);
        float progress = 0;
        while (progress <= 1)
        {
            transform.localScale = Vector3.Lerp(initialScale, finalScale, progress);
            progress += Time.deltaTime * timeScale;
            yield return null;
        }

        transform.localScale = finalScale;
    }
}