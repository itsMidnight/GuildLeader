using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelController : MonoBehaviour 
{
	public GameObject Card;
	public float xPadding = 2f;
	private int num_cards = 1;
	private GameObject[] Cards;
	Sprite [] sprites;
	string[] names1 = {"Dark", "Haunted", "Eerie", "Blasted", "Forgotten", "Dragon's", "Mage's"};
	string[] names2 = {"Forest", "Keep", "Castle", "Mountain", "Desert", "Tower", "Swamp"};
	public RectTransform containerRectTransform;
	// Use this for initialization

	void Start () 
	{
		Debug.Log ("start");


		sprites = Resources.LoadAll<Sprite>("Sprites/RaidIcons");


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
		string name = names1[(int)Random.Range(0f,(float)names1.Length)]+" " + names2[(int)Random.Range(0f,(float)names2.Length)];
		newCard.name = name + " Card";
		GameObject IconObj = newCard.transform.FindChild("Icon").gameObject;
		GameObject TitleObj = newCard.transform.FindChild("Title").gameObject;
		Image iconImg = IconObj.GetComponent<Image> ();
		Text titleText = TitleObj.GetComponent<Text> ();

		titleText.text = name;
		iconImg.sprite = sprites[(int)Random.Range(0f,(float)sprites.Length)];

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
