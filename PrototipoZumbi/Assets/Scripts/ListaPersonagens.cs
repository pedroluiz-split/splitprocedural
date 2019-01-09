using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListaPersonagens : MonoBehaviour {

	public GameObject personagens;
	public Vector3 posLista;
	public float scaleLista;
	public static float botaoAtivoAtual = 0;

	GameObject novaLista;
	public int botao;
	public static ListaPersonagens listaPersonagens;
	public GameObject selecaoPersonagens;
	public GameObject predios;
	public static List<int> listaAmigos;
	private Vector3 originalScale;
	public GameObject camera1;
	public GameObject camera2;
	public GameObject radar;
	public static float totalCombate = 0;

	void Awake ()
	{
		listaPersonagens = this;
	}

	void Start ()
	{
		listaAmigos = new List<int>();
	}

	public void SetarBotao (int num)
	{
		botao = num;
	}


	public void InstanciarLista ()
	{
		TrocarCamera(camera2,camera1);

		//Instancia nova lista de amigos
		novaLista = Instantiate (personagens, posLista, transform.rotation, transform.parent.parent);
		novaLista.GetComponent<Amigos> ().limLinha = 10;

		if (originalScale == null)
			originalScale = novaLista.transform.localScale;
		//novaLista.transform.localScale = novaLista.transform.localScale * 0.7f;

		RetirarAmigosUsados ();
		//StartCoroutine(novaLista.GetComponent<Amigos> ().ReordenarPosicoes(-10,0));

		//novaLista.transform.Translate(Vector3.left * 10);

		//novaLista.transform.localPosition = new Vector3 (-2303, 8259, 7344);

		//novaLista.transform.localScale = novaLista.transform.localScale * scaleLista;
		AcrescentarNome(novaLista,"$");
		//TrocarCamera(camera2, camera1);
		novaLista.transform.localScale = novaLista.transform.localScale*50;

		//novaLista.transform.position = novaLista.transform.position;
//		for (int i = 0; i < novaLista.transform.childCount; i++) {
//			novaLista.transform.GetChild(i).position = new Vector2(-20,0);
//		}
		
		//StartCoroutine (Teste());
		//StartCoroutine (novaLista.GetComponent<Amigos> ().ReordenarPosicoes (transform.position.x, transform.position.y));
		DesmarcarAmigos(novaLista);
		//StartCoroutine(TrocarPos(posLista));
		//if (listaAmigos.Count != 0)
			//listaAmigos.RemoveAt(0);

		StartCoroutine (novaLista.GetComponent<Amigos> ().ReordenarPosicoes (transform.position.x, transform.position.y));
		novaLista.GetComponent<Amigos> ().enabled = false;
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
			novaLista.transform.GetChild(Amigo.amigoEscolhido).GetComponent<Amigo>().EscolherPersonagem(Amigo.amigoEscolhido);
		//listaAmigos.Add(Amigo.amigoEscolhido);
		Amigo.amigoEscolhido = 0;

		camera2.SetActive(true);
		camera1.SetActive(false);
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
//		if (listaAmigos.Count < novaLista.transform.childCount-1) {
//			for (int i = 2; i < novaLista.transform.childCount; i++) {
//				if (listaAmigos.Contains (novaLista.transform.GetChild (i).GetSiblingIndex ())) {
//					Debug.Log ("Destruido");
//					Destroy (novaLista.transform.GetChild (i).gameObject);
//				}
//			}
//		}
			for (int i = 0; i < listaAmigos.Count; i++) {
				Destroy(novaLista.transform.GetChild(listaAmigos[i]).gameObject);
			}

//		if (transform.parent.parent.childCount > 2)
//				Destroy(transform.parent.parent.GetChild(2).gameObject);
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

		//Pegar o num e trocar a imagem daquele botao por outra

		//Caso ja tenha um amigo naquele botao, remover o nome desse amigo da lista
//		if (listaAmigos.Count > 0) {
//			if (listaAmigos.Contains (listaAmigos [Amigo.amigoEscolhido]))
//				listaAmigos.Remove (listaAmigos [Amigo.amigoEscolhido]);
//			else
//				listaAmigos.Add (novaLista.transform.GetChild (Amigo.amigoEscolhido).name);
//		}
//		StartCoroutine(novaLista.GetComponent<Amigos>().ReordenarPosicoes());
		StartCoroutine(EsperarETrocar(num));

		//Dict
		//TrocarCamera(camera2,camera1);

		//Destroy(novaLista);
	}

	public IEnumerator EsperarETrocar (int num)
	{
		if (Amigo.amigoEscolhido == 0) {
			yield return new WaitForSeconds(0.01f);
			StartCoroutine(EsperarETrocar(num));
		} else {
			if (selecaoPersonagens != null) 
			{
				//Troca o sprite pela cara do personagem
				Debug.Log(selecaoPersonagens.transform.GetChild(num).name);
				selecaoPersonagens.transform.GetChild (num).GetComponent<Image> ().sprite = Amigo.ultimoSprite;
				//Tira o + de cada botao
				selecaoPersonagens.transform.GetChild (num).GetChild (0).gameObject.SetActive (false);
				//Destroy(novaLista.transform.GetChild(novaLista.transform.childCount).gameObject);
				selecaoPersonagens.transform.GetChild (num).GetComponent<Image> ().color = new Color (1, 1, 1, 1);
				listaAmigos.Add(Amigo.amigoEscolhido);
				Amigo.amigoEscolhido = 0;
				Debug.Log(listaAmigos.Count);
				//Destroy(novaLista.gameObject);
			}
		}

	}
} 