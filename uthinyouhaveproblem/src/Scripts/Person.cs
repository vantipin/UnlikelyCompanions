using UnityEngine;
using System.Collections;
using System.Linq;

[System.Serializable]
public class PersonReputation
{
    public Person person;
    public float rate;
}

public abstract class Person : MonoBehaviour {

    public PersonReputation[] Reputations;

	public Sprite avatar;
    public string Name;
    public int morale;
	public int oldHp;
    public int hp;
    public bool highlighted = false;
	public bool oldEffected = false;
	public bool effected = false;

	public string EffectMessage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract void Effect();

	public void DoDamage(int amount) {
		oldHp = hp;
		hp -= amount; 
		hp = hp < 0 ? 0 : hp;
	}

	public void Demorale(int amount, Person who) {
		morale -= amount;
		morale = morale < 0 ? 0 : morale;

		//Debug.Log(who.Name + " => " + Name + " morale -" + amount + " = " + morale);
	}

	public float GetReputation(Person person)
	{
		float rate = Reputations
			.Where(r => r.person == person)
			.Select(r => r.rate)
			.Concat(new float[] { 1 })
			.FirstOrDefault();

		return rate;
	}

	public void InjurePerson(int amount)
	{
		DoDamage(amount);

		int minusMorale = hp == 0 ? Main.MoralePerDeath : Main.MoralePerInjury;

		var personMoraleRemovals = Main.Persons.Alive()
			.Select(p =>
			{
				float rate = p.GetReputation(this);
				int moraleRemoval = (int)(minusMorale * rate);
				return new { Person = p, MoraleRemoval = moraleRemoval };
			})
			.ToArray();

		foreach(var pmr in personMoraleRemovals)
		{
			pmr.Person.Demorale(pmr.MoraleRemoval, this);
		}
		
		string message = string.Format("{0} -{1}={2}hp => {3}", Name, amount, hp, personMoraleRemovals
		                               .Aggregate("", (msg, pmr) => msg += pmr.Person.Name + " -" + pmr.MoraleRemoval + "=" + pmr.Person.morale + "; "));
		Debug.Log(message);
	}
	
	public static void UpdateMorale()
	{
		var persons = Main.Persons.Where(p => p.morale == 0 && !p.effected);
		do
		{
			var zeroMoralePersons = persons.ToArray();
			if (zeroMoralePersons.Length > 0)
			{
				string msg = "";
				foreach(var p in zeroMoralePersons)
				{
					p.Effect(); p.oldEffected = p.effected; p.effected = true;
					msg += p.EffectMessage + "; ";
				}
				Debug.Log (msg);
			}
		}
		while(persons.Any());
	}
}
