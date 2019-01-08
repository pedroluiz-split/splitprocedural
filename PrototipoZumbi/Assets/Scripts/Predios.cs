using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predios : MonoBehaviour {

	public static Predios predios;
	public static bool predioAtivado = false;
	public static string [][] tiposPredios;
	public static List<GameObject> listaPredios;

	void Awake ()
	{
		predios = this;
	}

	public void EmbaralharPredios ()
	{
//		//Trocar de posicao entre predios do mesmo tamanho
//		switch (Random.Range (0, 4)) {
//		case 0: //Formação do primeiro tipo
//				//Não muda
//			break;
//			case 1: //Formação do segundo tipo
//				//Percorre os predios
//				for (int i = 0; i < transform.childCount; i++) {
//					if (transform.GetChild(i).name == "3")
//						listaPredios.Add(transform.GetChild(i).gameObject);
//				}
//			break;
//			case 2: //Formação do terceiro tipo
//
//			break;
//			case 3: //Formação do quarto tipo
//
//			break;
//		}
		Predio.podeProcurar = true;
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).localPosition = new Vector3(Random.Range(-150,150), transform.GetChild(i).localPosition.y,Random.Range(-150,150));

		}
		Predio.podeProcurar = false;
	}

	void Start ()
	{
//		tiposPredios = new string[2][];
//		tiposPredios[0] = new string[]{"3", "7", "30", "49", "50"};
//		tiposPredios[1] = new string[]{"5", "8", "10", "13", "27"};
		//EmbaralharPredios();
	}

	public void DesativarOutrosColliders (int excecao)
	{
		for (int i = 0; i < transform.childCount; i++) {
			if (i != excecao)
				transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;
		}
	}

	public void DesativarCollider(int col)
	{
		StartCoroutine(Reativar(col));

	}

	public void MudarAtivado (bool ativado)
	{
		predioAtivado = ativado;
	}

	IEnumerator Reativar (int col)
	{
		transform.GetChild(col).GetComponent<BoxCollider>().enabled = false;
		yield return new WaitForSeconds(1f);
		transform.GetChild(col).GetComponent<BoxCollider>().enabled = true;
	}

	public void AtivarColliders ()
	{
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).GetComponent<BoxCollider>().enabled = true;
		}
	}

	public void ResetarPredios ()
	{
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).GetComponent<Predio> ().GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.white);
			transform.GetChild (i).GetComponent<Predio> ().estaClicado = false;
			transform.GetChild (i).GetComponent<Predio> ().esperandoClique = false;
			transform.GetChild (i).GetComponent<Predio> ().jaEntrou = false;

//			if (Predio.ultimoAtivo != null)
//				Predio.ultimoAtivo.GetComponent<Predio> ().GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.green);
			if (Predio.ultimoAtivo != null) {
				Predio.ultimoAtivo.GetComponent<Predio> ().estaClicado = false;
				Predio.ultimoAtivo.GetComponent<Predio> ().esperandoClique = false;
				Predio.ultimoAtivo.GetComponent<Predio> ().jaEntrou = false;
			}
			AtivarColliders();

			Predio.ultimoAtivo = null;
		}
	}

	public void DesativarTodosPredios ()
	{
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).GetComponent<Predio> ().GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.white);
			transform.GetChild (i).GetComponent<Predio> ().estaClicado = false;
			transform.GetChild (i).GetComponent<Predio> ().esperandoClique = false;

//			if (Predio.ultimoAtivo != null)
//				Predio.ultimoAtivo.GetComponent<Predio> ().GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.green);
			if (Predio.ultimoAtivo != null) {
				Predio.ultimoAtivo.GetComponent<Predio> ().estaClicado = false;
				Predio.ultimoAtivo.GetComponent<Predio> ().esperandoClique = false;
			}
			AtivarColliders();

			Predio.ultimoAtivo = null;
		}
	}

	public void ReativarTodosPredios ()
	{
		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild(i).GetComponent<Predio>().jaEntrou)
				transform.GetChild(i).GetComponent<Predio>().GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.green);
			else
				transform.GetChild(i).GetComponent<Predio>().GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.white);
			transform.GetChild(i).GetComponent<Predio>().estaClicado = true;
		}
	}
}
