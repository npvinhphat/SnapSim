using UnityEngine;

public static class Container
{
	//public static AtlasManager AtlasManager { private set; get; }
	//public static GameController GameController { private set; get; }
	//public static InventoryManager InventoryManager { private set; get; }
	// insert more stuff here...

    public static FeatureLayout0 FeatureLayout { private set; get; }
	
	public static GameObject HoldAll { private set; get; }
	
	static Container()
	{
		HoldAll = SafeFindWithTag(Tags.holdAll);
	    Object.DontDestroyOnLoad(HoldAll);

	    //AtlasManager = Add(AtlasManager);
	    //InventoryManager = Add(InventoryManager);
	    //GameController = Add(GameController);
	    // when you insert something new, don't forget to Add it
	    FeatureLayout = Add(FeatureLayout);
	}
	
	private static GameObject SafeFindWithTag(string tag)
	{
		var GO = GameObject.FindWithTag(tag);
		if (GO == null)
			ThrowError("[Container]: GameObject of tag `"
			           + tag
			           + "` was not found!");
		return GO;
	}
	
	private static void ThrowError(string msg)
	{
		Debug.LogError(msg);
		Debug.Break();
	}
	
	private static T SafeGetComponent<T>(GameObject from) where T : Component
	{
		T comp = from.GetComponent<T>();
		if (comp == null)
			ThrowError("[Container]: Component `"
			           + typeof(T)
			           + "` was not found in the GameObject `"
			           + from.name);
		return comp;
	}
	
	private static T SafeGetComponent<T>() where T : Component
	{
		return SafeGetComponent<T>(HoldAll);
	}
	
	public static T Add<T>(T member) where T: Component
	{
		Object.DontDestroyOnLoad(member);
		return SafeGetComponent<T>();
	}
	
	public static void Ping(GameObject go)
	{
		Debug.Log("[Container]: Hear you loud and clear, `" + go.name + "`");
	}
	
	public static void Ping()
	{
		Debug.Log("[Container]: Everything's working fine. IT'S ALIVE!");
	}
}