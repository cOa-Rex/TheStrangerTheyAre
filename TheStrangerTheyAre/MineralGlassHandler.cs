using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class MineralGlassHandler : MonoBehaviour
    {
        [SerializeField]
        public GameObject[] objectsOn;
        [SerializeField]
        public GameObject[] objectsOff;

        private CodeTotemPuzzleHandler _codeTotemPuzzle;
        private bool _areObjectsOn;

        public void Start()
        {
            _areObjectsOn = false;
            _codeTotemPuzzle = SearchUtilities.Find("Sector/PreBrambleBase/Interactibles/CodePuzzle").GetComponent<CodeTotemPuzzleHandler>();
            ToggleObjects(true);
        }

        public void Update()
        {
            if (_codeTotemPuzzle.allCodesMatched)
            {
                ToggleObjects(false);
            }
            else if (!_codeTotemPuzzle.allCodesMatched && _areObjectsOn)
            {
                ToggleObjects(true);
            }
        }

        public void ToggleObjects(bool isOn)
        {
            if (isOn)
            {
                _areObjectsOn = true;
                foreach (GameObject objOn in objectsOn)
                {
                    objOn.SetActive(true);
                }
                foreach (GameObject objOff in objectsOff)
                {
                    objOff.SetActive(false);
                }
            } else
            {
                _areObjectsOn = false;
                foreach (GameObject objOn in objectsOn)
                {
                    objOn.SetActive(false);
                }
                foreach (GameObject objOff in objectsOff)
                {
                    objOff.SetActive(true);
                }
            }
        }
    }
}
