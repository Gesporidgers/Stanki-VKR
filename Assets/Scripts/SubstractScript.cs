using UnityEngine;
using Parabox.CSG;
using Unity.VisualScripting;

using System.Collections;

public class SubstractScript : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	
	public GameObject target;
	
    /*private IEnumerator Cooldown()
    {

    }*/
    public void Substract()
    {
		var sourceComponent = gameObject;
		var offset = sourceComponent.transform.localPosition;
		var scaleOffset = sourceComponent.transform.localScale;
		var substracted = target;
		Model result = CSG.Subtract(sourceComponent, substracted);
		Mesh mesh = result.mesh;
		Vector3[] verts = mesh.vertices;
		for (int i = 0; i < verts.Length; i++)
		{
			verts[i] -= offset;
			verts[i].x /= scaleOffset.x;
			verts[i].y /= scaleOffset.y;
			verts[i].z /= scaleOffset.z;
		}
		mesh.vertices = verts;
		mesh.RecalculateBounds();
		sourceComponent.GetComponent<MeshFilter>().sharedMesh = mesh;
		sourceComponent.GetComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
		
	}
}
