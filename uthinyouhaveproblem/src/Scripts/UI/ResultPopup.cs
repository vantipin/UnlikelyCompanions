using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageTextPair{
	public Image _image;
	public Text _text;
	
	public ImageTextPair(Image i , Text t){
		_image = i;
		_text = t;
	}
}


public class ResultPopup : MonoBehaviour {
	
	public ImageTextPair[] array = new ImageTextPair[4];
	
	private Person[] _persons;
	
	private bool _init = false;
	
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4; i++) {
			Image im = transform.Find("Image").transform.Find("Image" + ( i +  1)).GetComponent<Image>();
			Text t = transform.Find("Image").transform.Find("Text" + ( i +  1)).GetComponent<Text>();
			
			ImageTextPair pair = new ImageTextPair(im , t);
			array[i] = pair;
		}
		
		Button b = transform.Find("Button").GetComponent<Button>();
		
		b.onClick.AddListener(() => Destroy(gameObject , 0.1f));
		
	}
	
	
	public void AddPersons(Person[] persons){
		_persons = persons;
	}
	
	// Update is called once per frame
	void Update () {
		if (_persons != null && !_init) {
			for(int i = 0; i < _persons.Length; i++){
				var person = _persons[i];

				array[i]._image.sprite = person.avatar;

				string message = "";
				if (person.effected != person.oldEffected)
				{
					message = person.EffectMessage + "\n";
				}
				if (person.oldHp - person.hp != 0)
				{
					if (person.hp == 1)
						message += "Has been injured";
					else if (person.hp == 0)
						message += "Has been killed";
				}
				person.oldEffected = person.effected;
				person.oldHp = person.hp;
				array[i]._text.text = message;
			}
			_init = true;
		}
	}
}
