public class Ground: Device
{
    public override TypeEnum DeviceType
    {
        get { return TypeEnum.Ground; }
    }

    public DeviceNode GroundNode;

    public override string ToString()
    {
        return "";
    }

    public override double GetPercentageCurrent()
    {
        throw new System.NotImplementedException();
    }
}
