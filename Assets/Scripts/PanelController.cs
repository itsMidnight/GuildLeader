using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelController : MonoBehaviour 
{
	public GameObject Card;
	public float xPadding = 2f;
	private int num_cards = 1;

	private RectTransform containerRectTransform;
	// Use this for initialization

	void Start () 
	{
		Debug.Log ("start");
		containerRectTransform = gameObject.GetComponent<RectTransform>();
		for (int i = 0; i < 5; i++) {
			AddCard ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddCard()
	{
		RectTransform rectTrans;

		GameObject newCard = Instantiate (Card);
		rectTrans = newCard.GetComponent<RectTransform> ();
		rectTrans.SetParent(this.gameObject.transform);

		float width = containerRectTransform.rect.width;
		float ratio = width / rectTrans.rect.width;
		float height = rectTrans.rect.height * ratio;

		//Populate Raid Info Here
		newCard.name = "Temp Raid Name";
		GameObject IconObj = newCard.transform.FindChild("Icon").gameObject;
		Image iconImg = IconObj.GetComponent<Image> ();
		iconImg.sprite = Resources.Load<Sprite>("Raid_Icons_1");


		float x = 0;
		float y = 0 - height * num_cards;

		rectTrans.offsetMin = new Vector2(x, y);

		x = rectTrans.offsetMin.x + width;
		y = rectTrans.offsetMin.y + height;
		rectTrans.offsetMax = new Vector2(x, y);
		rectTrans.localScale = new Vector3(1, 1, 1);

		num_cards++;
	}
}
