using UnityEngine;

namespace TheStrangerTheyAre
{
    public class GhostBirdHandlerTSTA : MonoBehaviour
    {
        [SerializeField]
        public GameObject[] ghostBirds; // array of all ghostbirds, set in unity
        [SerializeField]
        public GameObject[] initialPos; // array for storing each starting position

        private bool _areDeadZone1; // boolean that tests if the ghosts have died.
        private bool _areDeadZone2; // boolean that tests if the ghosts have died.
        private bool _isCaught; // boolean to check if player is caught

        public void Awake()
        {
            GlobalMessenger.AddListener("ExitDreamWorld", OnExitDreamWorld); // checks if player leaves the sim
            GlobalMessenger.AddListener("PlayerGrabbedByGhost", OnPlayerGrabbedByGhost); // checks if player is caught by ghostbird
        }

        public void Start()
        {
            // set all bools to false by default
            _areDeadZone1 = false;
            _areDeadZone2 = false;
            _isCaught = false;

            // get ghost positions
            for (int i = 0; i < ghostBirds.Length; i++)
            {
                if (initialPos[i] != null)
                {
                    initialPos[i].transform.position = ghostBirds[i].transform.position; // gets starting pos of each ghostbird
                    initialPos[i].transform.rotation = ghostBirds[i].transform.rotation; // gets starting rotation of each ghostbird
                }
            }
        }

        public void OnExitDreamWorld()
        {
            for (int i = 0; i < ghostBirds.Length; i++)
            {
                _isCaught = false;
                if (ghostBirds[i] != null)
                {
                    ghostBirds[i].transform.position = initialPos[i].transform.position; // on dw exit, sets to starting pos of each ghostbird
                    ghostBirds[i].transform.rotation = initialPos[i].transform.rotation; // on dw exit, sets to starting rotation of each ghostbird
                    ghostBirds[i].GetComponent<GhostController>().SetLanternConcealed(true, true);
                    ghostBirds[i].GetComponentInChildren<Animator>().Play("Ghostbird_Idle_Unaware", 0); // on dw exit, sets each ghostbird animation to idle
                }
            }
        }

        public void OnPlayerGrabbedByGhost()
        {
            _isCaught = true;
        }

        public void Update()
        {
            if (Locator._toolModeSwapper.GetItemCarryTool().GetHeldItem() is DreamLanternItem lantern && lantern._lanternController._lit)
            {
                if (OWInput.IsPressed(InputLibrary.toolActionPrimary, InputMode.Character) && OWInput.IsPressed(InputLibrary.toolActionSecondary, InputMode.Character))
                {
                    Sneak(); // run sneak method if player is both focusing and contracting lantern
                }
                else
                {
                    NoSneak();
                }
            }

            if (TimeLoop.GetSecondsElapsed() > 790 && !_areDeadZone1)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (ghostBirds[i] != null)
                    {
                        ghostBirds[i].GetComponent<GhostBrain>().Die(); // kill ghostbirds linked to zone 1
                        ghostBirds[i].SetActive(false);
                    }
                };
                _areDeadZone1 = true; // set dead boolean for zone 1 true
            } else if (TimeLoop.GetSecondsElapsed() > 1230 && !_areDeadZone2)
            {
                for (int i = 2; i < 8; i++)
                {
                    if (ghostBirds[i] != null)
                    {
                        ghostBirds[i].GetComponent<GhostBrain>().Die(); // kill ghostbirds linked to zone 2
                    }
                };
                _areDeadZone2 = true; // set dead boolean for zone 2 true
            }
        }
        public void Sneak()
        {
            foreach (var bird in ghostBirds)
            {
                if (bird != null)
                {
                    Vector3 smallTrigger = new Vector3(1, 1, 1);
                    bird.transform.Find("ContactTrigger/ContactTrigger_Core").gameObject.transform.localScale = smallTrigger;
                }
            }
        }

        public void NoSneak()
        {
            foreach (var bird in ghostBirds)
            {
                if (bird != null)
                {
                    if (bird.GetComponentInChildren<CompoundLightSensor>().IsIlluminated() && !_isCaught)
                    {
                        Vector3 giantTrigger = new Vector3(7, 7, 7);
                        bird.transform.Find("ContactTrigger/ContactTrigger_Core").gameObject.transform.localScale = giantTrigger;
                    }
                    else
                    {
                        Vector3 bigTrigger = new Vector3(2, 2, 2);
                        bird.transform.Find("ContactTrigger/ContactTrigger_Core").gameObject.transform.localScale = bigTrigger;
                    }
                }
            }
        }
    }
}