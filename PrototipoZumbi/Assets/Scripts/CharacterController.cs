using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

	public GameObject cabeca;
	public GameObject torso;
	public GameObject perna;
	public GameObject nomeTexto;
	public GameObject descricaoTexto;
	public GameObject statsTexto;

	private GameObject [] cabecas;
	private GameObject [] torsos;
	private GameObject [] pernas;

	public string [] nomes;
	public string [] sobrenomes;
	public string [] descricoes;

	private string nome, sobrenome, descricao;
	private string statsInicial;

	public static CharacterController controller;

	// Use this for initialization
	void Start () {
		controller = this;

		statsInicial = statsTexto.GetComponent<Text>().text;
		//colocar os sprites de cabeca em uma variavel, e desativá-las
		cabecas = new GameObject[cabeca.transform.childCount];
		for (int i = 0; i < cabeca.transform.childCount; i++)
		{
			cabecas[i] = cabeca.transform.GetChild(i).gameObject;
			cabeca.transform.GetChild(i).gameObject.SetActive(false);
		}

		//colocar os sprites de torso em uma variavel, e desativá-las
		torsos = new GameObject[torso.transform.childCount];
		for (int i = 0; i < torso.transform.childCount; i++)
		{
			torsos[i] = torso.transform.GetChild(i).gameObject;
			torso.transform.GetChild(i).gameObject.SetActive(false);
		}

		//colocar os sprites de perna em uma variavel, e desativá-las
		pernas = new GameObject[perna.transform.childCount];
		for (int i = 0; i < perna.transform.childCount; i++)
		{
			pernas[i] = perna.transform.GetChild(i).gameObject;
			perna.transform.GetChild(i).gameObject.SetActive(false);
		}

		Gerar_Personagem();
	}

	public void Gerar_Personagem ()
	{
		// CABEÇAS
		int cabeca_index = Random.Range (0, cabecas.Length);
		//Escolher uma cabeca aleatoria
		for (int i = 0; i < cabecas.Length; i++) 
		{
			if (i == cabeca_index)
				cabecas [i].SetActive (true);
			else
				cabecas [i].SetActive (false);
		}

		//TORSOS
		int torso_index = Random.Range (0, torsos.Length);
		//Escolher uma cabeca aleatoria
		for (int i = 0; i < torsos.Length; i++) 
		{
			if (i == torso_index)
				torsos [i].SetActive (true);
			else
				torsos [i].SetActive (false);
		}

		//PERNAS
		int perna_index = Random.Range (0, pernas.Length);
		//Escolher uma cabeca aleatoria
		for (int i = 0; i < pernas.Length; i++) 
		{
			if (i == perna_index)
				pernas [i].SetActive (true);
			else
				pernas [i].SetActive (false);
		}

		string [] stats = new string[4];

		nome = nomes[Random.Range(0,nomes.Length)];
		sobrenome = sobrenomes[Random.Range(0,sobrenomes.Length)];
		descricao = descricoes[Random.Range(0, descricoes.Length)];
		descricao = descricao.Replace("*nome*", nome);
		descricao = descricao.Replace("*sobrenome*", sobrenome);
//		stats[0] = descricao.Split('(')[1].Split(',')[0];
//		stats[1] = descricao.Split('(')[1].Split(',')[1];
//		stats[2] = descricao.Split('(')[1].Split(',')[2];
//		stats[3] = descricao.Split('(')[1].Split(',')[3];
		statsTexto.GetComponent<Text>().text = statsInicial;

		//NOME
		nomeTexto.GetComponent<Text>().text = nome + " "+ sobrenome;

		//DESCRICAO
		descricaoTexto.GetComponent<Text>().text = descricaoTexto.GetComponent<Text>().text.Split(':')[0]+": " + descricao;

		//STATUS
//		statsTexto.GetComponent<Text>().text = statsTexto.GetComponent<Text>().text.Replace("L0",stats[0].Replace("L",""));
//		statsTexto.GetComponent<Text>().text = statsTexto.GetComponent<Text>().text.Replace("B0",stats[1].Replace("B",""));
//		statsTexto.GetComponent<Text>().text = statsTexto.GetComponent<Text>().text.Replace("P0",stats[2].Replace("P",""));
//		statsTexto.GetComponent<Text>().text = statsTexto.GetComponent<Text>().text.Replace("C0",stats[3].Replace("C",""));
	}
}
