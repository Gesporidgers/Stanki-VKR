using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float time = 0f;
    private bool isRunning = false;
    [HideInInspector]
    public float duration = 0f;
    [HideInInspector]
    public Action doAfter;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRunning)
        {
            time += Time.deltaTime;
			if (time >= duration)
            {
                isRunning = false;
                doAfter.Invoke();
            }
		}
    }

    public void StartTimer() { isRunning = true; time = Time.deltaTime; }
    public void StopTimer()
    {
        if (isRunning)
        {
            time = 0; isRunning = false;
        }
    }
}
