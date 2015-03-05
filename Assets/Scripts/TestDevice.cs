using UnityEngine;
using System.Collections;

public class TestDevice : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Device.ConvertDevicesToTextFile(Device.GetDevices());
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
	}
}
