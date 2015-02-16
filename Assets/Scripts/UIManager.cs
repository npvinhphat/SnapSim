using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    public GameObject CaptureImageGameObject;
    public static string ScreenShotName = "foo.png";
    public BGCamera BGCamera;

	[UsedImplicitly]
	void Start ()
	{
	}
	
	[UsedImplicitly]
	void Update () {
	
	}

    public void CaptureButtonClicked()
    {
        BGCamera.SaveImage(ScreenShotName);
        BGCamera.LoadImage(CaptureImageGameObject, ScreenShotName);
    }
}

