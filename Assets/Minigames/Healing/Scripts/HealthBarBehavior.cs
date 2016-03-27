using UnityEngine;
using System.Collections;

public class HealthBarBehavior : MonoBehaviour
{

    public float maxValue;
    public float CurrentValue
    {
        get;
        set;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(CurrentValue / maxValue, 1f);
	}
}
