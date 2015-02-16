using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static readonly object _lock = new object();
	private static T _instance;
	public static T Instance
	{
		get
		{
			lock (_lock) {
			    if (_instance != null) return _instance;
			    // try to find it
			    var instances = FindObjectsOfType(typeof(T)) as T[];
					
			    // couldn't find any object
			    if (instances == null || instances.Length == 0) {
			        var instanceObj = new GameObject("(Singleton) " + typeof(T));
			        _instance = instanceObj.AddComponent<T>();
			        Debug.Log("[Singleton]: An instance of `" + typeof(T) + "` is needed." +
			                  " So gameObject `" + instanceObj.name + "` was created" +
			                  " with `" + typeof(T) + "` component attached to it");
			    }
			    else {
			        // see if there's more than one, if so, do something about it
			        if (instances.Length > 1) {
			            Debug.LogWarning("[Singleton]: There is more than one instance of `" +
			                             typeof(T) +
			                             "` in your scene. Destroying all, keeping only one...");
							
			            for (int i = 1, len = instances.Length; i < len; i++) {
			                Destroy(instances[i]);
			            }
			        }
			        else if (instances.Length == 1) {
			            Debug.Log("[Singleton]: Found only one instance of `" +
			                      typeof(T) +
			                      "` in `" + instances[0].gameObject.name +
			                      "` So singlation successful! :)");
			        }
			        _instance = instances[0];
			    }
			    DontDestroyOnLoad(_instance); // for preservation of this object through scenes
			    return _instance;
			}
		}
	}
	
	public void Ping()
	{
		Debug.Log("[Singleton]: `" + this + "` is alive!");
	}
	
	protected virtual void Awake()
	{
		Ping(); // this is just so that DontDestroyOnLoad gets called on the gameObject that's gonna this script upon waking up - Calling Ping means accessing the singleton instance, doing so calls DontDestroyOnLoad at the end.
	}
}

