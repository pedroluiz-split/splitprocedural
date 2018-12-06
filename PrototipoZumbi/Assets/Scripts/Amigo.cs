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
	public bool estaClicado = false;
	public static GameObject amigo;
	public GameObject radar;
	public GameObject cameraCerta;
	public static GameObject ultimoAmigoAtivo;

	// Use this for initialization
	void Start ()
	{
//		if (transform.parent.GetSiblingIndex() < 5)
//			radar = GameObject.Find("Group$").transform.GetChild(0).GetChild(2).GetChild(2).GetChild(0).gameObject;
		EscolherTudo ();
		transform.name = transform.name.Replace ("1(Clone)", transform.GetSiblingIndex ().ToString ());
		if (transform.name.Contains("$")) {
			GetComponent<SpriteRenderer> ().color = new Color (0.5f, 0.5f, 0.5f, 1);
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
			radar.SetActive(true);
			transform.parent.parent.gameObject.SetActive (false);
			//AtualizarRadar(listaHabilidades);
			//radar.transform.GetChild(0)
			ArrumarTelaAmigo ();
			AtualizarRadar ();

			//Ao clicar nos amigos para selecionar quem vai entrar no prédio 
		} else {
			if (ultimoAmigoAtivo != null) {
				ultimoAmigoAtivo.GetComponent<SpriteRenderer> ().color = new Color (0.5f, 0.5f, 0.5f, 1);
				estaClicado = false;
			}
				transform.parent.parent.GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(true);
				//radar = transform.parent.parent.GetChild(0).GetChild(2).GetChild(2).gameObject;
				//radar.transform.GetComponent<Utilitario>().ReaparecerGrafico(radar.transform.gameObject);
				//Caso não tenha pai, ele verifica se aquele amigo está selecionado e troca a cor. Caso não esteja selecionado, troca a cor
				if (estaClicado) {
					//EscolherPersonagem ();
				if (amigoEscolhido != 0 && amigoEscolhido != null) {
						transform.parent.transform.GetChild (amigoEscolhido).GetComponent<SpriteRenderer> ().color = new Color (0.5f, 0.5f, 0.5f, 1);
						transform.parent.transform.GetChild (amigoEscolhido).GetComponent<Amigo> ().estaClicado = false;
					}
				} else {
				transform.parent.transform.GetChild (transform.GetSiblingIndex()).GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1);
					amigoEscolhido = transform.GetSiblingIndex ();
					estaClicado = true;
					radar.GetComponent<SpriteRenderer>().color = new Color (1,1,1,1);
//					if (radar != null) {
//						radar.GetComponent<RadarGraph>().ChamarAtual(listaHabilidades);
						//Amigos.amigos.AtualizarRadar (listaHabilidades);
					


				}
				Debug.Log("Mudou cameras");
				Camera.main.gameObject.SetActive(false);
				cameraCerta.SetActive(true);
				ultimoAmigoAtivo = this.gameObject;
				//transform.position = transform.parent.position;
				//StartCoroutine(transform.parent.GetComponent<Amigos>().ReordenarPosicoes());
			}


		}

		public void SelecionarAmigos ()
	{

	}
    //Caso o amigo escolhido não seja null ao clicar no botão, ele volta a ter a cor escura.

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
