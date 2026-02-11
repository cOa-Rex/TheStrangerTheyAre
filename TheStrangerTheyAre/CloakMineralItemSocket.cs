namespace TheStrangerTheyAre;

public class CloakMineralItemSocket : OWItemSocket
{
   public override void Awake()
    {
        base.Awake();
        _acceptableType = TheStrangerTheyAre.CloakMineralItemType;
    }
}
