using UnityEngine;
using UnityEngine.Animations;

public class AngleRotation : MonoBehaviour
{
    [SerializeField] 
    private Transform rotationSourse;

    [SerializeField]
    private float rotationSourseStartRotationAngle = 0f;

    [SerializeField] 
    private Axis rotationAxis = Axis.Y;

    [SerializeField]
    private Transform movingObject;

    [SerializeField]
    private Axis movementAxis = Axis.Y;

    [SerializeField]
    private float maxDistance = 0f;

    [SerializeField]
    private float rotationAngle = 360f;

    [SerializeField]
    private float totalRotations = 1f;

    [SerializeField]
    private bool invertDirection = false;

    [SerializeField]
    private float smoothness = 0.1f;

    private Vector3 startPosition;
    private Vector3 currentVelocity;
    private float prevAngle;
    private float totalRotationsDelta;

    private void Awake()
    {
        if (rotationSourse == null)
            rotationSourse = transform;
        if (movingObject == null)
            movingObject = transform;
    }

    private void Start()
    {
        startPosition = movingObject.localPosition;
        if (rotationAngle < 360f) totalRotations = 1f;
        totalRotationsDelta = 0f;
        prevAngle = getRawAngle();
    }

    private void FixedUpdate()
    {
        float currentAngle = getRawAngle();
        float delta = Mathf.DeltaAngle(prevAngle, currentAngle);
        if (delta <= 1e-7f) return;
        prevAngle = currentAngle;

        float newDelta = totalRotationsDelta + delta;

        float fullRotationAngle = rotationAngle * totalRotations;

        totalRotationsDelta = Mathf.Clamp(newDelta, 0f, fullRotationAngle);

        float normalizedProgress = totalRotationsDelta / fullRotationAngle;

        float targetOffset = normalizedProgress * maxDistance;

        if (invertDirection)
            targetOffset = maxDistance - targetOffset;

        Vector3 targetPosition = startPosition + GetOffsetVector(targetOffset);

        Vector3 newPosition;
        if (smoothness > 0.01f)
            newPosition = Vector3.SmoothDamp(movingObject.localPosition, targetPosition, ref currentVelocity, smoothness);
        else
            newPosition = targetPosition;

        movingObject.localPosition = newPosition;
    }
    private float getRawAngle()
    {
        Vector3 euler = rotationSourse.localEulerAngles;
        float rawAngle = GetAxisValue(euler, rotationAxis);

        return rawAngle;
    }

    private float GetAxisValue(Vector3 vec, Axis axis)
    {
        return axis switch
        {
            Axis.X => vec.x,
            Axis.Y => vec.y,
            Axis.Z => vec.z,
            Axis.None => 0f,
            _ => 0f
        };
    }

    private Vector3 GetOffsetVector(float offset)
    {
        return movementAxis switch
        {
            Axis.X => new Vector3(offset, 0, 0),
            Axis.Y => new Vector3(0, offset, 0),
            Axis.Z => new Vector3(0, 0, offset),
            _ => Vector3.zero
        };
    }
}
