using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class strDir
{
    public bool X;
    public bool Y;
    public bool Z;
}

public class ObjectRotation : MonoBehaviour
{
    private float CurrentSpeed;
    private int CurrentSpeedId;

    [SerializeField]
    public strDir Direction;
    [SerializeField]
    List<float> RotationSpeeds = new List<float>() { 10f };
    public GameObject StanokHolder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentSpeed = RotationSpeeds[0];
        CurrentSpeedId = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int work = StanokHolder.GetComponent<Stanok>().work;
        transform.Rotate(
            Convert.ToInt32(Direction.X) * CurrentSpeed * work,
            Convert.ToInt32(Direction.Y) * CurrentSpeed * work,
            Convert.ToInt32(Direction.Z) * CurrentSpeed * work
        );
    }
    public void IncreaseRotationSpeed()
    {
        CurrentSpeedId++;
        if (CurrentSpeedId >= RotationSpeeds.Count) CurrentSpeedId = RotationSpeeds.Count - 1;
        CurrentSpeed = RotationSpeeds[CurrentSpeedId];
    }
    public void DecreaseRotationSpeed()
    {
        CurrentSpeedId--;
        if (CurrentSpeedId < 0) CurrentSpeedId = 0;
        CurrentSpeed = RotationSpeeds[CurrentSpeedId];
    }
}
