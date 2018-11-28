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
}
