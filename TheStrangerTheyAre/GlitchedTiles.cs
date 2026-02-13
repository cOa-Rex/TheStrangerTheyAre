using UnityEngine;

namespace TheStrangerTheyAre
{
    public class GlitchedTiles : MonoBehaviour
    {
        // variables
        [SerializeField]
        public GameObject tiles; // creates variable to store the glitched tiles

        public void Awake()
        {
            tiles.SetActive(false); // deactivates object at start of loop
        }

        public void Update()
        {
            // variables for update function
            var shouldBeActive = TimeLoop.GetSecondsElapsed() > 399 && TimeLoop.GetSecondsElapsed() < 790; // these tiles activate when solar sails get deployed, deactivated when the flood starts
            var isActive = tiles.activeInHierarchy;

            if (shouldBeActive != isActive)
            {
                tiles.SetActive(shouldBeActive); // sets active if the time loop condition is met
            }
        }
    }
}