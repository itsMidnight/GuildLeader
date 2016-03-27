using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

public enum RaidType
{
    Tank, 
    DPS, 
    Heals
}

[Serializable]
public class RaidInstance
{
    public string Name { get; set; }

    public string Description { get; set; }

    public int MinFame { get; set; }

    public RaidType RaidClass { get; set; }

    [XmlArray("PreReqList"), XmlArrayItem(typeof(RaidReq), ElementName = "ReqType")]
    public RaidReq[] PreReqs { get; set; }

    public RaidInstance()
    {
        this.Name = "Naxx";
        this.PreReqs = new RaidReq[MemberManager.MaxRaidSize];
        this.MinFame = 10;
    }
}

[Serializable]
[XmlRoot("FullRaidList")]
public class FullRaidList
{
    private static readonly string RaidListFile = "Raids.xml";

    [XmlArray("InstanceList"), XmlArrayItem(typeof(RaidInstance), ElementName = "RaidInstances")]
    public List<RaidInstance> allPredefRaids { get; set; }

    public FullRaidList()
    {
        this.allPredefRaids = new List<RaidInstance>();
    }

    public bool Save()
    {
        XmlSerializer mySerializer = new XmlSerializer(typeof(FullRaidList));
        StreamWriter myWriter = new StreamWriter(RaidListFile);
        mySerializer.Serialize(myWriter, this);
        myWriter.Close();
        return true;
    }

    public FullRaidList Load()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(FullRaidList));
        FullRaidList result; 
        using (FileStream fs = new FileStream(RaidListFile, FileMode.OpenOrCreate))
        {
            result = (FullRaidList)serializer.Deserialize(fs);
        }
        return result;
    }

    public RaidInstance GetRaid(string name)
    {
        foreach(RaidInstance r in this.allPredefRaids)
        {
            if(r.Name == name)
            {
                return r;
            }
        }
        return null;
    }
}

public class RaidManager
{
    FullRaidList allRaids;

    public static readonly int MaxRaidSize = 5;

    public RaidManager()
    {
        this.allRaids = new FullRaidList(); 
    }

    public void Save()
    {
        this.allRaids.Save();
    }

    public void Load()
    {
        this.allRaids = this.allRaids.Load();
    }

    public RaidInstance GetRaidByName(string name)
    {
        if(string.IsNullOrEmpty(name))
        {
            return null;
        }
        return allRaids.GetRaid(name);
    }
}


public class RaidMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
