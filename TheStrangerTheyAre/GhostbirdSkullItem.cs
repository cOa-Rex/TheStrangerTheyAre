using System;
using UnityEngine;

namespace TheStrangerTheyAre;

public class GhostbirdSkullItem : OWItem
{
    public override void Awake()
    {
        _type = TheStrangerTheyAre.GhostbirdSkullItemType;
        base.Awake();
    }

    public void Start()
    {
        enabled = false;
    }

    public override string GetDisplayName()
    {
        return string.Format(TheStrangerTheyAre.NewHorizonsAPI.GetTranslationForOtherText("SkullDisplayName"), TheStrangerTheyAre.NewHorizonsAPI.GetTranslationForUI("Skull"));
    }

    public override void DropItem(Vector3 position, Vector3 normal, Transform parent, Sector sector, IItemDropTarget customDropTarget)
    {
        base.DropItem(position, normal, parent, sector, customDropTarget);
        Vector3 defaultScale = new Vector3(1, 1, 1);
        transform.localScale = defaultScale;
    }

    public override void PickUpItem(Transform holdTranform)
    {
        base.PickUpItem(holdTranform);
        Vector3 smallerScale = new Vector3(0.3f, 0.3f, 0.3f);
        transform.localScale = smallerScale;
        Locator.GetShipLogManager().RevealFact("QUANTUM_CORPSE_SKULL");
    }
}
