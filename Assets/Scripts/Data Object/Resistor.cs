public class Resistor: Device
{
    public override TypeEnum DeviceType
    {
        get { return TypeEnum.Resistor; }
    }

    public DeviceNode PositiveNode;
    public DeviceNode NegativeNode;
    public double Resistance = 100;

    public override string ToString()
    {
        return "R" + DeviceName + " " + PositiveNode + " " + NegativeNode + " " + Device.ConvertValueToString(Resistance) + "\n";
    }

    public void SetPositiveNode(Node positiveNode)
    {
        PositiveNode.ConnectedNode = positiveNode;
        positiveNode.AddDeviceNode(PositiveNode);
    }

    public void SetNegativeNode(Node negativeNode)
    {
        NegativeNode.ConnectedNode = negativeNode;
        negativeNode.AddDeviceNode(NegativeNode);
    }

    public double GetVoltageDrop()
    {
        return (PositiveNode.ConnectedNode.GetVoltage() - NegativeNode.ConnectedNode.GetVoltage());
    }

    public double GetCurrent()
    {
        return GetVoltageDrop()/Resistance;
    }

}

