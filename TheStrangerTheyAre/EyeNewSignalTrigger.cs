using UnityEngine;

namespace TheStrangerTheyAre
{
    public class EyeNewSignalTrigger : MonoBehaviour
    {
        [SerializeField]
        public GameObject signal; // creates variable to store signal
        [SerializeField]
        public GameObject parentObject; // creates variable to store parent

        private bool _triggerHit;

        public void Awake()
        {
            _triggerHit = false;
        }

        public void Update()
        {
            if (parentObject.activeSelf && !_triggerHit)
            {
                signal.SetActive(true); // activates object when inside the trigger
            }
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                _triggerHit = true;
                signal.SetActive(false); // activates object when inside the trigger
            }
        }
    }
}