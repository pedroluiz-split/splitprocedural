using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilitario : MonoBehaviour {

	public static bool amigoAtivo = false;
	public bool jaAtualizou = false;


	public void ReaparecerGrafico (GameObject grafico)
	{
		grafico.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
	}

	public void DesaparecerGrafico (GameObject grafico)
	{
		grafico.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
	}
}
