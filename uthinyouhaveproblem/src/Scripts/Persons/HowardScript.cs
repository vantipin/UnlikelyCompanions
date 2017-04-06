using UnityEngine;
using System.Collections;

public class HowardScript : Person {

	public override void Effect()
    {
		//Debug.Log("Howard injured himself");
		InjurePerson(1);
    }
}
