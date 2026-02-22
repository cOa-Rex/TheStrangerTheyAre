using UnityEngine;

namespace TheStrangerTheyAre;

public class CloakMineralItem : OWItem
{
    public override void Awake()
    {
        _type = TheStrangerTheyAre.CloakMineralItemType;
        base.Awake();
    }

    public void Start()
    {
        enabled = false;
    }

    public override string GetDisplayName()
    {
        return string.Format(TheStrangerTheyAre.NewHorizonsAPI.GetTranslationForOtherText("MineralDisplayName"), TheStrangerTheyAre.NewHorizonsAPI.GetTranslationForUI("Mineral")); ;
    }

    public override void DropItem(Vector3 position, Vector3 normal, Transform parent, Sector sector, IItemDropTarget customDropTarget)
    {
        base.DropItem(position, normal, parent, sector, customDropTarget);
    }

    public override void PickUpItem(Transform holdTranform)
    {
        Locator.GetShipLogManager().RevealFact("ANGLERS_EYE_MINE_MINERAL");
        base.PickUpItem(holdTranform);
    }
}
