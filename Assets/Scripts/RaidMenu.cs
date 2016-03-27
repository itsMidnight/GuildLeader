using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class RaidInstance
{
    public string Name { get; set; }

    public string Description { get; set; }

    public int MinFame { get; set; }

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
    public List<RaidInstance> allBuddies { get; set; }

    public FullRaidList()
    {
        this.allBuddies = new List<RaidInstance>();
        this.allBuddies.Add(new RaidInstance());
        this.allBuddies.Add(new RaidInstance());
    }


    public bool Save()
    {
        FullRaidList myObject = new FullRaidList();
        XmlSerializer mySerializer = new XmlSerializer(typeof(FullRaidList));
        StreamWriter myWriter = new StreamWriter(RaidListFile);
        mySerializer.Serialize(myWriter, myObject);
        myWriter.Close();
        return true;
    }

    public bool Load()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(FullRaidList));
        FileStream fs = new FileStream(RaidListFile, FileMode.OpenOrCreate);
        FullRaidList po;
        po = (FullRaidList)serializer.Deserialize(fs);
        return true;
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
