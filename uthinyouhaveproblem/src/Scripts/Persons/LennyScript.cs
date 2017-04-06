using UnityEngine;
using System.Collections;

public class LennyScript : Person {

	public override void Effect()
	{
		//Debug.Log("Lenny commit suicide");
		InjurePerson(2);
	}
}
