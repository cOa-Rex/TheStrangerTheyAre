using UnityEngine;

namespace TheStrangerTheyAre;

public class GhostbirdSkullItem : OWItem
{
    protected bool isRotating;

    public override void Awake()
    {
        _type = TheStrangerTheyAre.GhostbirdSkullItemType;
        isRotating = true;
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
        isRotating = true;
        base.DropItem(position, normal, parent, sector, customDropTarget);
        Vector3 defaultScale = new Vector3(1, 1, 1);
        transform.localScale = defaultScale;
    }

    public override void PickUpItem(Transform holdTranform)
    {
        isRotating = false;
        base.PickUpItem(holdTranform);
        Vector3 smallerScale = new Vector3(0.3f, 0.3f, 0.3f);
        transform.localScale = smallerScale;
        Locator.GetShipLogManager().RevealFact("QUANTUM_CORPSE_SKULL");
    }
}
