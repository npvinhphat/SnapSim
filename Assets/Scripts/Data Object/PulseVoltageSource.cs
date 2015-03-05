public class PulseVoltageSource: Device
{
    public override TypeEnum DeviceType
    {
        get { return TypeEnum.PulseVoltageSource; }
    }

    public DeviceNode PositiveNode;
    public DeviceNode NegativeNode;
    public double V0 = 0;
    public double Va = 5;
    public double Frequency = 60;
    public double Td = 0;
    public double Damp = 0;

    public override string ToString()
    {
        return "V" + DeviceName + " " + PositiveNode + " " + NegativeNode + " SIN ( " + V0 + "V " + Va + "V " + Frequency + "Hz " + Td + " " + Damp + " )" + "\n";
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
        string str = "i(v" + DeviceName + ")";
        int index = Container.Simulator.NameList.IndexOf(str);
        return Container.Simulator.DataList[index][Container.Simulator.CurrentStep];
    }

    public override double GetPercentageCurrent()
    {
        return GetCurrent()/Container.Simulator.MaximumCurrent;
    }
}


