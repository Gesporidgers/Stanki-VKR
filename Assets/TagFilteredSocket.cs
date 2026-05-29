using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TagFilteredSocket : XRSocketInteractor
{
    [SerializeField]
    private string allowedTag;

	public override bool CanSelect(IXRSelectInteractable interactable)
	{
		if (!base.CanSelect(interactable)) return false;
		return interactable.transform.CompareTag(allowedTag);
	}

	public override bool CanHover(IXRHoverInteractable interactable)
	{
		if (!base.CanHover(interactable)) return false;
		return interactable.transform.CompareTag(allowedTag);
	}
}
