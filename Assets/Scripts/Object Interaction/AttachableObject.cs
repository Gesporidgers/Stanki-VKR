using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AttachableObject : MonoBehaviour
{

    public Transform StartSize;
    public bool isChildObjectAtStart;
    private Transform scale;
    private bool isChildScale;

    void Start()
    {
        scale = StartSize;
        isChildScale = isChildObjectAtStart;
    }

    // Update is called once per frame
    public void Resize()
    {
        if (isChildScale && this.transform.parent != null || !isChildScale && this.transform.parent == null)
        {
            this.transform.localScale = scale.localScale;
            //gameObject.GetComponent<XRGrabInteractable>().retainTransformParent = true;
        }
    }
}
