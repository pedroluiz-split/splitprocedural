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
	public float [] listaHabilidades;		//Vai da ordem de 0 a 100
	public GameObject telaAmigo;
	private TextAsset nomeMascText;
	private TextAsset nomeFemText;
	public bool personagemMasc;
	public static int amigoEscolhido;
	private bool estaClicado = false;
	public static GameObject amigo;

	// Use this for initialization
	void Start ()
	{
		EscolherTudo ();
		transform.name = transform.name.Replace ("1(Clone)", transform.GetSiblingIndex ().ToString ());
		if (transform.parent.parent == null) {
			GetComponent<SpriteRenderer>().color = new Color(0.5f,0.5f,0.5f,1);
		}
	}

	public void EscolherHabilidades ()
	{
		listaHabilidades = new float[8];

		for (int i = 0; i < listaHabilidades.Length; i++) {
			if (i == 4) {
				listaHabilidades [i] = 100 - listaHabilidades[0];
			} else if (i == 6) {
				listaHabilidades [i] = 100 - listaHabilidades[2];
			}
			else
				listaHabilidades[i] = Random.Range(0,100);
		}

	}

	public void MandarEmbora ()
	{
		transform.parent = transform.parent.parent;
		Destroy(this.gameObject);
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
		EscolherHabilidades();

		//Amigos.amigos.AtualizarRadar (listaHabilidades);
	}

	void OnMouseDown ()
	{
		//Ao clicar nos amigos da TimeLine
		if (transform.parent.parent != null) {
			Debug.Log ("Clicou no amigo " + nomeEscolhido);
			amigoEscolhido = transform.GetSiblingIndex ();
			Amigos.amigos.AtualizarRadar (listaHabilidades);
			telaAmigo.SetActive (true);
			telaAmigo.transform.GetChild (0).GetComponent<Image> ().sprite = GetComponent<SpriteRenderer> ().sprite;
			telaAmigo.transform.GetChild (1).GetComponent<Text> ().text = "Nome: " + nomeEscolhido;
			telaAmigo.transform.GetChild (2).GetComponent<Text> ().text = "Profissão: " + profissaoEscolhida;
			telaAmigo.transform.GetChild (3).GetComponent<Text> ().text = "Descrição: " + descricaoEscolhida;
			transform.parent.parent.gameObject.SetActive (false);
		}	

		//Ao clicar nos amigos para selecionar quem vai entrar no prédio 
		else {
			//Caso não tenha pai, ele verifica se aquele amigo está selecionado e troca a cor. Caso não esteja selecionado, troca a cor
			if (estaClicado) {
				//EscolherPersonagem ();
			} else {
				if (amigoEscolhido != 0 && amigoEscolhido != null) {
					transform.parent.transform.GetChild (amigoEscolhido).GetComponent<SpriteRenderer> ().color = new Color (0.5f, 0.5f, 0.5f, 1);
					transform.parent.transform.GetChild (amigoEscolhido).GetComponent<Amigo>().estaClicado = false;
				}
				amigoEscolhido = transform.GetSiblingIndex ();
				estaClicado = true;
				GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
			}
		}
    }//Caso o amigo escolhido não seja null ao clicar no botão, ele volta a ter a cor escura.

    public void EscolherPersonagem ()
	{
		estaClicado = false;
		//Coloca no amigo escolhido
		Debug.Log ("Amigo " + amigoEscolhido);
		ListaPersonagens.listaPersonagens.TrocarImagemBotao (amigoEscolhido);
		ListaPersonagens.listaPersonagens.transform.parent.GetChild (1).gameObject.SetActive (true);
		ListaPersonagens.listaPersonagens.gameObject.SetActive (false);
		amigoEscolhido = 0;
		Destroy (transform.parent.gameObject);
	}
}
