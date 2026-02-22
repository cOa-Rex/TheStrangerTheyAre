using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class ByeByeInterloper : MonoBehaviour
    {
        private GameObject _interloper; // creates variable to store the interloper
        private GameObject _nomShuttle;

        public void Awake()
        {
            _interloper = SearchUtilities.Find("Comet_Body/Sector_CO"); // gets the interloper
            _nomShuttle = SearchUtilities.Find("Comet_Body/Prefab_NOM_Shuttle");
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            // checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                _interloper.SetActive(false); // deactivates object when inside the trigger
                _nomShuttle.SetActive(false);
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            // checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                _interloper.SetActive(true); // activates object when outside the trigger
                _nomShuttle.SetActive(true);
            }
        }
    }
}