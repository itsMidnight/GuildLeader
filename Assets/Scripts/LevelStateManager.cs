using UnityEngine;
using System.Collections;



// Jess or Turbo fill in here??
public class RaidData
{
    public bool isWin;
}

public class LevelStateManager : MonoBehaviour {

    public RaidData result; 
    private static LevelStateManager instance = null;
    public static LevelStateManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else {
            instance = this;
        }
        //DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartLevel()
    {
        result = new RaidData();
    }

    public void EndLevel(RaidData ret)
    {
        result = ret;
    }

    public RaidData GetResults()
    {
        return result; 
    }

    public void ClearResult()
    {
        result = null;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
