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
    Skull,
    Moon
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

    public int GetAvailableCount()
    {
        return this.all.allBuddies.Count;
    }

    public void MovetoGuild(int globalIndex)
    {
        GuildMember temp =  this.all.allBuddies[globalIndex];
        this.all.allBuddies.RemoveAt(globalIndex);
        this.currentMembers.Add(temp);
    }

    public void Gkick(GuildMember temp)
    {
        this.currentMembers.Remove(temp);
    }

    public void AddToRaid(GuildMember temp)
    {
        for (int i = 0; i < raidTeam.Length; i++)
        {
            if (this.raidTeam[i] != null)
            {
                this.raidTeam[i] = temp;
                break;
            }
        }
    }

    public void RemoveFromRaid(GuildMember temp)
    {
        for (int i = 0; i < raidTeam.Length; i++)
        {
            if (this.raidTeam[i].Name == temp.Name)
            {
                break;
            }
        }
    }

    public bool SaveList()
    {
        AvailableGuildies myObject = new AvailableGuildies();
        XmlSerializer mySerializer = new XmlSerializer(typeof(AvailableGuildies));
        StreamWriter myWriter = new StreamWriter(GuildieList);
        mySerializer.Serialize(myWriter, myObject);
        myWriter.Close();
        return true;
    }

    public bool LoadAvailable()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AvailableGuildies));
        FileStream fs = new FileStream(GuildieList, FileMode.OpenOrCreate);
        all = (AvailableGuildies)serializer.Deserialize(fs);
        return (all != null && all.allBuddies.Count > 0);
    }
}

public class CharacterMenu : MonoBehaviour {
    public GameObject b;
    public Canvas canvas;
    MemberManager manager; 

    void Start ()
    {
        this.manager = new MemberManager();
        this.manager.LoadAvailable();
    }
	
	void Update () {
	    
	}

    void OnGUI()
    {
    }

    void Add(int addIndex)
    {
        int count = manager.GetAvailableCount();
        int index = UnityEngine.Random.Range(0,count);
        manager.MovetoGuild(index);
    }
}
