using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

	private Button _exit;

	private Button _tryAgain;

	// Use this for initialization
	void Start () {
	
		_tryAgain = transform.Find ("endScreen").transform.Find("Try").GetComponent<Button>();

		_exit = transform.Find ("endScreen").transform.Find("Exit").GetComponent<Button>();

		_tryAgain.onClick.AddListener(() => Application.LoadLevel(Application.loadedLevel));

		_exit.onClick.AddListener(() => Application.Quit());


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
