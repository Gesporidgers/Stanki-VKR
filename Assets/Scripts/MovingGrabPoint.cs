using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class MovingGrabPoint : MonoBehaviour
{
    [Header("Компоненты")]
    [SerializeField] private XRGrabInteractable grabInteractable;
    [SerializeField] private HingeJoint hingeJoint;
    [SerializeField] private Transform grabPoint;

    public void Awake()
    {
        if (grabInteractable == null) grabInteractable = GetComponentInParent<XRGrabInteractable>();
        if (grabPoint == null) grabPoint = transform;
        if (hingeJoint == null) hingeJoint = GetComponentInParent<HingeJoint>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Vector3 localOffset = grabInteractable.transform.InverseTransformPoint(grabPoint.position);

        grabInteractable.attachTransform = grabPoint;

        Rigidbody rb = grabInteractable.GetComponent<Rigidbody>();
        if(rb != null)
        {
            XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;
            if(interactor != null)
            {
                Vector3 targetVel = (-interactor.transform.position + grabPoint.position) / Time.fixedDeltaTime;
                rb.linearVelocity = Vector3.ClampMagnitude(targetVel, 5f);
            }
        }
    }
}
