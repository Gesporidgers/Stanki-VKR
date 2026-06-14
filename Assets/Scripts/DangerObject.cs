using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class DangerObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void CheckEnabled()
    {
        if (gameObject.GetComponentInParent<Stanok>().work == 1)
        {
            gameObject.GetComponentInParent<Stanok>().Switch();
            HapticsUtility.SendHapticImpulse(0.5f, 0.5f, HapticsUtility.Controller.Both);
            SceneManager.LoadScene("GameOver");

        }

    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            CheckEnabled();
        }
    }
}
