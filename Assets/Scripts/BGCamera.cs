using JetBrains.Annotations;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BGCamera : MonoBehaviour {

	public string DeviceName;
	private WebCamTexture _webCamTexture;
	private GameObject _webCam;

	// Use this for initialization
    [UsedImplicitly]
	void Start () {
		WebCamDevice[] devices = WebCamTexture.devices;
		DeviceName = devices[devices.Length - 1].name;
		Debug.Log ("Number of devices" + devices.Length);
		_webCamTexture = new WebCamTexture (DeviceName, Screen.width, Screen.height, 60);
		_webCam = GameObject.FindGameObjectWithTag ("WebCam");
		_webCam.transform.localScale = new Vector3 (2f, 1f, 1f);
		_webCam.GetComponent<Renderer>().material.mainTexture = _webCamTexture;

		_webCamTexture.Play ();
	}
	
	// Update is called once per frame
    [UsedImplicitly]
	void Update () {

	}

    public void SaveImage(string screenShotName)
    {
        Application.CaptureScreenshot(screenShotName);
        Texture2D snap = new Texture2D(_webCamTexture.width, _webCamTexture.height);
        snap.SetPixels(_webCamTexture.GetPixels());
        snap.Apply();
        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/" + screenShotName, snap.EncodeToPNG());
    }

    public void LoadImage(GameObject captureImageGameObject, string screenShotName)
    {
        StartCoroutine(LoadImageEnumerator(captureImageGameObject, screenShotName));
    }

    IEnumerator LoadImageEnumerator(GameObject captureImageGameObject, string screenShotName)
    {
        string path;
        #if UNITY_EDITOR
            path = "file:" + Application.persistentDataPath;
        #elif UNITY_ANDROID
            path = "file:///"+ Application.persistentDataPath;
        #elif UNITY_IOS
            path = "file:" + Application.persistentDataPath;
        #else
            //Desktop (Mac OS or Windows)
            path = "file:"+ Application.persistentDataPath;
        #endif

        //WWW www = new WWW("http://images.earthcam.com/ec_metros/ourcams/fridays.jpg");
        WWW www = new WWW(path + "/" + screenShotName);
        print("nana " + Application.persistentDataPath + "/" + screenShotName);
        print(www.url);
        yield return www;
        captureImageGameObject.GetComponentInChildren<Image>().sprite =
            Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
    }
}
