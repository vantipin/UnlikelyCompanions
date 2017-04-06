using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CanvasInit : MonoBehaviour {
	
	private Main _main;
	private Person[] _drawablePersons;
	
	private Canvas _canvas;
	private Holder[] _containers;
	
	void Start () {
		
		_canvas = GetComponent<Canvas>();
		_containers = transform.GetComponentsInChildren<Holder>();
		
		Debug.Log(_containers.Length);
		
	}
	
	public void Init(Person[] persons){
		for (int i = 0; i < persons.Length; i++)
		{
			_containers[i].Init(persons[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
