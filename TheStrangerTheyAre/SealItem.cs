using System;
using UnityEngine;

namespace TheStrangerTheyAre;

public class SealItem : OWItem
{
    [SerializeField]
    public SealID sealID;

    private string _sealName;
    private string _sealFactID;

    public override void Awake()
    {
        base.Awake();
        _type = TheStrangerTheyAre.SealItemType;
        _sealName = GetSealName(sealID);
        _sealFactID = GetSealFactID(sealID);
    }

    public override string GetDisplayName()
    {
        return string.Format(TheStrangerTheyAre.NewHorizonsAPI.GetTranslationForOtherText("SealDisplayName"), TheStrangerTheyAre.NewHorizonsAPI.GetTranslationForUI(_sealName));
    }

    public override void PickUpItem(Transform holdTranform)
    {
        base.PickUpItem(holdTranform);
        Locator.GetShipLogManager().RevealFact(_sealFactID);
    }

    public static string GetSealName(SealID id)
    {
        return id switch
        {
            SealID.Desert => "Sizzling Sands",
            SealID.Ringed => "Ringed Giant",
            SealID.Crimson => "Velvet Vortex",
            SealID.CrimsonMoon => "Burning Bombardier",
            SealID.Enigma => "Distant Enigma",
            _ => throw new ArgumentException("Invalid SealID")
        };
    }

    public static string GetSealFactID(SealID id)
    {
        return id switch
        {
            SealID.Desert => "DESERT_SEAL_E",
            SealID.Ringed => "RING_SEAL_E",
            SealID.Crimson => "CRIMSON_SEAL_E",
            SealID.CrimsonMoon => "VOLCANO_SEAL_E",
            SealID.Enigma => "QUANTUM_SEAL_E",
            _ => throw new ArgumentException("Invalid SealID")
        };
    }
}
