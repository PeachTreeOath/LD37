using UnityEngine;

namespace Controller
{
    public class ATraveler : MonoBehaviour
    {
        protected DIRECTION2D direction2D;

        /// <summary>
        /// Private random generator.
        /// </summary>
        protected System.Random random = new System.Random();

        protected float xMultiplier = 1;
        protected float yMultiplier = 1;

        protected enum DIRECTION2D
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        /// <summary>
        /// Randomly sets the 2D direction
        /// </summary>
        protected void RandomizeDirection2D()
        {
            direction2D = (DIRECTION2D)random.Next(1, 5);
        }
    }
}