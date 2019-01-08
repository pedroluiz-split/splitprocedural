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
	public static int amigoEscolhido = 0;
	public bool estaClicado = false;
	public static GameObject amigo;
	public GameObject radar;
	public GameObject camera1, camera2;
	public static GameObject ultimoAmigoAtivo;
	private GameObject selecaoPersonagens;
	public static Sprite ultimoSprite;

	// Use this for initialization
	void Start ()
	{
//		if (transform.parent.GetSiblingIndex() < 5)
//			radar = GameObject.Find("Group$").transform.GetChild(0).GetChild(2).GetChild(2).GetChild(0).gameObject;
		EscolherTudo ();
		transform.name = transform.name.Replace ("1(Clone)", transform.GetSiblingIndex ().ToString ());
		if (transform.name.Contains ("$")) {
			GetComponent<SpriteRenderer> ().color = new Color (0.5f, 0.5f, 0.5f, 1);
		}

		if (transform.parent.name.Contains("Group")) 
		{
			selecaoPersonagens = transform.parent.GetChild(0).GetChild(1).GetChild(0).GetChild(1).gameObject;
		}

	}

	void Update()
	{
//		if (ultimoAmigoAtivo != null)
//			if (transform.name == ultimoAmigoAtivo.name)
//				AtualizarRadar();
		//AtualizarRadar();
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

	}

	public void AtualizarRadar ()
	{
		radar.GetComponent<RadarGraph>().habilidades = listaHabilidades;
		//radar.transform.GetChild(0).GetComponent<RadarGraph>().DebugDrawPolygon(radar.transform.GetChild(0).GetComponent<RadarGraph>().posicaoInicial,radar.transform.GetChild(0).GetComponent<RadarGraph>().raio,radar.transform.GetChild(0).GetComponent<RadarGraph>().qntItens);
		radar.GetComponent<RadarGraph>().DebugDrawPolygon(radar.GetComponent<RadarGraph>().posicaoInicial,1,9);
	}

	public void ArrumarTelaAmigo ()
	{
		telaAmigo.SetActive (true);
		telaAmigo.transform.GetChild (0).GetComponent<Image> ().sprite = GetComponent<SpriteRenderer> ().sprite;
		telaAmigo.transform.GetChild (1).GetComponent<Text> ().text = "Nome: " + nomeEscolhido;
		telaAmigo.transform.GetChild (2).GetComponent<Text> ().text = "Profissão: " + profissaoEscolhida;
		telaAmigo.transform.GetChild (3).GetComponent<Text> ().text = "Descrição: " + descricaoEscolhida;
	}

	void OnMouseDown ()
	{
		
		//Ao clicar nos amigos da TimeLine
		if (!transform.name.Contains ("$")) {
			radar.transform.parent.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
			amigoEscolhido = transform.GetSiblingIndex ();
			//Amigos.amigos.AtualizarRadar (listaHabilidades);
			//radar.transform.GetChild(0).GetComponent<RadarGraph>().ChamarAtual(listaHabilidades);
			radar.SetActive (true);
			transform.parent.parent.gameObject.SetActive (false);
			//AtualizarRadar(listaHabilidades);
			//radar.transform.GetChild(0)
			ArrumarTelaAmigo ();
			AtualizarRadar ();

			//Ao clicar nos amigos para selecionar quem vai entrar no prédio 
		} else {
			estaClicado = !estaClicado;

			if (ultimoAmigoAtivo != null) {

				ultimoAmigoAtivo.GetComponent<SpriteRenderer> ().color = new Color (0.5f, 0.5f, 0.5f, 1);
				ultimoAmigoAtivo.GetComponent<Amigo> ().estaClicado = false;
				ListaPersonagens.totalCombate -= ultimoAmigoAtivo.GetComponent<Amigo> ().listaHabilidades[5];
				ultimoSprite = null;
			}

			if (estaClicado) {
				GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
				ultimoAmigoAtivo = this.gameObject;
				for (int i = 0; i > Amigos.amigos.transform.childCount; i++) {
					if (Amigos.amigos.transform.GetChild (i).name == this.name) {
						amigoEscolhido = i;

					}
				}
				amigoEscolhido = transform.GetSiblingIndex();
				ultimoSprite = this.GetComponent<SpriteRenderer>().sprite;
				ListaPersonagens.listaAmigos.Add(transform.GetSiblingIndex());
				ListaPersonagens.totalCombate += listaHabilidades[5];
				Debug.Log("Combate: "+ListaPersonagens.totalCombate);

			} else {
				GetComponent<SpriteRenderer> ().color = new Color (0.5f, 0.5f, 0.5f, 1);
				ultimoAmigoAtivo = null;
				amigoEscolhido = 0;
				ultimoSprite = null;
				ListaPersonagens.listaAmigos.Remove(transform.GetSiblingIndex());
				ListaPersonagens.totalCombate -= listaHabilidades[5];
				Debug.Log("Combate: "+ListaPersonagens.totalCombate);
			}


			//Debug.Log("Mudou cameras");
			//Camera.main.gameObject.SetActive(false);
//			camera1.SetActive(false);
//			camera2.SetActive(true);

			Debug.Log(amigoEscolhido);
		}


	}

    public void EscolherPersonagem (int amigo)
	{
		//amigoEscolhido = amigo;
		if (amigo != 0) {
			estaClicado = false;
			//Coloca no amigo escolhido
			Debug.Log ("Amigo " + amigo);
			//ListaPersonagens.listaPersonagens.TrocarImagemBotao (amigoEscolhido);
			//ListaPersonagens.listaPersonagens.transform.parent.GetChild (1).gameObject.SetActive (true);
			//ListaPersonagens.listaPersonagens.gameObject.SetActive (false);
		}

		Destroy (transform.parent.gameObject);
	}
}
