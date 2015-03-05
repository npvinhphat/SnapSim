using UnityEngine;
using System.Collections;

public class TestSimulation : MonoBehaviour
{
    public Resistor TestResistor;
    public Capacitor TestCapacitor;
    public Device TestDevice;
    public Node TestNode;

	// Use this for initialization
    void Start()
    {
        Container.Simulator.StartSimulation();
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Container.Simulator.CurrentStep++;
            print(TestCapacitor.GetCurrent() + "A");
        }
	}
}

