using NewHorizons.Components;
using NewHorizons.Utility;
using System.Collections;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class LeaderHandler : MonoBehaviour
    {
        [SerializeField]
        public GameObject leaderDialogueIntro; // leader intro dialogue
        [SerializeField]
        public GameObject leaderDialogueAfter; // leader dialogue after vision
        [SerializeField]
        public GameObject leaderDialogueIntroKnowName; // leader intro, but you know cypress's name
        [SerializeField]
        public GameObject[] othersDialogue; // all the others' dialogue

        private GameObject _leader; // leader in the partyhouse
        private GameObject _vesselLeader; // leader in the vessel
        private GameObject _vessel; // the vessel itself
        private GameObject _vesselReveal; // the shiplog condition to reveal vessel
        private GameObject _music; // final end times music
        private VesselSpawnPoint _vesselSpawn; // to store vessel spawn point

        protected PlayerSpawner _spawner; // for spawning the player
        private bool _hasWarped; // sets boolean to make sure warp doesn't happen repeatedly on update.
        public const float blinkTime = 0.5f; // constant for blink time
        public const float animTime = blinkTime / 2f; // constant for blink animation time

        public void Start()
        {
            // getting gameobjects
            _leader = SearchUtilities.Find("StrangersHomeworld_Body/Sector/EntryAndPartyHouse/Sector_PartyHouse/Ghostbird_NPCs/Prefab_IP_GhostBird_ScientistDescendant");
            _vesselLeader = SearchUtilities.Find("RingedGiant_Body/Sector/Prefab_IP_GhostBird_ScientistDescendant_Vessel1");
            _vessel = SearchUtilities.Find("RingedGiant_Body/Sector/Vessel_Body");
            _music = SearchUtilities.Find("FinalEndTimes_STR");

            _hasWarped = false; // sets has warped boolean to false at start of every loop
            leaderDialogueAfter.SetActive(false);

            if (_vessel != null)
            {
                Locator.GetShipLogManager().RevealFact("HOME_VESSEL");
            } else
            {
                PlayerData.SetPersistentCondition("CYPRESS_BOARDVESSEL", false);
            }
        }

        private IEnumerator Blink()
        {
            var cameraEffectController = FindObjectOfType<PlayerCameraEffectController>(); // gets camera controller

            // disable dialogue to prevent softlock
            Destroy(leaderDialogueIntro);
            Destroy(leaderDialogueAfter);
            Destroy(leaderDialogueIntroKnowName);

            // close eyes
            cameraEffectController.CloseEyes(animTime); // closes eyes
            yield return new WaitForSeconds(animTime);  // waits until animation stops to proceed to next line
            GlobalMessenger.FireEvent("PlayerBlink"); // fires an event for the player blinking

            // move leader to vessel
            _leader.SetActive(false);
            _vesselLeader.SetActive(true);
            
            // warp to vessel
            _vesselSpawn = SearchUtilities.Find("RingedGiant_Body/Sector/Vessel_Body/SPAWN_Vessel").GetComponent<VesselSpawnPoint>(); // gets vessel spawn point
            _spawner = GameObject.FindGameObjectWithTag("Player").GetRequiredComponent<PlayerSpawner>(); // gets player spawner
            _spawner.DebugWarp(_vesselSpawn); // warps you to vessel

            // open eyes
            cameraEffectController.OpenEyes(animTime, false); // open eyes
            yield return new WaitForSeconds(animTime); //  waits until animation stops to proceed to next line
            _hasWarped = true; // sets has warped to true so this doesn't run constantly (because it's being called in update)
        }

        public void Update()
        {
            if (HasMetCypress())
            {
                leaderDialogueIntroKnowName.SetActive(true); // if you met cypress, enable the dialogue with his name known
                leaderDialogueIntro.SetActive(false); // if you met cypress, disable the dialogue with his name unknown
                foreach (var item in othersDialogue)
                {
                    item.SetActive(true); // enables everyone else's dialogue in the building if you met cypress
                }
            }
            else
            {
                leaderDialogueIntroKnowName.SetActive(false); // if you didn't meet cypress yet, disable the dialogue with his name known.
                leaderDialogueIntro.SetActive(true); // if you didn't meet cypress yet, enable the dialogue with his name unknown.
                foreach (var item in othersDialogue)
                {
                    item.SetActive(false); // disables everyone's dialogue in the building if you didn't meet cypress yet
                }
            }

            // check if vision torch is was used
            if (!IsViewingProjector() && SawVisionCondition())
            {
                leaderDialogueIntro.SetActive(false); // if already enabled, disable the intro dialogue before meeting cyprus
                leaderDialogueIntroKnowName.SetActive(false); // if already enabled, disable the intro dialogue after meeting cyprus
                leaderDialogueAfter.SetActive(true); // enables remote after using torch
                // using a condition to check if cypress hasn't been met yet, so that it doesn't run this any loop after.
            }
            else
            {
                leaderDialogueAfter.SetActive(false); // sets after-scan dialogue to disabled unless vision torch is scanned.
            }

            // check if vessel is active
            if (_vessel != null)
            {
                _music.SetActive(true); // enables final end times music
                if (HasCypressBoardedVessel())
                {
                    if (!_hasWarped)
                    {
                        StartCoroutine(Blink()); // blink coroutine that warps you and cypress to vessel if you convince cypress to board it. should only run once.
                    }
                }
                else
                {
                    _leader.SetActive(true); // makes sure cypress is enabled in his default spot
                    _vesselLeader.SetActive(false); // makes sure cypress in the vessel is disabled
                }
            } else
            {
                Destroy(_vesselLeader); // gets rid of vessel leader so he's not just floating there when the vessel is gone
                Destroy(_vesselReveal); // destroy ship log that confirms you warped the vessel when vessel is not there to begin with
                _music.SetActive(false); // disable final end times music when the vessel is not there
            }
        }

        private bool IsViewingProjector()
        {
            return PlayerState.IsViewingProjector();
        }
        private bool SawVisionCondition()
        {
            return DialogueConditionManager.SharedInstance.GetConditionState("CYPRESS_TORCH");
        }

        private bool HasMetCypress()
        {
            return Locator.GetShipLogManager().IsFactRevealed("HOME_LEADER_E");
        }

        private bool HasCypressBoardedVessel()
        {
            return DialogueConditionManager.SharedInstance.GetConditionState("CYPRESS_BOARDVESSEL");
        }

        private bool MetCypressCondition()
        {
            return PlayerData.GetPersistentCondition("CYPRESS_MET");
        }
    }
}