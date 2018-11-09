using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Secret : MonoBehaviour {

	public int contador = 0;

	// Use this for initialization
	void Start () {
		StartCoroutine(ZerarContador());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown ()
	{
		contador++;
		if (contador > 10)
			SceneManager.LoadScene("GoogleMaps");
	}

	IEnumerator ZerarContador()
	{
		contador = 0;
		yield return new WaitForSeconds(5);
		StartCoroutine(ZerarContador());
	}
}
