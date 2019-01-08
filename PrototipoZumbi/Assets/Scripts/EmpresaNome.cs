using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmpresaNome : MonoBehaviour {

	public string [] nomes;
	public int numEscolhido = 0;
	public int novoNum;

	// Use this for initialization
	void Start () {
		TrocarEmpresa();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TrocarEmpresa ()
	{
		novoNum = Random.Range(0,nomes.Length);

		//Caso o numero seja igual ao antigo, ele é reescolhido
		while(numEscolhido == novoNum)
			novoNum = Random.Range(0,nomes.Length);

		GetComponent<Text>().text = ""+nomes[novoNum];

		numEscolhido = novoNum;

		OldController.oldController.EscolherHabilidades();

		OldController.oldController.AtualizarRadar();
	}
}
