using UnityEngine;

namespace TheStrangerTheyAre
{
    public class IgnoreEyeEndlessVolume : MonoBehaviour
    {
        [SerializeField]
        public GameObject ring; // creates variable to store the ringed planet

        private GameObject _endlessVolume; // creates variable to store the endless eye volume

        public void Start()
        {
            // gets the endless eye volume
            var fireVol = GameObject.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/Volumes_Campfire"); // gets the quantum planet with nh
            _endlessVolume = fireVol.transform.Find("EndlessCylinder_Forest").gameObject; // gets the endless eye volume

            // runs two frames later so it can do sectoring stuff
            TheStrangerTheyAre.Instance.ModHelper.Events.Unity.FireInNUpdates(() => {
                ring.SetActive(false); // deactivates object when outside the trigger
            }, 2);
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                _endlessVolume.SetActive(false); // deactivates object when inside the trigger
                ring.SetActive(true); // activates object when inside the trigger
            }
        }
    }
}