using UnityEngine;
using System.Linq;
using System.Collections;

public class ZackScript : Person {

	public override void Effect()
	{
		var persons =  Main.Persons.Alive().Except(new[]{ this }).ToArray();
		var person = persons[Random.Range(0, persons.Length)];
		EffectMessage += " " + person.Name;
		//Debug.Log("Zack injured " + person.Name);
		person.InjurePerson(1);
	}
}
