using UnityEngine;
using System.Collections;

public enum MiniGameType
{
    DPS, 
    Tank, 
    Heals
}


public class RaidData
{
    public bool isWin;
    public MiniGameType gameType;
}

public class LevelStateManager : MonoBehaviour {
    public static string MiniGameHostScene = "MiniGameView";
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
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary> Saves inputs to the minigame </summary>
    /// <param name="inputs"> passing in values </param>
    public void StartLevel(RaidData inputs)
    {
        result = inputs;
    }


    /// <summary> Saves outputs from the minigame </summary>
    /// <param name="inputs"> result values </param>
    public void EndLevel(RaidData returnData)
    {
        result = returnData;
    }

    /// <summary> Gets saved results from minigames. Call clear after </summary>
    /// <returns>Datastructure with game results </returns>
    public RaidData GetResults()
    {
        return result; 
    }

    /// <summary> Resets game results </summary>
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
