using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts
{
    public class CharacterCombatBehavior
    {
        public float maxHealth = 100f;
        public float currentHealth = 20f;
        
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
        //protected float difficultyModifier = 1.0f;

        //private int updateCounter = 0;
        private float startTime;
        public delegate void DeathEventHandler(CharacterCombatBehavior dude);

        public event DeathEventHandler CharacterDied;

        void Start()
        {

        }

        void FixedUpdate()
        {

        }

        void Update()
        {

        }

        public bool IsDead
        {
            get
            {
                lock (isDeadLock)
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
            // Commenting RB2D stuff since Chars don't need rigids.
            //rigidBody.velocity = Vector3.zero;
            progressBar.enabled = false;
            var deathHandlerCopy = CharacterDied;
            if (null != deathHandlerCopy)
            {
                deathHandlerCopy.Invoke(this);
            }
        }

        public void Damaged(int damageAmount = 5)
        {
            Debug.Assert(damageAmount > 0);
            Debug.Log("Damaged");
            ChangeHealth(-damageAmount);
            damagedCount++;
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

        /// <summary>
        /// Use for receiving triggers like, heal spells or something.
        /// </summary>
        /// <param name="coll"></param>
        void OnTriggerEnter2D(Collider2D coll)
        {
            // TODO: switch on the collider source to figure out what to do here.

            Healed(10);
        }
    }
}
