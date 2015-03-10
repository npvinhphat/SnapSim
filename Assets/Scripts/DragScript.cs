using UnityEngine;
using System.Collections;

public class DragScript : MonoBehaviour
{
    [SerializeField]
    private bool _isDrag = false;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            //print(touchPosition);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(new Vector2(touchPosition.x, touchPosition.y)))
                    {
                        _isDrag = true;
                    }
                    break;

                case TouchPhase.Moved:
                    if (_isDrag)
                    {
                        DragObject(touchPosition);
                    }
                    break;

                case TouchPhase.Ended:
                    _isDrag = false;
                    break;
            }
        }




    }

    void DragObject(Vector3 touchPosition)
    {
        this.transform.position = new Vector3(Mathf.Round(touchPosition.x), Mathf.Round(touchPosition.y), transform.position.z);
    }
}