using UnityEngine;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Collections;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ObjectAttachment : MonoBehaviour
{
    private bool iHaveAttachment = false;
    [SerializeField]
    string Tag = "Drill_Attachment";

    private void OnTriggerEnter(Collider other)
    {
        if (iHaveAttachment)
            return;
        if (other == null) return;
        if (!other.gameObject.CompareTag(Tag))
            return;

        var interact = other.gameObject.GetComponent<XRGrabInteractable>();
        if (interact != null)
        {
            if (interact.interactorsSelecting.Count != 0)
            {
                XRInteractionManager manager = new();
                IXRSelectInteractor[] interactions = new IXRSelectInteractor[interact.interactorsSelecting.Count];
                interact.interactorsSelecting.CopyTo(interactions);
                foreach (var interaction in interactions)
                {
                    manager.SelectExit(interaction, interact);
                }
            }
        }
        other.gameObject.transform.rotation = this.transform.rotation;
        other.gameObject.transform.position = this.transform.position;
        other.gameObject.transform.parent = this.gameObject.transform;
        iHaveAttachment = true;
        Debug.Log(this.gameObject.transform.childCount);

        //m_Child = other.gameObject;
    }

    private void FixedUpdate()
    {
        // для закрепления.
    }

    private void OnTriggerExit(Collider other)
    {
        iHaveAttachment = false;
    }
}