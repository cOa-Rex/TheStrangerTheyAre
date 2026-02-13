using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre;
public class AtmosphereBurnoutEffect : MonoBehaviour
{
    // custom variables
    [SerializeField]
    public GameObject flames;
    [SerializeField]
    public Animator flamesAnim;

    // get nh stuff
    private GameObject _revealVol;
    private GameObject _atmosphere;
    private GameObject _clouds;
    private GameObject _hazard;
    private GameObject _ashes;
    private GameObject[] _travelerAssets = new GameObject[5];

    private bool _runOnce;

    public void Start()
    {
        _runOnce = false; // something for the time loop checking if statement to make sure it doesn't run again

        // gets nh generated objects
        var desert = TheStrangerTheyAre.NewHorizonsAPI.GetPlanet("Sizzling Sands"); // gets the desert planet with nh
        _atmosphere = desert.transform.Find("Sector/Atmosphere").gameObject;
        _clouds = desert.transform.Find("Sector/Clouds").gameObject;
        _hazard = desert.transform.Find("Sector/HazardVolume").gameObject;
        _revealVol = desert.transform.Find("Sector/DesertSolution").gameObject;

        // gets arcadia
        _travelerAssets[0] = SearchUtilities.Find("SizzlingSands_Body/Sector/Prefab_IP_GhostBird_Desert/Ghostbird_IP_ANIM");
        _travelerAssets[1] = SearchUtilities.Find("SizzlingSands_Body/Sector/Prefab_IP_GhostBird_Desert/Prisoner_FocalPoint");
        _travelerAssets[2] = SearchUtilities.Find("SizzlingSands_Body/Sector/Prefab_IP_GhostBird_Desert/Collider (2)");
        _travelerAssets[3] = SearchUtilities.Find("SizzlingSands_Body/Sector/Prefab_IP_GhostBird_Desert/ConversationZone");
        _travelerAssets[4] = SearchUtilities.Find("SizzlingSands_Body/Sector/Prefab_IP_GhostBird_Desert/AudioSource");

        // handle arcadia's ashes
        _ashes = SearchUtilities.Find("SizzlingSands_Body/Sector/Props_IP_Ash");
        _ashes.SetActive(false);
        _revealVol.SetActive(false);
    }

    public void Update()
    {
        
        var burnDuration = TimeLoop.GetSecondsElapsed() > 1320;
        var atmosphereBurnt = TimeLoop.GetSecondsElapsed() > 1330;
        var burnEnd = TimeLoop.GetSecondsElapsed() > 1334;


        if (burnDuration && !_runOnce)
        {
            flames.SetActive(true);
            flamesAnim.Play("AtmosphereBurn", 0);
            _runOnce = true;
        }

        if (atmosphereBurnt)
        {
            foreach (var asset in _travelerAssets)
            {
                Destroy(asset);
            }
            _ashes.SetActive(true);
            _atmosphere.SetActive(false);
            _clouds.SetActive(false);
            _hazard.SetActive(false);
            _revealVol.SetActive(true);
        }

        if (burnEnd)
        {
            //Locator.GetShipLogManager().RevealFact("DESERT_MAIN_ATMO");
            //Locator.GetShipLogManager().RevealFact("DESERT_LIGHT_ATMO");
            flames.SetActive(false);
        }
    }
}
