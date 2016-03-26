using UnityEngine;
using System.Collections;

public class DudeBehavior : MonoBehaviour {
    public float maxHealth = 100f;
    public float currentHealth = 20f;
    // How much this dude scales.
    public float scaleFactor = 5f;
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

    // Difficutly 1-3
    // Output: win/lose
    // Input: difficulty (1-3)
    // Optional Input: 

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
        startTime = Time.time;
        transform.localScale = new Vector3(scaleFactor, scaleFactor);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        updateCounter++;
        // Depending on health, scale x,y of dude
        if (updateCounter % 125 == 0)
        {
            if (0.5 < Random.Range(0f, 1f))
            {
                Healed();
            }
            else
            {
                Damaged();
            }
            
            updateCounter = 0;
        }        
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

    public void Damaged(int damageAmount = 10)
    {
        Debug.Assert(damageAmount > 0);

        Debug.Log("Damaged");
        // Fuck, I got hit, shrink!
        ChangeHealth(-damageAmount);
    }

    protected void ChangeHealth(float changeAmt)
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
            StartCoroutine(LerpUp(pctOfMax * scaleFactor, 2.0f));
        }
    }

    IEnumerator LerpUp(float growthScale, float timeScale)
    {
        Vector3 initialScale = transform.localScale;                
        Vector3 finalScale = new Vector3(growthScale , growthScale , timeScale);
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
