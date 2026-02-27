using UnityEngine;

public class UITabsSwitch : MonoBehaviour
{
    [SerializeField]
    Canvas Current;
    [SerializeField]
    Canvas Next;

    private void Start()
    {
        //Switch(); test
    }
    public void Switch()
    {
        Current.gameObject.SetActive(false);
        Next.gameObject.SetActive(true);
    }
}
