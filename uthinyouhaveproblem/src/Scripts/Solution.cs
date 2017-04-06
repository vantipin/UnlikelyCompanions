using UnityEngine;
using System.Linq;
using System.Collections;
using UnityEngine.UI;

public class Solution : MonoBehaviour {

    public Person[] Persons;
	public bool clicked = false;

	public GameObject solutionPopup;
	private GameObject _currentPopup;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.RotateAround (transform.position, Vector3.up, 1f); 
		float position = transform.position.y;
		float ampl = Mathf.Sin (Time.time*4)*0.5f;
		transform.position = new Vector3 (transform.position.x, position + ampl, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Person applySolution() {
        Person[] persons = Persons.Alive();	

        
        if (persons.Length == 1)
        {
            int rand = Random.Range(0, 2);
            bool injure = System.Convert.ToBoolean(rand);
            if(!injure) {
                return null;
            }
        }

        Person injuredPerson = persons [Random.Range(0, persons.Length)];

        return injuredPerson;
    }

	void OnMouseEnter()
	{
		foreach(Person p in Persons)
		{
			p.highlighted = true;
		}
		if (_currentPopup == null) {
			Vector3 newPos = transform.position + new Vector3(0 , 0 , 4f);
			_currentPopup = (GameObject)GameObject.Instantiate (solutionPopup, transform.position, GameObject.FindGameObjectWithTag ("MainCamera").transform.rotation);
			PopulatePopup(_currentPopup);
		}
	}

	void PopulatePopup(GameObject gObject){
		if (Persons.Length > 0)
		{
			gObject.transform.Find("pane").transform.Find("portrait1").GetComponent<Image>().sprite = Persons[0].avatar;
			if (Persons.Length > 1)
			{
				gObject.transform.Find("pane").transform.Find("portrait2").GetComponent<Image>().sprite = Persons[1].avatar;
			}
		}
		//gObject.
	}

	void OnMouseExit()
	{
		foreach(Person p in Persons)
		{
			p.highlighted = false;
		}
		if (_currentPopup != null) {
			Destroy(_currentPopup , 0.3f);
		}
	}

    void OnMouseDown()
	{  
		foreach(Person p in Persons)
		{
			p.highlighted = false;
		}
		Destroy(_currentPopup , 0.3f);
        clicked = true;
    }
}
