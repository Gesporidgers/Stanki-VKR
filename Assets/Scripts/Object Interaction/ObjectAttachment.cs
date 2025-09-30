using UnityEngine;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectAttachment : MonoBehaviour
{
    [SerializeField]
    GameObject m_Child = null;

    public GameObject Bindings
    {
        get => m_Child;
        set { m_Child = value; }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (m_Child != null) return;
        if (!other.gameObject.CompareTag("Flag")) // "Flag" - временная метка, нужно заменить
            return;

        var interact = other.gameObject.GetComponent<XRGrabInteractable>();
        if (interact != null)
        {
            if (interact.interactorsSelecting.Count != 0)
            {
                XRInteractionManager manager = new();
                manager.SelectExit(interact.interactorsSelecting[0], interact);
                if (interact.interactorsSelecting.Count > 1)
                {
                    manager.SelectExit(interact.interactorsSelecting[1], interact);
                }
            }
        }
        other.gameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
        other.gameObject.transform.position = this.transform.position;
        m_Child = other.gameObject;
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_Child == null) return;
        if (m_Child.transform.childCount > 0) return;
        m_Child = null;
    }
}