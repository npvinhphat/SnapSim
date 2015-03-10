using UnityEngine;
using System.Collections;

public class FeatureLayout : MonoBehaviour
{
    public DeviceNode ClickedDeviceNode = null;

    public virtual void TestFunction()
    {
        const string featureLayoutHaveWorked = "Feature Layout have worked";
        print(featureLayoutHaveWorked);
    }

    public virtual void MakeObjectsFromText(TextAsset textAsset)
    {
    }
}
