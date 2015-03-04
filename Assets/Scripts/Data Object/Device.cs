using System;
using UnityEngine;

public abstract class Device : MonoBehaviour
{
    public string DeviceName { get; set; }
    public abstract TypeEnum DeviceType { get; }
    public static int DeviceCount = 0;

    public enum TypeEnum
    {
        Resistor,
        Capacitor,
        Ground,
        PulseVoltageSource
    }

    public void Awake()
    {
        if (DeviceName == null)
        {
            DeviceCount++;
            DeviceName = DeviceCount.ToString();
        }
    }

    public readonly static string Suffix = "afpnumkKMGT";
    public readonly static double[] Scales = 
    {
        1e-18,
        1e-15,
        1e-12,
        1e-9,
        1e-6,
        1e-3,
        1e3,
        1e3,
        1e6,
        1e9,
        1e12
    };

    public static string ConvertValueToString(double value)
    {
        return value.ToString();
    }

    public static double ConvertStringToValue(string str)
    {
        var chars = str.ToCharArray();
        var n = chars.Length;
        while (n > 0)
        {
            if (!Char.IsLetter(chars[n - 1])) break;
            n--;
        }
        double value;
        try
        {
            if (n < chars.Length)
            {
                value = Double.Parse(str.Substring(0, n));
                n = Suffix.IndexOf(chars[n]);
                if (n < 0) return value;
                value *= Scales[n];
            }
            else value = Double.Parse(str);
        }
        catch (Exception e)
        {
            print(e);
            value = 0.0;
        }
        return value;
    }

    public static Device[] GetDevices()
    {
        var gameObjects = GameObject.FindGameObjectsWithTag(Tags.Device);
        var devices = new Device[gameObjects.Length];
        for (var i = 0; i < gameObjects.Length; i++)
        {
            devices[i] = gameObjects[i].GetComponent<Device>();
        }
        return devices;
    }

    /** Return t
     * 
     */
    public static void ConvertDevicesToTextFile(Device[] devices)
    {
        string str = "";
        foreach (Device device in devices)
        {
            if (device.DeviceType != Device.TypeEnum.Ground)
            {
                str = str + device;
            }
        }
        /* File.WriteAllText(Application.dataPath + "/test.text", str));
        AssetDatabase.Refresh();*/
        print(str);
    }
}
