using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class RemoveSimWall : MonoBehaviour
    {
        private GameObject _simWall; // to store sim wall
        private bool _isInTrigger = false;

        public void Start()
        {
            _simWall = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_DreamZone_4/Simulation_DreamZone_4/Effects_DreamZone_4_Upper/Effects_IP_SIM_BoundaryCylinder"); // get simulation wall
        }

        public void Update()
        {
            if (_isInTrigger)
            {
                _simWall.SetActive(false); // disables sim wall when in trigger
            }
            else
            {
                _simWall.SetActive(true); // enables sim wall  when outside of trigger
            }
        }
        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                _isInTrigger = true;                
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            //checks if player exits with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                _isInTrigger = false;
            }
        }
    }
}