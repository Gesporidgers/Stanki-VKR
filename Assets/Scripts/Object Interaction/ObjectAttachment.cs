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
    [SerializeField]
    string Tag = "Drill_Attachment";

    public bool isAttached() => HaveAttachment;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if (HaveAttachment)
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
        
        FixedJoint fj = other.gameObject.GetComponent<FixedJoint>();
        if(fj != null)
            fj.connectedBody = other.gameObject.GetComponent<Rigidbody>();

        HaveAttachment = true;
        //ParentColliderUpdate();

        //Debug.Log(this.gameObject.transform.childCount);
    }

    private void FixedUpdate()
    {
        // для закрепления.
    }

    private void OnTriggerExit(Collider other)
    {
        FixedJoint fj = other.gameObject.GetComponent<FixedJoint>();
        if (fj != null)
            fj.connectedBody = null;
        HaveAttachment = false;
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