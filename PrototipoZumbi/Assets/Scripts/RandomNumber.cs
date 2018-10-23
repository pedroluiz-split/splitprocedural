using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomNumber : MonoBehaviour {

	public float min;
	public float max;
	private float numEscolhido;
	private string textoInicial;

	// Use this for initialization
	void Start () {
		textoInicial = GetComponent<Text>().text;
		MudarNumero();
	}
	
	public void MudarNumero ()
	{
		numEscolhido = Random.Range(min, max);
		GetComponent<Text>().text = textoInicial + (int)numEscolhido;
	}
}
