using UnityEngine;
using System.Collections;

public class LucyScript : Person {

	public override void Effect()
	{
		//Debug.Log("Lucy demoralized everyone");
		Main.Persons.Alive().ForEach(p => 
        {
			p.Demorale(Main.MoralePerInjury, this);
		});
	}
}
