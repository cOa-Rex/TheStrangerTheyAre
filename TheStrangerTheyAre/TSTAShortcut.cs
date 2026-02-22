using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class TSTAShortcut : MonoBehaviour
    {
        // variables
        private GameObject _warpDZ3; // for storing the other warp zone

        public void Start()
        {
            _warpDZ3 = SearchUtilities.Find("TSTA_Shortcut_DZ3"); // gets other warp zone
            _warpDZ3.SetActive(false); // sets it false at the start of the loop
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                _warpDZ3.SetActive(true); // upon player contact, enables the other side.
            }
        }
    }
}