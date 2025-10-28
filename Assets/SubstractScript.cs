using UnityEngine;
using Parabox.CSG;

public class SubstractScript : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void OnTriggerEnter(Collider other)
	{
		var sverlo = other.gameObject;
		var cube = gameObject;
		var transf = cube.transform;
		Model result = CSG.Subtract(cube, sverlo);
		
		gameObject.GetComponent<MeshFilter>().mesh = result.mesh;
		gameObject.GetComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
		cube.transform.position = transf.position;
		cube.transform.rotation = transf.rotation;
		
		gameObject.GetComponent<SubstractScript>().enabled = false;
		//gameObject.GetComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
	}
}
