using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterMovementBehavior : MonoBehaviour
    {
        // Queue of movements to make. Includes x, y and speed.
        protected Queue<Vector3> wayPoints;
        protected Vector3 currentWayPoint;
        protected Rigidbody2D rigidBody;

        // These are used to flag a move whether a move is in progress.
        protected static object lockMoving = new object();
        protected bool isMoving;

        // All characters have a base speed
        protected float baseSpeed = 5.0f;

        // Should I use an animation when moving?
        bool isMoveAnmiated = false;

        protected bool ColliderEnabled
        {
            get
            {
                // Not sure this works.
                return rigidBody.GetComponent<Collider2D>().enabled;
            }
            set
            {
                // Not sure this works.
                rigidBody.GetComponent<Collider2D>().enabled = value;
            }
        }



        void Start()
        {
            // Characters need starting locations, and they should probably
            // know where they are.

            // 
            rigidBody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {

        }

        void FixedUpdate()
        {
            // Draw movement 
            if (!isMoving)
            {
                lock (lockMoving)
                {
                    if (!isMoving)
                    {
                        // Move me somewhere on (or off) the screen.

                        // TODO: Should I trigger something when I move off screen?

                        // TODO: What do I do when I get there?
                    }
                }
            }

            if (currentWayPoint != null)
            {
                Move(currentWayPoint.x, currentWayPoint.y, currentWayPoint.z);
            }

        }

        /// <summary>
        /// Where this character should next move.
        /// </summary>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        /// <param name="speed"></param>
        /// <param name="processImmediately">If true, dumps waypoint queue and processes immediately.</param>
        public void Move(float newX, float newY, float force, bool processImmediately = false)
        {
            rigidBody.AddForce(force * new Vector2(newX, newY) * Time.deltaTime * baseSpeed);            
        }
    }
}
