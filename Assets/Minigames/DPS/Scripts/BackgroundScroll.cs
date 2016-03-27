using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour
{
    public float Speed;
    private Renderer rend;
    private Light lightSource;

    void Start()
    {
        rend = GetComponent<Renderer>();
        lightSource = GetComponentInChildren<Light>();
    }

    void Update()
    {
        Vector2 offset = new Vector2(Time.time * Speed, 0);
        rend.material.mainTextureOffset = offset;

        
        // Simulate sunlight coming in through the trees / leaves
        lightSource.intensity = Mathf.Min(2.75f, Mathf.Max(2f, Random.Range(0.97f * lightSource.intensity, 1.03f * lightSource.intensity)));


        //lightSource.intensity = Mathf.InverseLerp(0, 30, 2);


    }
}