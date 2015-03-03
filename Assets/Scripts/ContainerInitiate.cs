using UnityEngine;
using System.Collections;

public class ContainerInitiate : MonoBehaviourSingleton<ContainerInitiate>
{
	// Use this for initialization
	void Awake ()
	{
	    Container.Ping();
        if (Application.loadedLevelName == "PreScene")
	    {
	        Application.LoadLevel(1);
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
