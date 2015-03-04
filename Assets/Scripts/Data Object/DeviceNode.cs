using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceNode : MonoBehaviour
{
    public Node ConnectedNode = null;

    public override string ToString()
    {
        return ConnectedNode.ToString();
    }

}

