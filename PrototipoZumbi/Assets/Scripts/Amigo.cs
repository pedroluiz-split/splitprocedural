using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Amigo : MonoBehaviour {

	public string [] nomesPossiveis;
	public string [] profissoesPossiveis;
	public string [] descricoesPossiveis;
	public string nomeEscolhido;
	public string profissaoEscolhida;
	public string descricaoEscolhida;
	public GameObject telaAmigo;
	private TextAsset nomeMascText;
	private TextAsset nomeFemText;
	public bool personagemMasc;

	// Use this for initialization
	void Start ()
	{
		EscolherTudo();
	}
	
	public void EscolherNome ()
	{
		nomeEscolhido = nomesPossiveis[Random.Range(0,nomesPossiveis.Length)];
	}

	public void EscolherProfissao ()
	{
		profissaoEscolhida = profissoesPossiveis[Random.Range(0,profissoesPossiveis.Length)];
	}

	public void EscolherDescricao ()
	{
		descricaoEscolhida = descricoesPossiveis[Random.Range(0,descricoesPossiveis.Length)];
	}

	public void EscolherTudo ()
	{
		if (personagemMasc) {
			nomeMascText = Resources.Load ("NomesMasculinos") as TextAsset;
			nomesPossiveis = nomeMascText.text.Split (new string[]{ "\n" }, System.StringSplitOptions.None);
		} 
		else {
			nomeFemText = Resources.Load ("NomesFemininos") as TextAsset;
			nomesPossiveis = nomeFemText.text.Split (new string[]{ "\n" }, System.StringSplitOptions.None);
		}

		EscolherNome();
		EscolherProfissao();
		EscolherDescricao();
	}

	void OnMouseDown()
    {
        Debug.Log("Clicou no amigo " + nomeEscolhido);
        telaAmigo.SetActive(true);
        telaAmigo.transform.GetChild(0).GetComponent<Image>().sprite = GetComponent<SpriteRenderer>().sprite;
		telaAmigo.transform.GetChild(1).GetComponent<Text>().text = "Nome: "+ nomeEscolhido;
		telaAmigo.transform.GetChild(2).GetComponent<Text>().text = "Profissão: "+ profissaoEscolhida;
		telaAmigo.transform.GetChild(3).GetComponent<Text>().text = "Descrição: "+ descricaoEscolhida;
    }
}
