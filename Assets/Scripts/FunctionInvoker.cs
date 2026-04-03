using UnityEngine;
using UnityEngine.Events;

public class FunctionInvoker : MonoBehaviour
{
    public double time = 0.0;
    
    public UnityEvent Function;
    double timeElapsed = 0.0;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Function != null)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= time)
            {
                Function.Invoke();
                Function = null;
            }
        }
    }
}
