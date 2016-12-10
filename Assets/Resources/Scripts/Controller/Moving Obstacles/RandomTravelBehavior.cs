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
        public double turnProbability = 10;

        /// <summary>
        /// The rate at which
        /// </summary>
        public float speed = 0.2f;

        /// <summary>
        /// Container for random movement value.
        /// </summary>
        private double curVal = 0;

        // Update is called once per frame
        private void Update()
        {
            curVal = random.Next(1, 101);
            if (curVal >= turnProbability)
            {
                switch (direction2D)
                {
                    case DIRECTION2D.UP:
                        transform.position = new Vector3(transform.position.x * speed, transform.position.y, transform.position.z);
                        break;

                    case DIRECTION2D.DOWN:
                        transform.position = new Vector3(transform.position.x * speed * -1, transform.position.y, transform.position.z);
                        break;

                    case DIRECTION2D.LEFT:
                        transform.position = new Vector3(transform.position.x, transform.position.y * speed, transform.position.z);
                        break;

                    case DIRECTION2D.RIGHT:
                        transform.position = new Vector3(transform.position.x, transform.position.y * speed * -1, transform.position.z);
                        break;
                }
            }
        }
    }
}