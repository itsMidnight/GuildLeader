using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System;

public enum RaidReq
{
    Star,
    Square,
    Skull
}

[Serializable]
public class GuildMember
{
    public string Name { get; set; }

    [XmlElement("FlavorText")]
    public string Flavor { get; set; }

    public string Description { get; set; }

    [XmlElement("Ability")]
    public RaidReq PreReq { get; set; }

    public GuildMember()
    {
        this.Name = "MrPoopyButthole";
        this.Flavor = "Spicy";
        this.Description = "No sad memories";
    }
}

[Serializable]
[XmlRoot("AvailableGuildies")]
public class AvailableGuildies
{
    [XmlArray("GuildMemberList"), XmlArrayItem(typeof(GuildMember), ElementName = "GuildMembers")]
    public List<GuildMember> allBuddies { get; set; }

    public AvailableGuildies()
    {
        this.allBuddies = new List<GuildMember>();
        this.allBuddies.Add(new GuildMember());
        this.allBuddies.Add(new GuildMember());
    }
}

public class MemberManager
{
    List<GuildMember> currentMembers;
    GuildMember[] raidTeam;
    AvailableGuildies all;

    public static readonly int MaxRaidSize = 5;
    static readonly string GuildieList = "GuildMemberList.xml"; 
    public MemberManager()
    {
        this.currentMembers = new List<GuildMember>();
        this.raidTeam = new GuildMember[MaxRaidSize];
        this.all = new AvailableGuildies();
    }

    public GuildMember[] GetRaidTeam()
    {
        return null;
    }

    public bool AddGuildMember()
    {
        return true;
    }

    public bool LoadAvailable()
    {
        AvailableGuildies myObject = new AvailableGuildies();
        XmlSerializer mySerializer = new XmlSerializer(typeof(AvailableGuildies));
        StreamWriter myWriter = new StreamWriter(GuildieList);
        mySerializer.Serialize(myWriter, myObject);
        myWriter.Close();
        return true;
    }

    public bool SaveList()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AvailableGuildies));
        FileStream fs = new FileStream(GuildieList, FileMode.OpenOrCreate);
        AvailableGuildies po;
        po = (AvailableGuildies)serializer.Deserialize(fs);
        return true;
    }
}

public class CharacterMenu : MonoBehaviour {
    public GameObject b;
    public Canvas canvas;

    void Start ()
    {
        CharacterCardScript prefab = Resources.Load("Prefabs/YourPrefab") as CharacterCardScript;
        if (prefab)
        {
            //get down tonight 
        }

        FullRaidList l = new FullRaidList();
        l.Save();
        l.Load();

        var panel = GameObject.Find("CharacterCard0");
        if (panel != null)  // make sure you actually found it!
        {
            GameObject a = (GameObject)Instantiate(b);
            var test = a.GetComponent<RectTransform>();
            a.transform.SetParent(panel.transform, false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        int i = 0; 
        if(i<1)
        {

        }
    }
}
