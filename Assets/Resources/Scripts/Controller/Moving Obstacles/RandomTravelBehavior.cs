using System;
using UnityEngine;

namespace Controller
{
    /// <summary>
    /// Supports driving a model in random direction across the screen. Uses the
    /// turn probability level to determine how frequently the model randomly
    /// changes it's direction.
    /// </summary>
    public class RandomTravelBehavior : ATraveler
    {
        /// <summary>
        /// The probability at which a model will roll for a different direction.
        /// Represented as a value from 1-100. The larger the number, the greater
        /// the chance of the model turning.
        /// </summary>
        public double turnProbability = 5;

        /// <summary>
        /// The rate at which the model moves
        /// </summary>
        public float speed = 0.001f;

        /// <summary>
        /// The delay in ms between model position updates.
        /// </summary>
        public float msDelay = 500f;

        /// <summary>
        /// Tracks the time passed for the delay.
        /// </summary>
        private float timePassed = 0;

        /// <summary>
        /// Track previous position to lerp from.
        /// </summary>
        private Vector3 currPosition;

        /// <summary>
        /// Track next position to lerp to.
        /// </summary>
        private Vector3 nextPosition;

        private void Start()
        {
            currPosition = nextPosition = transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            timePassed += Time.deltaTime * 1000;

            if (timePassed >= msDelay)
            {
                currPosition = nextPosition;
                if (random.Next(1, 101) >= turnProbability)
                {
                    RandomizeDirection2D();
                }
                MovePosition();
                timePassed = 0;
            }

            transform.position = Vector3.Lerp(currPosition, nextPosition, timePassed / msDelay);
        }

        /// <summary>
        /// Handles moving the position of the model in the direction and speed provided.
        /// </summary>
        private void MovePosition()
        {
            switch (direction2D)
            {
                case DIRECTION2D.UP:
                    nextPosition = new Vector3(transform.position.x + speed,
                        transform.position.y, transform.position.z);
                    break;

                case DIRECTION2D.DOWN:
                    nextPosition = new Vector3(transform.position.x + (speed * -1),
                        transform.position.y, transform.position.z);
                    break;

                case DIRECTION2D.LEFT:
                    nextPosition = new Vector3(transform.position.x,
                        transform.position.y + speed, transform.position.z);
                    break;

                case DIRECTION2D.RIGHT:
                    nextPosition = new Vector3(transform.position.x,
                        transform.position.y + (speed * -1), transform.position.z);
                    break;
            }
        }
    }
}