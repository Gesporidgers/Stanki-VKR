using UnityEngine;

public class AngleRotation : MonoBehaviour
{
    [SerializeField]
    public GameObject MovingObject;
    [SerializeField]
    public float minAngleValue;
    [SerializeField]
    public float maxAngleValue;
    
    private float minDistanceValue;
    [SerializeField]
    public float maxDistanceValue;

    private float dirivative;

    int counter = 0;


    private bool IsActive = false;

    public void StartAngleRecalculation()
    {
        IsActive = true;
        minDistanceValue = MovingObject.transform.localPosition.y;
    }

    public void StopAngleRecalculation()
    {
        IsActive = false;
    }

    private void FixedUpdate()
    {
        counter++;
        if (IsActive)
        {
            float angle = transform.rotation.z - minAngleValue;
            Debug.Log(transform.rotation.z);
            float distance = maxDistanceValue * (angle / (maxAngleValue - minAngleValue));
            Debug.Log("delta: " + distance);
            MovingObject.transform.Translate(new Vector3(MovingObject.transform.localPosition.x, minDistanceValue - distance, MovingObject.transform.localPosition.z));
        }
        if (counter > 20) counter = 0;
    }

    private void Start()
    {
        dirivative = maxAngleValue - minAngleValue;
    }
}
