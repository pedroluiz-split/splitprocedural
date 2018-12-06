using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predios : MonoBehaviour {

	public static Predios predios;
	public static bool predioAtivado = false;
	
	void Awake ()
	{
		predios = this;
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

	public void DesativarTodosPredios ()
	{
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).GetComponent<Predio> ().GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.white);
			transform.GetChild (i).GetComponent<Predio> ().estaClicado = false;
			transform.GetChild (i).GetComponent<Predio> ().esperandoClique = false;

//			if (Predio.ultimoAtivo != null)
//				Predio.ultimoAtivo.GetComponent<Predio> ().GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.green);

			Predio.ultimoAtivo.GetComponent<Predio> ().estaClicado = false;
			Predio.ultimoAtivo.GetComponent<Predio> ().esperandoClique = false;

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
