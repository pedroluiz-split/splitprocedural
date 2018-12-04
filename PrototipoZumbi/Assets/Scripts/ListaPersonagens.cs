using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListaPersonagens : MonoBehaviour {

	public GameObject personagens;
	public Vector3 posLista;
	public float scaleLista;

	GameObject novaLista;
	public int botao;
	public static ListaPersonagens listaPersonagens;
	public GameObject selecaoPersonagens;
	public GameObject predios;
	public List<string> listaAmigos;
	private Vector3 originalScale;
	public GameObject camera1;
	public GameObject camera2;
	public GameObject radar;

	void Awake ()
	{
		listaPersonagens = this;
	}

	void Start ()
	{
		listaAmigos = new List<string>();
	}

	public void SetarBotao (int num)
	{
		botao = num;
	}

	public void InstanciarLista ()
	{
		//Instancia nova lista de amigos
		novaLista = Instantiate (personagens, posLista, transform.rotation, transform.parent.parent);
		novaLista.GetComponent<Amigos> ().limLinha = 10;

		if (originalScale == null)
			originalScale = novaLista.transform.localScale;
		//novaLista.transform.localScale = novaLista.transform.localScale * 0.7f;
		RetirarAmigosUsados ();


		novaLista.GetComponent<Amigos> ().enabled = false;
		//novaLista.transform.localScale = novaLista.transform.localScale * scaleLista;
		AcrescentarNome(novaLista,"$");
		TrocarCamera(camera2, camera1);
		novaLista.transform.localScale = novaLista.transform.localScale*50;
		novaLista.transform.position = novaLista.transform.position;
//		for (int i = 0; i < novaLista.transform.childCount; i++) {
//			novaLista.transform.GetChild(i).position = new Vector2(-20,0);
//		}
		
		//StartCoroutine (Teste());
		StartCoroutine (novaLista.GetComponent<Amigos> ().ReordenarPosicoes (transform.position.x, transform.position.y));
		DesmarcarAmigos(novaLista);
		//StartCoroutine(TrocarPos(posLista));
	}

	public IEnumerator Teste ()
	{
		yield return new WaitForSeconds(11f);
//		for (int i = 0; i < novaLista.transform.childCount; i++) {
//			novaLista.transform.GetChild(i).position = new Vector2(-20,0);
//		}
		

	}

	public void TrocarCamera (GameObject cameraAntiga, GameObject cameraNova)
	{
		cameraAntiga.SetActive(false);
		cameraNova.SetActive(true);
	}

	public void AcrescentarNome (GameObject amigos, string adicional)
	{
		for (int i = 0; i < amigos.transform.childCount; i++) {
			amigos.transform.GetChild(i).transform.name = amigos.transform.GetChild(i).transform.name + adicional;
		}
		amigos.transform.parent.name = amigos.transform.parent.name + "$";
	}

	public void EscolherAmigo ()
	{
		if (Amigo.amigoEscolhido != 0 && Amigo.amigoEscolhido != null)
			novaLista.transform.GetChild(Amigo.amigoEscolhido).GetComponent<Amigo>().EscolherPersonagem();
	}

	public void DesmarcarAmigos (GameObject amigos)
	{
		for (int i = 0; i < amigos.transform.childCount; i++) 
		{
			amigos.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(0.5f,0.5f,0.5f,1);
		}
	}

	public void DesativarLista ()
	{
		if (novaLista != null)
			Destroy(novaLista.gameObject);

		
	}

	public void RetirarAmigosUsados ()
	{
		for (int i = 2; i < novaLista.transform.childCount; i++) {
			if (listaAmigos.Contains (novaLista.transform.GetChild (i).name)) {
				Debug.Log("Destruido");
				Destroy (novaLista.transform.GetChild (i).gameObject);
			}
		}
	}

	public void DesativarColliderPredios (string nomeExcecao)
	{
		for (int i = 0; i < predios.transform.childCount; i++) {
			if (predios.transform.GetChild(i).name != nomeExcecao)
				predios.transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;
		}
	}

	public void AtivarColliderPredios ()
	{
		for (int i = 0; i < predios.transform.childCount; i++) {
			predios.transform.GetChild(i).GetComponent<BoxCollider>().enabled = true;
		}
	}

	public IEnumerator TrocarPos (Vector3 pos)
	{
		yield return new WaitForSeconds(0.01f);
		novaLista.transform.Translate(posLista);
	}

	public void InvadirPredio ()
	{
		if (Predio.ultimoAtivo != null)
			Predio.ultimoAtivo.GetComponent<Predio>().EntrarPredio();
		
	}

	public void TrocarImagemBotao (int num)
	{
		//Caso ja tenha um amigo naquele botao, remover o nome desse amigo da lista
		if (listaAmigos.Contains (selecaoPersonagens.transform.GetChild (botao).name))
			listaAmigos.Remove (selecaoPersonagens.transform.GetChild (botao).name);
		listaAmigos.Add (novaLista.transform.GetChild (num).name);

//		StartCoroutine(novaLista.GetComponent<Amigos>().ReordenarPosicoes());
		selecaoPersonagens.transform.GetChild(botao).GetComponent<Image>().sprite = novaLista.transform.GetChild(num).GetComponent<SpriteRenderer>().sprite;
		//Tira o + de cada botao
		selecaoPersonagens.transform.GetChild(botao).GetChild(0).gameObject.SetActive(false);
		selecaoPersonagens.transform.GetChild(botao).GetComponent<Image>().color = new Color(1,1,1,1);
	}
} 