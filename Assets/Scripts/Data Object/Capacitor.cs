public class Capacitor: Device
{
    public override TypeEnum DeviceType
    {
        get { return TypeEnum.Capacitor; }
    }

    public DeviceNode PositiveNode;
    public DeviceNode NegativeNode;
    public double Capacitance = 100;

    public override string ToString()
    { 
        return "XC" + DeviceName + " " + PositiveNode + " " + NegativeNode + " capacitor cval=" + Device.ConvertValueToString(Capacitance) + "\n";
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
        string str = "xc" + DeviceName + ".mid";
        int index = Container.Simulator.NameList.IndexOf(str);
        double midVoltage = Container.Simulator.DataList[index][Container.Simulator.CurrentStep];
        return (PositiveNode.ConnectedNode.GetVoltage() - midVoltage);
    }
}
