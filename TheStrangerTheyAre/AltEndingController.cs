using NewHorizons.Utility;
using System.Collections;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class AltEndingController : MonoBehaviour
    {
        // declare variables
        private GameObject _cypressVessel;
        private GameObject _endTimesAudio;
        private GameObject _vesselDiscoverAudio;
        private GameObject _cypressDialogue;

        // warp stuff again
        private PlayerSpawner _spawner; // for spawning the player

        private bool _hasWarped; // sets boolean to make sure warp doesn't happen repeatedly on update.

        public const float blinkTime = 0.5f; // constant for blink time
        public const float animTime = blinkTime / 2f; // constant for blink animation time

        public void Start()
        {
            // define variables
            _cypressVessel = SearchUtilities.Find("Sector_VesselDimension/Prefab_IP_GhostBird_ScientistDescendant_Vessel1");
            _cypressDialogue = SearchUtilities.Find("Sector_VesselDimension/Prefab_IP_GhostBird_ScientistDescendant_Vessel1");
            _endTimesAudio = SearchUtilities.Find("Sector_VesselDimension/Sector_VesselBridge/FinalEndTimes_ALTEND");
            _vesselDiscoverAudio = SearchUtilities.Find("Sector_VesselDimension/Volumes_VesselDimension/VesselDiscoveryMusicTrigger");
            _hasWarped = false;

            if (Check()) {
                // destroy vessel discover audio
                Destroy(_vesselDiscoverAudio);
            } else
            {
                // destroy all altending objects
                Destroy(_endTimesAudio);
                Destroy(_cypressVessel);
            }
        }

        public void Update()
        {
            if (ReadyToWarpToSim() && !_hasWarped)
            {
                Destroy(_cypressDialogue);
                StartCoroutine(Blink());
            }
        }

        private IEnumerator Blink()
        {
            _hasWarped = true; // sets has warped to true so this doesn't run constantly (because it's being called in update)
            var cameraEffectController = FindObjectOfType<PlayerCameraEffectController>(); // gets camera controller

            // close eyes
            cameraEffectController.CloseEyes(animTime); // closes eyes
            yield return new WaitForSeconds(animTime);  // waits until animation stops to proceed to next line
            GlobalMessenger.FireEvent("PlayerBlink"); // fires an event for the player blinking

            // warp to dreamfire
            _spawner = GameObject.FindGameObjectWithTag("Player").GetRequiredComponent<PlayerSpawner>(); // gets player spawner
            SpawnPoint dreamSpawn = SearchUtilities.Find("Spawn_IP_Zone_3_DreamFire").GetComponent<SpawnPoint>();
            _spawner.DebugWarp(dreamSpawn); // warps you to the dream campfire
            OWItem item = SearchUtilities.Find("Sector_RingWorld/Sector_SecretEntrance/Interactibles_SecretEntrance/Prefab_IP_DreamLanternItem_2").GetComponent<OWItem>(); // get artifact
            Locator.GetToolModeSwapper().GetItemCarryTool().PickUpItemInstantly(item); // gives player the artifact

            SlidingDoor slidingDoor = SearchUtilities.Find("RingWorld_Body/Sector_RingInterior/Sector_Zone3/Sector_HiddenGorge/Sector_DreamFireHouse_Zone3/Interactables_DreamFireHouse_Zone3/VisibleFromFar_Interactables_DreamFireHouse_Zone3/SecretPassage_DFH_Zone3").GetComponent<SlidingDoor>();
            slidingDoor.SetOpenImmediate(true); // open the door to the dream campfire

            // move lanterns away from door so it looks like you opened the door, not just teleported there
            Transform lanterns = SearchUtilities.Find("RingWorld_Body/Sector_RingInterior/Sector_Zone3/Sector_HiddenGorge/Sector_DreamFireHouse_Zone3/Interactables_DreamFireHouse_Zone3/Lanterns_DFH_Zone3").transform;
            SimpleLanternItem lantern5 = SearchUtilities.Find("RingWorld_Body/Sector_RingInterior/Sector_Zone3/Sector_HiddenGorge/Sector_DreamFireHouse_Zone3/Interactables_DreamFireHouse_Zone3/Lanterns_DFH_Zone3/Prefab_IP_SimpleLanternItem_Zone3DFH_5").GetComponent<SimpleLanternItem>();
            lantern5.transform.SetParent(lanterns, false);
            lantern5.transform.localPosition = new Vector3(-1.66f, 0, 4.1f);
            lantern5.transform.localEulerAngles = Vector3.zero;
            SimpleLanternItem lantern6 = SearchUtilities.Find("RingWorld_Body/Sector_RingInterior/Sector_Zone3/Sector_HiddenGorge/Sector_DreamFireHouse_Zone3/Interactables_DreamFireHouse_Zone3/Lanterns_DFH_Zone3/Prefab_IP_SimpleLanternItem_Zone3DFH_6").GetComponent<SimpleLanternItem>();
            lantern6.transform.SetParent(lanterns, false);
            lantern6.transform.localPosition = new Vector3(1.55f, 0, 3.952f);
            lantern6.transform.localEulerAngles = Vector3.zero;

            yield return new WaitForSeconds(animTime);  // waits until animation stops to proceed to next line
            _spawner.DebugWarp(dreamSpawn); // warps you again because dark bramble is weird with spawnpoints
            yield return new WaitForSeconds(3);  // waits until animation stops to proceed to next line

            // open eyes
            cameraEffectController.OpenEyes(animTime, false); // open eyes
            yield return new WaitForSeconds(animTime); //  waits until animation stops to proceed to next line
        }

        private bool Check()
        {
            return PlayerData.GetPersistentCondition("CYPRESS_BOARDVESSEL");
        }

        private bool ReadyToWarpToSim()
        {
            return DialogueConditionManager.SharedInstance.GetConditionState("CYPRESS_TOSIM");
        }
    }
}