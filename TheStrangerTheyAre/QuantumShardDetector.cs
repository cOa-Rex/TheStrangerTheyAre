using UnityEngine;
using OWML.Common;
using NewHorizons.Utility;
using NewHorizons.Utility.Files;

namespace TheStrangerTheyAre
{
    public class QuantumShardDetector : MonoBehaviour
    {
        private GameObject _ice; // creates variable to store the enigma state ice
        private GameObject _iceBroken; // creates variable to store the enigma state 3 broken ice
        private GameObject _shard; // creates variable to store the weak shard
        private GameObject _activateSocket; // creates variable to store the weak shard

        private GameObject _planetState; // creates variable to store quantum planet
        private GameObject _planetState3; // creates variable to store quantum planet
        private GameObject _barkShip; // creates vairable to store ship

        private OWAudioSource _audio;
        private bool _hasBroken; // creates boolean to check if the ice broke
        private bool _stateActiveOnce; // creates boolean to check if the player saw state 3 or not.

        private Vector3 _shardPos = new Vector3(-179.4065f, 60.6461f, -57.375f); // creates variable to store shard position
        private Quaternion _shardRot = new Quaternion(11.4458f, 345.3546f, 74.4897f, -0.0001f); // creates variable to store shard rotation
        private Vector3 _moveShipPos = new Vector3(-126.8444f, 51.2016f, -48.3741f); // creates variable to store coordinates to move the ship

        private VisibilityObject _shardVisibilityObject;
        private OWCamera _probeCam; // creates variable to store probe camera

        public void Start()
        {
            _hasBroken = false; // sets activation checking ice to false every loop
            _stateActiveOnce = false; // sets state 3 activation checking volume to false every loop

            _planetState = TheStrangerTheyAre.NewHorizonsAPI.GetPlanet("Distant Enigma").transform.Find("Sector").gameObject; // gets state 1 of the planet
            _planetState3 = TheStrangerTheyAre.NewHorizonsAPI.GetPlanet("Distant Enigma").transform.Find("Sector-3").gameObject; // gets state 3 of the planet

            _ice = _planetState3.transform.Find("EnigmaThinIce").gameObject; // gets the ice
            _iceBroken = _planetState3.transform.Find("EnigmaThinIceBroken").gameObject; // gets the broken ice
            _shard = _planetState.transform.Find("ENIGMA_SHARD_WEAK").gameObject; // gets the weak shard
            _audio = _shard.GetComponent<OWAudioSource>(); // gets the shard audio
            _activateSocket = _planetState.transform.Find("Quantum Sockets - WeakenedQuantum/Socket 0").gameObject; // gets the weak shard socket
            _probeCam = Locator.GetProbe().GetForwardCamera().GetOWCamera();
            _barkShip = SearchUtilities.Find("Sector-3/State3_Ship");

            _shardVisibilityObject = _shard.GetComponent<VisibilityObject>();

            AudioUtilities.SetAudioClip(_audio, "assets/Audio/enigmabreak.ogg", TheStrangerTheyAre.Instance); // sets audio clip
        }

        public void Update()
        {
            // handle breaking
            if (_planetState.activeSelf && _shard.activeSelf
                && _activateSocket.GetComponent<QuantumSocket>().IsOccupied()
                && _shardVisibilityObject.CheckVisibilityFromProbe(_probeCam))
            {
                if (!_hasBroken) {
                    _audio.Play();
                    _hasBroken = true; // sets break-checking boolean to true when weakened shard is active, player is in state 1, and in the right spot.
                }
            }
            if (_planetState3.activeSelf)
            {
                // run when broken
                if (_hasBroken)
                {
                    _barkShip.transform.localPosition = _moveShipPos; // moves ship if glass is broken
                    _ice.SetActive(false); //  disables normal ice when broken and player is at state 3
                    _iceBroken.SetActive(true); // enables broken ice when broken and player is at state 3
                }
                else
                {
                    _ice.SetActive(true); // enables normal ice at the start of the loop
                    _iceBroken.SetActive(false); // disables broken ice at the start of the loop
                }
            }
        }
    }
}