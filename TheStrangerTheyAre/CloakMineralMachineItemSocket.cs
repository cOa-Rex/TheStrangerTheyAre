using UnityEngine;
using NewHorizons.Utility.Files;

namespace TheStrangerTheyAre;

public class CloakMineralMachineItemSocket : CloakMineralItemSocket
{
    [SerializeField]
    public OWAudioSource audio;
    [SerializeField]
    public GameObject effects;
    [SerializeField]
    public ElectricityEffect electricEffect;

    private GameObject _activeShard;
    private GameObject _inactiveShard;
    
   public override void Awake()
    {
        base.Awake();
        _activeShard = GameObject.Find("ENIGMA_SHARD_WEAK"); // finds the shard that should be active upon inserting the mineral
        _inactiveShard = GameObject.Find("ENIGMA_SHARD"); // finds the shard that should be active upon removing the mineral
        _activeShard.SetActive(false);
        _inactiveShard.SetActive(true);
        effects.SetActive(false);
    }

    public override bool PlaceIntoSocket(OWItem item)
    {
        if (base.PlaceIntoSocket(item))
        {
            AudioUtilities.SetAudioClip(audio, "assets/Audio/activatemachine.ogg", TheStrangerTheyAre.Instance);
            effects.SetActive(true);
            electricEffect.Play();
            audio.Play();
            _activeShard.SetActive(true);
            _inactiveShard.SetActive(false);
            return true;
        }
        return false;
    }

    public override OWItem RemoveFromSocket()
    {
        effects.SetActive(false);
        if (audio != null)
        {
            AudioUtilities.SetAudioClip(audio, "assets/Audio/deactivatemachine.ogg", TheStrangerTheyAre.Instance);
            audio.Play();
        }
        OWItem oWItem = base.RemoveFromSocket();
        _activeShard.SetActive(false);
        _inactiveShard.SetActive(true);
        return oWItem;
    }
}
