using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceNode : MonoBehaviour
{
    private bool _onClicked = false;

    public Node ConnectedNode = null;

    public override string ToString()
    {
        return ConnectedNode.ToString();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(new Vector2(touchPosition.x, touchPosition.y)))
                    {
                        _onClicked = true;
                    }
                    break;

                case TouchPhase.Moved:
                    _onClicked = false;
                    //Container.FeatureLayout.
                    break;

                case TouchPhase.Ended:
                    if (_onClicked)
                    {
                        _onClicked = false;
                    }
                    break;
            }
        }
    }

    public static void ConnectDeviceNodes()
    {

    }
}

