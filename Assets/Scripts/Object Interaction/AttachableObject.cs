using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AttachableObject : MonoBehaviour
{

    public Transform StartSize;
    public bool isChildAtStart;
    private Transform scale;
    private bool isChildScale;

    private void Awake()
    {
        if (StartSize == null) StartSize = transform;
    }

    void Start()
    {
        scale = StartSize;
        isChildScale = isChildAtStart;
    }

    // Update is called once per frame
    public void Resize()
    {
        if (isChildScale && this.transform.parent != null || !isChildScale && this.transform.parent == null)
        {
            this.transform.localScale = scale.localScale;
        }
    }
}
