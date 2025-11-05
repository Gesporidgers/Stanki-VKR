using UnityEngine;
using Parabox.CSG;
using Unity.VisualScripting;

public class SubstractScript : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void OnTriggerEnter(Collider other)
	{
		var subtracted = other.gameObject;
		var sourceComponent = gameObject;
		var offset = sourceComponent.transform.localPosition;

        Model result = CSG.Subtract(sourceComponent, subtracted);
		Mesh mesh = result.mesh;
		Vector3[] verts = mesh.vertices;
		for (int i = 0; i < verts.Length; i++)
			verts[i] -= offset;
		mesh.vertices = verts;
		mesh.RecalculateBounds();
		sourceComponent.GetComponent<MeshFilter>().sharedMesh = mesh;
		sourceComponent.GetComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
    }
}
