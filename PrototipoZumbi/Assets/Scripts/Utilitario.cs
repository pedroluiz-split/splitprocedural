using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilitario : MonoBehaviour {

	public GameObject grafico;


	public void ReaparecerGrafico ()
	{
		grafico.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
	}
}
