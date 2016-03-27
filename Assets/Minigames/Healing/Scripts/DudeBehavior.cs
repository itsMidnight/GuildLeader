using UnityEngine;
using System.Collections;

public class DudeBehavior : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 20f;
    // How much this dude scales.
    protected float scaleFactor = 10f;
    // How fast this dude moves.
    public float baseSpeed = 10;

    protected Rigidbody2D body;
    protected SpriteRenderer sprite;
    private int updateCounter = 0;
    public Transform startMarker;
    public Transform endMarker;
    public float speed = 1.0F;
    private float startTime;
    protected int healCount = 0;
    protected int damagedCount = 0;
    protected bool isDead = false;
    protected static object isDeadLock = new object();
    protected ProgressBar progressBar;
    // Difficulty 1-3
    // Output: win/lose
    // Input: difficulty (1-3)
    // Optional Input: 

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        progressBar = transform.FindChild("progressbar").FindChild("bar").GetComponent<ProgressBar>();

        startTime = Time.time;
        transform.localScale = new Vector3(scaleFactor, scaleFactor);
        // Fill-up health bar by default.
        progressBar.amountFilled = 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead) return;

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

        Move(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(5f, 15f));
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
        sprite.color = Color.red;
        isDead = true;
    }

    public void Move(float x, float y, float force)
    {
        //sprite.transform.Translate(new Vector3(x, y));
        body.AddForce(force * new Vector2(x, y) * speed);
    }

    public void Damaged(int damageAmount = 10)
    {
        Debug.Assert(damageAmount > 0);

        Debug.Log("Damaged");
        // Fuck, I got hit, shrink!
        ChangeHealth(-damageAmount);
    }

    protected void ChangeHealth(float changeAmt)
    {
        lock (isDeadLock)
        {
            if (!isDead)
            {
                currentHealth += changeAmt;
                currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
                currentHealth = currentHealth < 0 ? 0 : currentHealth;

                if (currentHealth == 0)
                {
                    // Dead! FUCK!
                }
                else if (currentHealth == maxHealth)
                {
                    // Full health! WOO
                    // TODO: Do something cool
                }
                else
                {
                    // Scale up/down by change amount.
                    float pctOfMax = currentHealth / maxHealth;
                    progressBar.amountFilled = pctOfMax;
                    //StartCoroutine(LerpUp(pctOfMax * scaleFactor, 2.0f));
                    
                    //var rend = healthBar.GetComponent<SpriteRenderer>();
                    //rend.transform.localScale = new Vector3(pctOfMax, rend.transform.localScale.y);
                }
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