using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour
{
    public float Speed;
    private Renderer rend;
    private Light lightSource;
    private float x;
    private float y;
    private float z;


    void Start()
    {
        rend = GetComponent<Renderer>();
        lightSource = GetComponentInChildren<Light>();
        x = lightSource.transform.rotation.x;
        y = lightSource.transform.rotation.y;
        z = lightSource.transform.rotation.z;
    }

    void Update()
    {
        Vector2 offset = new Vector2(Time.time * Speed, 0);
        rend.material.mainTextureOffset = offset;

        // Simulate sunlight coming in through the trees / leaves
        lightSource.intensity = Mathf.Min(2.75f, Mathf.Max(2f, Random.Range(0.97f * lightSource.intensity, 1.03f * lightSource.intensity)));

    }
}