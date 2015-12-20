using UnityEngine;
using System.Collections;

public class CarInformation : MonoBehaviour 
{
	public float Speed{get;set;}
	public float MinSpeed{get; private set;}
	public float MaxSpeed{get; private set;}

	void Awake()
	{
		MinSpeed = 0f;
		MaxSpeed = 220f;
	}
}
