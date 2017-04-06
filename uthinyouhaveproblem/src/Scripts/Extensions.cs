using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
	public static Person[] Alive(this IEnumerable<Person> persons)
	{
		return persons.Where(p => p.hp > 0).ToArray();
	}

	public static void ForEach(this IEnumerable<Person> persons, Action<Person> action)
	{
		foreach(var person in persons)
		{
			action(person);
		}
	}
}


