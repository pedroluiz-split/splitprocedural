using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Predio : MonoBehaviour {

	public bool estaClicado = false;
	public GameObject infoPredioObjeto;
	public int qntZumbis;
	public int qntComidas;
	public int qntSobreviventes;
	private bool esperandoClique = false;
	private GameObject group;
	private Text dialogo;
	public GameObject deadbook;
	public int qntComida;

	// Use this for initialization
	void Start () {
		//infoPredioObjeto = transform.parent.parent.transform.transform.GetChild(0).GetChild(0).gameObject;
		group = transform.parent.parent.gameObject;
		qntComida = Random.Range(1,10);
		infoPredioObjeto.GetComponent<Text>().text = group.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text;
		qntZumbis = Random.Range(1,20);
	}

	void OnMouseDown ()
	{

		if (esperandoClique) {
			EntrarPredio();
		} 
		else {
			if (estaClicado) 
			{
				estaClicado = false;
				GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
				infoPredioObjeto.transform.parent.gameObject.SetActive(false);
			} else {
				estaClicado = true;
				StartCoroutine(EsperarClique());
				GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
				TrocarTextoDialogo();
				infoPredioObjeto.transform.parent.gameObject.SetActive(true);
			}
		}


    }

    void EntrarPredio ()
	{
		//Ativa o DeadBook
		deadbook.SetActive(true);

		//Coletar itens
		OldController.oldController.AdicionarComida(qntComida);

		//Passar dia
		OldController.oldController.AdicionarDias();

		estaClicado = false;
		esperandoClique = false;
		GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
		//Voltar para DeadBook
		Camera.main.orthographic = false;
		group.SetActive(false);

	}

	void TrocarTextoDialogo()
	{
		//Achar dialogo e trocar texto
		infoPredioObjeto.GetComponent<Text>().text = "Prédio "+transform.name + "\n\nZumbis: " + qntZumbis + "\nComida: "+qntComida;
	}

    IEnumerator EsperarClique ()
	{
		esperandoClique = true;
		yield return new WaitForSeconds(2);
		esperandoClique = false;
	}


}
