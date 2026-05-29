using UnityEngine;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Collections;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ObjectAttachment : MonoBehaviour
{
    private bool HaveAttachment = false;
    private GameObject m_attachedObject;
    [SerializeField]
    string Tag = "Drill_Attachment";

    public bool isAttached() => HaveAttachment;

    private void OnTriggerEnter(Collider other)
    {
        if (HaveAttachment)
            return;
        Debug.Log("Collision");
        if (other == null) return;
        if (!other.gameObject.CompareTag(Tag))
            return;

        var interact = other.gameObject.GetComponent<XRGrabInteractable>();
        if (interact != null)
        {
            if (interact.interactorsSelecting.Count != 0)
            {
                var manager = interact.interactionManager;
                IXRSelectInteractor[] interactions = new IXRSelectInteractor[interact.interactorsSelecting.Count];
                interact.interactorsSelecting.CopyTo(interactions);
                foreach (var interaction in interactions)
                {
                    manager.SelectExit(interaction, interact);
                }
            }
        }

        var rb = other.gameObject.GetComponent<Rigidbody>();

        rb.MovePosition(transform.position);
        rb.MoveRotation(transform.rotation);


        FixedJoint fj = other.gameObject.GetComponent<FixedJoint>();
        if(fj != null)
            fj.connectedBody = gameObject.GetComponent<Rigidbody>();

        HaveAttachment = true;
        m_attachedObject = other.gameObject;
        //ParentColliderUpdate();

        //Debug.Log(this.gameObject.transform.childCount);
    }

    private void FixedUpdate()
    {
        // для закрепления.
    }

    private void OnTriggerExit(Collider other)
    {
        if(!HaveAttachment) return;
        if (other.gameObject != m_attachedObject) return;
        Debug.Log("Exit");
        //ParentColliderUpdate();
    }

    public void ParentColliderUpdate()
    {
        if (gameObject.GetComponentInParent<AttachableObject>() != null)
        {
            int n = gameObject.transform.parent.childCount;
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                count += gameObject.transform.parent.GetChild(i).childCount;
            }

            if (count == 0)
            {
                gameObject.transform.parent.GetComponent<Collider>().enabled = true;
            }
            else
            {
                gameObject.transform.parent.GetComponent<Collider>().enabled = false;
            }
        }
    }
}