using UnityEngine;
using Parabox.CSG;
using Unity.VisualScripting;

using System.Collections;

public class SubstractScript : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created

	public GameObject target;
	public ParticleSystem particles;


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == target)
		{
			particles.Play();
			gameObject.GetComponent<Timer>().duration = 1f;
			gameObject.GetComponent<Timer>().doAfter = Substract;
			gameObject.GetComponent<Timer>().StartTimer();
		}


	}
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

		gameObject.GetComponent<Collider>().enabled = false;
		gameObject.GetComponent<Timer>().duration = 5f;
		gameObject.GetComponent<Timer>().doAfter = () => { sourceComponent.GetComponent<Collider>().enabled = true; Debug.Log("Invoked"); };
		gameObject.GetComponent<Timer>().StartTimer();
		particles.Stop();
	}

	public void OnTriggerExit(Collider other)
	{
		if (gameObject.GetComponent<Timer>().duration != 5f)
			gameObject.GetComponent<Timer>().StopTimer();
	}
}
