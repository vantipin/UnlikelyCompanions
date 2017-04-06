using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Holder : MonoBehaviour {
	
	public static float MORALE_BAR_MAXWIDTH = 120f;
	public static float MORALE_BAR_MAXWIDTH_K = MORALE_BAR_MAXWIDTH / 100f;
	
	public Sprite woundedOverlay;
	public Sprite deadOverlay;
	
	private Person _avatar;
	
	private Image _image;
	private Image _overlay;
	private Image _moraleBar;
	private Text _text;
	
	// Use this for initialization
	void Start () {
		_image = transform.Find("image").GetComponent<Image>();
		_text = GetComponentInChildren<Text>();
		_overlay = _image.transform.Find("overlay").GetComponent<Image>();
		_moraleBar = _image.transform.Find ("moraleBar").GetComponent<Image> ();
	}
	
	
	public void Init(Person p){
		_avatar = p;
		_text.text = _avatar.name;
		_image.sprite = p.avatar;
	}
	
	// Update is called once per frame
	void Update () {
		if (_avatar != null)
		{
			if ( (_avatar.hp == 1) && !(_avatar.hp <= 0))
			{
				_overlay.sprite = woundedOverlay;
				_overlay.enabled = true;
			} else if (_avatar.hp <= 0)
			{
				_overlay.sprite = deadOverlay;
				_overlay.enabled = true;
			} else
			{
				if (_overlay.enabled)
				{
					_overlay.enabled = false;
				}
			}
			if(_avatar.highlighted){
				float period = Mathf.Sin(Time.time * 5) * 0.2f;
				_image.transform.localScale = new Vector3(1f + period,1f + period , 1f +period);
			}
			else
			{
				_image.transform.localScale = Vector3.one;
			}
			
			_moraleBar.rectTransform.sizeDelta = new Vector2(_avatar.morale * MORALE_BAR_MAXWIDTH_K , 10);
			_moraleBar.color = Color.Lerp(Color.black , new Color( 80f / 255f , 108f / 255f , 245f / 255f , 1f) , _avatar.morale / 100f);
			
			
		}
	}
}