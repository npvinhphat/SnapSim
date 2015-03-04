using UnityEngine;
using System.Collections;

public class FeatureLayout0 : FeatureLayout
{
    public static LineRenderer WireLineRenderer;

    public override void TestFunction()
    {
        base.TestFunction();
        print("Feature Layout 0");
    }

    public override void MakeObjectsFromText(TextAsset textAsset)
    {
    }
}
