using UnityEngine;

namespace TheStrangerTheyAre
{
    public class EyeScientistTrigger : MonoBehaviour
    {
        [SerializeField]
        public GameObject sci1; // creates variable to store scientist1
        [SerializeField]
        public GameObject sci2; // creates variable to store scientist2

        private bool _triggerHit;

        public void Awake()
        {
            _triggerHit = false;
        }

        public void Update()
        {
            if (this.gameObject.activeSelf && !_triggerHit)
            {
                sci1.SetActive(true); // activates object when inside the trigger
                sci2.SetActive(false); // activates object when inside the trigger
            }
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                _triggerHit=true;
                sci1.SetActive(false); // activates object when inside the trigger
                sci2.SetActive(true); // activates object when inside the trigger
            }
        }
    }
}