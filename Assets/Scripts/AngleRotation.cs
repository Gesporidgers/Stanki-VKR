using UnityEngine;

public class AngleRotation : MonoBehaviour
{
    [SerializeField]
    public GameObject MovingObject;
    [SerializeField]
    public float minAngleValue;
    [SerializeField]
    public float maxAngleValue;
    public float minDistanceValue;
    [SerializeField]
    public float maxDistanceValue;

    private float dirivative;


    private bool IsActive = false;

    void StartAngleRecalculation()
    {
        IsActive = true;
        minDistanceValue = MovingObject.transform.localPosition.y;
    }

    void StopAngleRecalculation()
    {
        IsActive = false;
    }

    private void FixedUpdate()
    {
        if (IsActive)
        {
            float angle = transform.rotation.z - minAngleValue;
            float distance = maxDistanceValue * (angle / (maxAngleValue - minAngleValue));
            MovingObject.transform.localPosition.Set(MovingObject.transform.localPosition.x, minDistanceValue - distance, MovingObject.transform.localPosition.z);
        }
    }

    private void Start()
    {
        dirivative = maxAngleValue - minAngleValue;
    }
}
