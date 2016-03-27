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
    }
}

public class MemberManager
{
    List<GuildMember> currentMembers;
    List<GuildMember> raidTeam;
    AvailableGuildies all;

    public static readonly int MaxRaidSize = 5;
    static readonly string GuildieList = "GuildMemberList.xml"; 

    public MemberManager()
    {
        this.currentMembers = new List<GuildMember>();
        this.raidTeam = new List<GuildMember>(MaxRaidSize);
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
        if(this.raidTeam.Count > MaxRaidSize)
        {
            return;
        }
        this.raidTeam.Add(temp);
    }

    public void RemoveFromRaid(GuildMember temp)
    {
        for (int i = 0; i < raidTeam.Count; i++)
        {
            if(temp.Name == raidTeam[i].Name)
            {
                raidTeam.RemoveAt(i);
            }
        }
    }

    /// <summary> Doesnt check prereqs, just body count </summary>
    /// <returns>True if the users has enough raiders. </returns>
    public bool IsRaidReady()
    {
        return this.raidTeam.Count == MaxRaidSize;
    }

    /// <summary> 
    /// Check if raid meets prereqs. Static for now since I think it will end up in turbos control? 
    /// If not just remove the members input but still need raid 
    /// </summary>
    /// <param name="members">Raiders heading to the raid</param>
    /// <param name="raid">Raid requirements</param>
    /// <returns>True if all requirements are met</returns>
    public static bool IsRaidValid(List<GuildMember> members, RaidInstance raid)
    {
        RaidReq[] conditions = raid.PreReqs;
        List<GuildMember> applicants = members;
        if(applicants.Count <=0 || conditions.Length > applicants.Count)
        {
            return false;
        }

        foreach (RaidReq item in conditions)
        {
            for (int i = 0; i < applicants.Count; i++) //Yuck
            {
                if(applicants[i].PreReq == item)
                {
                    applicants.RemoveAt(i);
                    break;
                }
            }
        }
        return applicants.Count==0;
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
