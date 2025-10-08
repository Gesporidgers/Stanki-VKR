using UnityEngine;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Collections;

public class ObjectAttachment : MonoBehaviour
{
    [SerializeField]
    GameObject m_Child = null;
    [SerializeField]
    string Tag = "Drill_Attachment";

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (!other.gameObject.CompareTag(Tag)) // "Flag" - временная метка, нужно заменить
            return;

        var interact = other.gameObject.GetComponent<XRGrabInteractable>();
        if (interact != null)
        {
            if (interact.interactorsSelecting.Count != 0)
            {
                XRInteractionManager manager = new();
                foreach (var interaction in interact.interactorsSelecting)
                {
                    manager.SelectExit(interaction, interact);
                }
            }
        }
        other.gameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
        other.gameObject.transform.position = this.transform.position;
        m_Child = other.gameObject;
    }

    private void FixedUpdate()
    {
        // для закрепления.
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_Child == null) return;
        if (m_Child.transform.childCount > 0) return;
        m_Child = null;
    }
}