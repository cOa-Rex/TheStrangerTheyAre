using NewHorizons.Components.Props;

namespace TheStrangerTheyAre;

public class CloakMineralItemSocket : NHItemSocket
{
   public override void Awake()
    {
        base.Awake();
        _acceptableType = TheStrangerTheyAre.CloakMineralItemType;
    }
}
