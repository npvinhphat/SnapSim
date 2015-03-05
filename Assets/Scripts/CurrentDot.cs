using UnityEngine;
using System.Collections;

public class CurrentDot : MonoBehaviour
{
    public Vector3 StartPos;
    public Vector3 EndPos;
    public Device AttachedDevice;
    public double CurrentValue = 1; // This value should be between -1 and 1
    public float NormalSpeed = 1f;
    public float ColorFactor = 1.5f;
    public float TimeFactor = 1f;
    public float T = 0;

	// Use this for initialization
	void Start ()
	{
	    transform.position = StartPos;
	    T = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Container.Simulator.IsSimulating)
	    {
	        CurrentValue = GetCurrentValue();
	        T += Time.deltaTime*NormalSpeed*(float) CurrentValue;
	        if (T > 1f)
	        {
	            T = 0f;
	        }
	        if (T < 0f)
	        {
	            T = 1f;
	        }
	        transform.position = Vector3.Lerp(StartPos, EndPos, T);
	        Color newColor = transform.GetComponent<SpriteRenderer>().color;
	        newColor.a = Mathf.Abs((float) CurrentValue * ColorFactor);
            transform.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetComponent<SpriteRenderer>().color, newColor, Time.deltaTime*TimeFactor);
            //print(transform.GetComponent<SpriteRenderer>().color);
	    }
	}

    double GetCurrentValue()
    {
        return AttachedDevice.GetPercentageCurrent();
    }
}
