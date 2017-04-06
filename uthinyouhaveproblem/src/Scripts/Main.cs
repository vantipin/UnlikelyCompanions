using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class Pair
{
    public static Pair Create(Person first, Person second)
    {
        return new Pair
        {
            First = first,
            Second = second
        };
    }

    public Person First;
    public Person Second;

}

public enum State {
    GameStart,
    RoundStart, // show problem and solutions
    Moving, //move group to solution
    MovingBack, //move group to start point
    Round, // gameplay
    RoundEnd, // hide problem and solution
    GameEnd
}

public class Main : MonoBehaviour {

	//prefabs
	public GameObject popupPrefab;
	public GameObject endPrefab;


    public static int MoralePerInjury = 25;
	public int ConfigMoralePerInjury = 25;
    public static int MoralePerDeath = 50;
	public int ConfigMoralePerDeath = 50;

    public GameObject[] Problems;
    public GameObject PersonGroup;
    public static Person[] Persons;
	public Person[] ConfigPersons;

	public Text DaysLeft;

    int problemId = 0;
    State state = State.GameStart;

	// Use this for initialization
	void Start () {
		if (ConfigPersons.Length == 0) throw new UnityException("Setup Persons");
		if (ConfigPersons.Any(p => p == null)) throw new UnityException("Setup Persons");
		if (Problems.Length == 0) throw new UnityException("Setup Problems");
		if (Problems.Any(p => p == null)) throw new UnityException("Setup Problems");

		Main.MoralePerInjury = ConfigMoralePerInjury;
		Main.MoralePerDeath = ConfigMoralePerDeath;
		Main.Persons = ConfigPersons;
	}

	// Update is called once per frame
	void Update ()
    {
        if (state == State.GameStart) GameStart();
        if (state == State.RoundStart) RoundStart();
        if (state == State.Moving) Moving();
        if (state == State.MovingBack) MovingBack();
        if (state == State.Round) Round();
        if (state == State.RoundEnd) RoundEnd();
        if (state == State.GameEnd) GameEnd();
    }

    private void GameStart()
    {
		//Init player for UI
		CanvasInit init = GameObject.FindGameObjectWithTag ("Canvas").GetComponent<CanvasInit>();
		init.Init (Persons);

        foreach (var problem in Problems)
        {
            problem.SetActive(false);
        }

        state = State.RoundStart;
    }

    private void RoundStart()
    {
        ActivateProblem();
        state = State.Round;
    }

	private void UpdateDaysLeft()
	{
		DaysLeft.text = (Problems.Length - problemId) + " day" + (Problems.Length > 1 ? "s" : "") + " left";
	}

    private void ActivateProblem()
    {
		UpdateDaysLeft();
        CurrentProblem.SetActive(true);

        var solutions = GetSolutions();
        var pairs = GetPairs().Take(solutions.Length).ToArray();

        for(int i = 0; i < solutions.Length; i++)
        {
            var pair = pairs[i];
			if (pair.First == pair.Second)
			{
				solutions[i].Persons = new[]{ pair.First };
			}
			else
			{
            	solutions[i].Persons = new[]{ pair.First, pair.Second };
			}
        }
    }

    private IEnumerable<Pair> GetPairs()
    {
        int i = Random.Range(0, Persons.Alive().Length);
        while(true)
        {
			Person first = (Persons.Alive()[i % Persons.Alive().Length]);
			Person second = (Persons.Alive()[++i % Persons.Alive().Length]);
            
            yield return Pair.Create(first, second);
        }
    }

    private void Round()
    {
        Solution clickedSolution = GetClickedSolution();
        if (clickedSolution != null)
        {                
            Person injuredPerson = clickedSolution.applySolution();
			if(injuredPerson != null) {
				injuredPerson.InjurePerson(1);
				Person.UpdateMorale();
			}

			GameObject gj = (GameObject)GameObject.Instantiate(popupPrefab);
			ResultPopup rp = gj.GetComponent<ResultPopup>();
			rp.AddPersons(Persons);

            state = State.RoundEnd;
        }

    }

    private void Moving()
    {
        
    }

    private void MovingBack()
    {
        
    }

    private Solution GetClickedSolution()
    {
        Solution clickedSolution = GetSolutions()
            .Where(s => s.clicked)
                .FirstOrDefault();
        return clickedSolution;
    }

    private void RoundEnd()
    {
        CurrentProblem.SetActive(false);
        problemId++;

		if (!Persons.Alive().Any()) 
		{
			state = State.GameEnd;
			return;
		}

        if (problemId < Problems.Length)
        {
            state = State.RoundStart;
        }
        else
        {
            state = State.GameEnd;
        }
    }

    private void GameEnd()
    {
		GameObject[] popups = GameObject.FindGameObjectsWithTag("Popup");
		for (int i = 0; i < popups.Length; i++) {
			Destroy(popups[i]);
		}
		if (GameObject.FindGameObjectWithTag ("End") == null) {
			Instantiate (endPrefab);
		}
    }
    
    private Solution[] GetSolutions()
    {
        return CurrentProblem
            .GetComponentsInChildren(typeof(Solution))
                .Cast<Solution>()
                .ToArray();
    }

    private GameObject CurrentProblem
    {
        get
        {
            return Problems[problemId];
        }
    }
    
}

