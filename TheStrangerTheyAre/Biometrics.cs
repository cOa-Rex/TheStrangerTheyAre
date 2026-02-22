using System.Collections;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class Biometrics : MonoBehaviour
    {
        [SerializeField]
        public GameObject scanner;
        [SerializeField]
        public AudioSource scanAudio;
        [SerializeField]
        public AudioSource stopAudio;
        [SerializeField]
        public GameObject pass;
        [SerializeField]
        public AudioSource passAudio;
        [SerializeField]
        public GameObject fail;
        [SerializeField]
        public AudioSource failAudio;
        [SerializeField]
        public RotatingDoor door;

        private Animator _scanAnim;
        private bool _isInTrigger;
        private bool _isScanning;

        public void Start()
        {
            _scanAnim = scanner.GetComponent<Animator>();
            scanner.SetActive(false);
            pass.SetActive(false);
            fail.SetActive(true);
        }

        public IEnumerator DelayedCheck()
        {
            _isScanning = true;
            yield return new WaitForSeconds(5); // wait until scan is done

            if (_isInTrigger)
            {
                if (Locator.GetToolModeSwapper().GetItemCarryTool().GetHeldItemType() == TheStrangerTheyAre.GhostbirdSkullItemType)
                {
                    scanner.SetActive(false);
                    pass.SetActive(true);
                    fail.SetActive(false);
                    passAudio.Play();
                    door.Open();
                    Locator.GetShipLogManager().RevealFact("CRIMSON_SPINLAB_BIO");
                    _isScanning = false;
                }
                else
                {
                    scanner.SetActive(false);
                    pass.SetActive(false);
                    fail.SetActive(true);
                    failAudio.Play();
                    door.Close();
                    _isScanning = false;
                }
            }
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                _isInTrigger = true;
                scanner.SetActive(true);
                scanAudio.Play();
                _scanAnim.Play("scan", 0);
                StartCoroutine(DelayedCheck());
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            //checks if player exits the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                _isInTrigger = false;
                if (_isScanning)
                {
                    StopCoroutine(DelayedCheck());
                    _scanAnim.Play("scanstop", 0);
                    scanAudio.Stop();
                    stopAudio.Play();
                }
            }
        }
    }
}