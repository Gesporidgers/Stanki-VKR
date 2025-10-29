using UnityEngine;
using Parabox.CSG;

public class SubstractScript : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void OnTriggerEnter(Collider other)
	{
		var subtracted = other.gameObject;
		var sourceComponent = gameObject;
		var transf = sourceComponent.transform;
		Model result = CSG.Subtract(sourceComponent, subtracted);
		sourceComponent.transform.position = new Vector3(0, 0, 0);
		sourceComponent.GetComponent<MeshFilter>().sharedMesh = result.mesh;
		sourceComponent.GetComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
		sourceComponent.transform.position = transf.position;
	}
}
