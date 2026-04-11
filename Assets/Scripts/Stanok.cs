using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class EngineSound
{
	public AudioSource StartEngine; 
	public AudioSource StopEngine; 
	public AudioSource IdleEngine;
	public IEnumerator Start()
	{
		StartEngine.Play();
		yield return new WaitUntil(()=>!StartEngine.isPlaying);
		IdleEngine.Play();
	}
	public void Stop()
	{
		IdleEngine.Stop();
		StopEngine.Play();
	}
}

public class Stanok : MonoBehaviour
{
	[HideInInspector]
	public int work = 0;
	public List<float> MotorForces;
	private int forceIndex = 0;
	
	[SerializeField]
	public EngineSound Sound;

	public Canvas StanokUI;
	public List<Button> Buttons;
	public TMPro.TMP_Text Text;

    private void Start()
    {
		/*for (int i = 0; i < transform.childCount; i++)
		{
			Debug.Log(transform.GetChild(i).gameObject.name);
		}*/
		
		
	}

    public void Switch()
	{
		work = Convert.ToInt32(work == 0);
		if (work == 1)
		{
			JointMotor m = gameObject.GetComponent<HingeJoint>().motor;
			m.force = MotorForces[forceIndex];
			StanokUI.gameObject.SetActive(true);
			gameObject.GetComponent<HingeJoint>().motor = m;
			
			StartCoroutine(Sound.Start());
            Buttons[1].interactable = forceIndex - 1 > 0;
            Buttons[0].interactable = forceIndex + 2 < MotorForces.Count;
			this.Text.text = (MotorForces[0] / 10).ToString();
		}
		else
		{
			StanokUI.gameObject.SetActive(false);
			JointMotor m = gameObject.GetComponent<HingeJoint>().motor;
			m.force = 0;
			gameObject.GetComponent<HingeJoint>().motor = m;
			StopAllCoroutines();
			Sound.Stop();
			
		}
	}

	public void IncSpeed()
    {
		forceIndex++;
		JointMotor m = gameObject.GetComponent<HingeJoint>().motor;
		m.force = MotorForces[forceIndex];
		gameObject.GetComponent<HingeJoint>().motor = m;
		Buttons[1].interactable = forceIndex - 1 > 0;
		Buttons[0].interactable = forceIndex + 1 < MotorForces.Count;
		this.Text.text = (MotorForces[forceIndex] / 10).ToString();
		//return forceIndex + 2 > MotorForces.Count;
	}

	public void DecSpeed()
    {
		forceIndex--;
		JointMotor m = gameObject.GetComponent<HingeJoint>().motor;
		m.force = MotorForces[forceIndex];
		gameObject.GetComponent<HingeJoint>().motor = m;
		Buttons[1].interactable = forceIndex - 1 > 0;
		Buttons[0].interactable = forceIndex + 2 < MotorForces.Count;
		this.Text.text = (MotorForces[forceIndex] / 10).ToString();
		//return ;
	}
}
