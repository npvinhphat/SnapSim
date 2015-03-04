using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static int NodeCount = 0;
    public List<DeviceNode> DeviceNodes;
    public List<GameObject> Wires; 
    public string NodeName { get; set; }
    public bool IsGround = false;
    public override string ToString()
    {
        return IsGround ? "0" : NodeName;
    }

    private void Awake()
    {
        if (NodeName == null)
        {
            NodeCount++;
            NodeName = NodeCount.ToString();
        }

        //DELETE LATER
        if (DeviceNodes.Count >= 2)
        {
            UpdateNode();
        }
    }

    public void AddDeviceNode(DeviceNode deviceNode)
    {
        if (!(DeviceNodes.Contains(deviceNode)))
        {
            DeviceNodes.Add(deviceNode);
        }
        else
        {
            Debug.LogWarning("Add an existed Device Node -> Device Nodes (List)");
        }
    }

    public void UpdateNode()
    {
        UpdatePosition();
        UpdateLineDraws();
    }

    private void UpdatePosition()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        foreach (DeviceNode deviceNode in DeviceNodes)
        {
            pos += deviceNode.gameObject.transform.position;
        }
        this.gameObject.transform.position = pos/DeviceNodes.Count;
    }

    private void UpdateLineDraws()
    {
        // First we delete all the other line
        while (Wires.Count < DeviceNodes.Count)
        {
            GameObject newWire = (GameObject) Instantiate(Container.CircuitElements.Wire, new Vector3(0, 0, 0), Quaternion.identity);
            Wires.Add(newWire);
        }
        while (Wires.Count > DeviceNodes.Count)
        {
            Wires.RemoveAt(Wires.Count - 1);
        }

        // Redraw all the shit
        for (int i = 0; i < Wires.Count; i++)
        {
            LineRenderer wireLineRenderer = Wires[i].GetComponent<LineRenderer>();
            wireLineRenderer.SetVertexCount(2);
            wireLineRenderer.SetPosition(0, DeviceNodes[i].transform.position);
            wireLineRenderer.SetPosition(1, transform.position);
            //print("Draw from " + DeviceNodes[i].transform.position + " to " + transform.position);
        }
    }
}

