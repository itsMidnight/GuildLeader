using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour {
    protected Image image;
    public float amountFilled;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        image.fillAmount = Mathf.MoveTowards(image.fillAmount, amountFilled, Time.deltaTime * .5f);
    }
}
