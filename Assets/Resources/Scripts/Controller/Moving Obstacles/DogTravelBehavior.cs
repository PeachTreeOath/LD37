using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class DogTravelBehavior : RandomTravelBehavior
    {

        private SpriteRenderer sprite;

        /// <summary>
        /// Handles moving the position of the model in the direction and speed provided.
        /// </summary>
        protected override void MovePosition()
        {
            if(sprite == null)
            {
                sprite = GetComponent<SpriteRenderer>();
            }

            switch (direction2D)
            {
                case DIRECTION2D.UP:
                    nextPosition = new Vector3(transform.position.x + speed,
                        transform.position.y, transform.position.z);
                    sprite.flipX = false;
                    break;

                case DIRECTION2D.DOWN:
                    nextPosition = new Vector3(transform.position.x + (speed * -1),
                        transform.position.y, transform.position.z);
                    sprite.flipX = true;
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