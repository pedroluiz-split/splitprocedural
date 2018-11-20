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
	public List<string> listaAmigos;

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
		novaLista = Instantiate(personagens,posLista,transform.rotation);
		novaLista.GetComponent<Amigos>().limLinha = 6;
		RetirarAmigosUsados();
		StartCoroutine(novaLista.GetComponent<Amigos>().ReordenarPosicoes());
		novaLista.GetComponent<Amigos>().enabled = false;
		novaLista.transform.localScale = novaLista.transform.localScale*scaleLista;
		StartCoroutine(TrocarPos(posLista));
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

	public IEnumerator TrocarPos (Vector3 pos)
	{
		yield return new WaitForSeconds(0.01f);
		novaLista.transform.Translate(posLista);
	}

	public void TrocarImagemBotao (int num)
	{
		//listaAmigos.Add(selecaoPersonagens.transform.GetChild(botao).GetChild(0).name);
		listaAmigos.Add(novaLista.transform.GetChild(num).name);
		selecaoPersonagens.transform.GetChild(botao).GetComponent<Image>().sprite = novaLista.transform.GetChild(num).GetComponent<SpriteRenderer>().sprite;
		selecaoPersonagens.transform.GetChild(botao).GetChild(0).gameObject.SetActive(false);
		selecaoPersonagens.transform.GetChild(botao).GetComponent<Image>().color = new Color(1,1,1,1);
	}
}
