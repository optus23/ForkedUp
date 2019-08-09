using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class OrderPrefabs : MonoBehaviour 
{
	int decal = 6;

	void Update() 
	{
		int i = 0;
		int j = 0;

		foreach(Transform t in transform)
		{
			t.position = new Vector3(i * decal, j * decal, 0);

			i++;

			if(i%5 == 0)
			{
				i = 0;
				j++;
			}
		}
	}
}