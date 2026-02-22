using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class AnglerfishCalmingVolume : MonoBehaviour
    {
        private AnglerfishController[] _anglerControllers = new AnglerfishController[2];
        private SocketedQuantumObject[] _anglerQuantumControllers = new SocketedQuantumObject[2];

        public void Start()
        {
            for (int i = 0; i < 2; i++)
            {
                _anglerControllers[i] = SearchUtilities.Find("BASE_ANGLER" + (i + 1)).GetComponent<AnglerfishController>();
                _anglerQuantumControllers[i] = SearchUtilities.Find("BASE_ANGLER" + (i + 1)).GetComponent<SocketedQuantumObject>();
            }
            
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                for (int i = 0; i < 2; i++)
                {
                    _anglerQuantumControllers[i].ChangeQuantumState(true);
                    _anglerControllers[i].ChangeState(AnglerfishController.AnglerState.Lurking);
                }
            }
        }
    }
}