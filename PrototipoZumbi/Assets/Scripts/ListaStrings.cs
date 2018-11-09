using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListaStrings : MonoBehaviour {

	public string [] objetos;
	private string textoOriginal;

	// Use this for initialization
	void Start () {
		textoOriginal = GetComponent<Text>().text;
		//MudarObjeto();
	}

	public void MudarObjeto ()
	{
		GetComponent<Text>().text = textoOriginal + objetos[Random.Range(0, objetos.Length)];
	}

}
