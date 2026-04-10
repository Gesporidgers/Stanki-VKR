using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class MovingGrabPoint : XRBaseGrabTransformer
{
    [Header("Компоненты")]
    [SerializeField] private HingeJoint hingeJoint;
    [SerializeField] private Transform grabPoint;

    private XRBaseInteractor interactor;
    private Rigidbody rb;

    public void Awake()
    {
        if (grabPoint == null) grabPoint = transform;
        if (hingeJoint == null) hingeJoint = GetComponent<HingeJoint>();

        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            rb = grabInteractable.GetComponent<Rigidbody>();

            grabInteractable.trackPosition = false;
            grabInteractable.trackRotation = false;
            grabInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;
            if (GetComponent<XRSingleGrabFreeTransformer>() == null)
            {
                this.AddComponent<XRSingleGrabFreeTransformer>();
                grabInteractable.startingSingleGrabTransformers.Add(GetComponent<XRSingleGrabFreeTransformer>());
                grabInteractable.startingSingleGrabTransformers.Add(GetComponent<MovingGrabPoint>());
            }
            else if (grabInteractable.startingSingleGrabTransformers.Count == 0)
            {
                grabInteractable.startingSingleGrabTransformers.Add(GetComponent<XRSingleGrabFreeTransformer>());
                grabInteractable.startingSingleGrabTransformers.Add(GetComponent<MovingGrabPoint>());
            }
            Debug.Log("Start up successful");
        }
        else Debug.Log("Ошибка, ненайден скрипт XRGrabInteractable");
    }

    public override void OnLink(XRGrabInteractable grabInteractable)
    {
        Debug.Log("Link function call");
        //base.OnLink(grabInteractable);
        
    }

    public override bool canProcess => true;

    protected override RegistrationMode registrationMode => RegistrationMode.SingleAndMultiple;

    public override void Process(XRGrabInteractable grabInteractable, XRInteractionUpdateOrder.UpdatePhase updatePhase, ref Pose targetPose, ref Vector3 localScale)
    {
        Debug.Log("Programm working");
        if (updatePhase != XRInteractionUpdateOrder.UpdatePhase.Fixed) return;

        if (grabInteractable.interactorsSelecting.Count == 0) return;
        interactor = grabInteractable.interactorsSelecting[0] as XRBaseInteractor;

        Vector3 handPosition = interactor.GetAttachTransform(grabInteractable).position;

        Vector3 worldPosition = grabPoint.position;

        Vector3 hingePosition = hingeJoint.transform.TransformPoint(hingeJoint.anchor);

        Vector3 direction = handPosition - worldPosition;

        Vector3 lever = worldPosition - hingePosition;

        Vector3 axis = hingeJoint.transform.TransformDirection(hingeJoint.axis).normalized;

        float force = Vector3.Dot(direction, Vector3.Cross(axis, lever).normalized);

        Vector3 torque = axis * force * 10f;

        rb.AddTorque(torque, ForceMode.Force);

        rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, 3f);

        Debug.Log($"angularVel: {rb.angularVelocity}");
    }

    public override void OnGrab(XRGrabInteractable grabInteractable)
    {
        Debug.Log("Есть касание");
        base.OnGrab(grabInteractable);
    }
}
