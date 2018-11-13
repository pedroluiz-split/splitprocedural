using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Amigos : MonoBehaviour 
{
	public GameObject [] predios;
	public Vector2 posicaoInicial;
	public int limLinha;
	public GameObject amigoPrefab;
	public int limiteCriacaoAmigos;
	public static Amigos amigos;
	public GameObject texto;
	public GameObject graficoRadar;

	void Awake ()
	{
		amigos = this;
	}

	// Use this for initialization
	void Start ()
	{
		predios = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
		{
			predios[i] = transform.GetChild(i).gameObject;
		}
		posicaoInicial = transform.GetChild(0).transform.position;

		//OrganizarPredios();
		CriarAmigos();

		//StartCoroutine(AtualizarLista());
	}


	public IEnumerator AtualizarLista ()
	{
		StartCoroutine(ReordenarPosicoes());
		yield return new WaitForSeconds(0.05f);

		StartCoroutine(AtualizarLista());
	}

	public void MandarAmigoEmbora ()
	{
		transform.GetChild(Amigo.amigoEscolhido).GetComponent<Amigo>().MandarEmbora();
		StartCoroutine(ReordenarPosicoes());
	}

	public void AtualizarRadar (float [] habilidades)
	{
		graficoRadar.GetComponent<RadarGraph>().habilidades = habilidades;
		graficoRadar.GetComponent<RadarGraph>().DebugDrawPolygon(graficoRadar.GetComponent<RadarGraph>().posicaoInicial,graficoRadar.GetComponent<RadarGraph>().raio,graficoRadar.GetComponent<RadarGraph>().qntItens);
	}

	void Update ()
	{
		AtualizarTexto();
		StartCoroutine(ReordenarPosicoes());
	}

	public void AtualizarTexto ()
	{
		if (texto != null) 
		{
			texto.GetComponent<Text>().text = "AMIGOS "+(transform.childCount-2).ToString()+"/"+limiteCriacaoAmigos;
		}
	}


	//Instanciar numero aleatorio de no max limiteCriacaoAmigos de amigos
	public void CriarAmigos ()
	{
		DestruirAmigos();

		int numeroAleatorioAmigos = Random.Range (0, limiteCriacaoAmigos);

		//Cria um numero x de amigos entre 0 e limiteCriacaoAmigos
		for (int i = 0; i < numeroAleatorioAmigos; i++) 
		{
			CriarAmigo();
		} 

		//yield return new WaitForSeconds(0.1f);

		//Organizar as posições da lista de amigos
		StartCoroutine(ReordenarPosicoes());
	}

	//Cria um novo amigo com atributos (inclusive imagem) aleatorios
	public GameObject CriarAmigo ()
	{
		
		if (transform.childCount - 2 < limiteCriacaoAmigos) {
			//Criar um prefab de um amigo novo
			GameObject amigoNovo = Instantiate (amigoPrefab, this.transform);
			amigoNovo.SetActive (true);

			//Pegar Imagem aleatoria de amigo na pasta Resources/Amigos e colocar no amigo
			object[] spriteList;
			spriteList = Resources.LoadAll ("Amigos", typeof(Sprite));
			Sprite spriteEscolhido = (Sprite)spriteList [Random.Range (0, spriteList.Length)];
			amigoNovo.GetComponent<SpriteRenderer> ().sprite = spriteEscolhido;

			//De acordo com o sexo da pessoa da imagem, escolhe os atributos apropriados
			if (spriteEscolhido.name.Contains ("H")) {
				amigoNovo.GetComponent<Amigo> ().personagemMasc = true;
				amigoNovo.GetComponent<Amigo> ().EscolherTudo ();
			} else {
				amigoNovo.GetComponent<Amigo> ().personagemMasc = false;
				amigoNovo.GetComponent<Amigo> ().EscolherTudo ();
			}

			return amigoNovo;
		}

		StartCoroutine(ReordenarPosicoes());

		return null;
	}

	//Cria um novo amigo de determinado sexo
	public GameObject CriarAmigo (string sexo)
	{
		GameObject amigoNovo = null;
		if (transform.childCount - 2 < limiteCriacaoAmigos) 
		{
			if (sexo.ToLower () == "masculino" || sexo.ToLower () == "homem") {
				//Criar um prefab de um amigo novo
				amigoNovo = Instantiate (amigoPrefab, this.transform);
				amigoNovo.SetActive (true);

				//Pegar Imagem aleatoria de amigo na pasta Resources/Amigos e colocar no amigo
				object[] spriteList;
				spriteList = Resources.LoadAll ("Amigos", typeof(Sprite));
				Sprite spriteEscolhido;
				spriteEscolhido = (Sprite)spriteList [Random.Range (0, spriteList.Length)];
				amigoNovo.GetComponent<SpriteRenderer> ().sprite = spriteEscolhido;
				while (!spriteEscolhido.name.Contains ("H")) {
					spriteEscolhido = (Sprite)spriteList [Random.Range (0, spriteList.Length)];
					amigoNovo.GetComponent<SpriteRenderer> ().sprite = spriteEscolhido;
				}
				//De acordo com o sexo da pessoa da imagem, escolhe os atributos apropriados
				amigoNovo.GetComponent<Amigo> ().personagemMasc = true;
				amigoNovo.GetComponent<Amigo> ().EscolherTudo ();
			} else if (sexo.ToLower () == "feminino" || sexo.ToLower () == "mulher") {
				//Criar um prefab de um amigo novo
				amigoNovo = Instantiate (amigoPrefab, this.transform);
				amigoNovo.SetActive (true);

				//Pegar Imagem aleatoria de amigo na pasta Resources/Amigos e colocar no amigo
				object[] spriteList;
				spriteList = Resources.LoadAll ("Amigos", typeof(Sprite));
				Sprite spriteEscolhido;
				spriteEscolhido = (Sprite)spriteList [Random.Range (0, spriteList.Length)];
				amigoNovo.GetComponent<SpriteRenderer> ().sprite = spriteEscolhido;
				while (!spriteEscolhido.name.Contains ("M")) {
					spriteEscolhido = (Sprite)spriteList [Random.Range (0, spriteList.Length)];
					amigoNovo.GetComponent<SpriteRenderer> ().sprite = spriteEscolhido;
				}
				//De acordo com o sexo da pessoa da imagem, escolhe os atributos apropriados
				amigoNovo.GetComponent<Amigo> ().personagemMasc = false;
				amigoNovo.GetComponent<Amigo> ().EscolherTudo ();
			} else {
				//Criar um prefab de um amigo novo
				amigoNovo = Instantiate (amigoPrefab, this.transform);
				amigoNovo.SetActive (true);

				//Pegar Imagem aleatoria de amigo na pasta Resources/Amigos e colocar no amigo
				object[] spriteList;
				spriteList = Resources.LoadAll ("Amigos", typeof(Sprite));
				Sprite spriteEscolhido = (Sprite)spriteList [Random.Range (0, spriteList.Length)];
				amigoNovo.GetComponent<SpriteRenderer> ().sprite = spriteEscolhido;

				//De acordo com o sexo da pessoa da imagem, escolhe os atributos apropriados
				if (spriteEscolhido.name.Contains ("H")) {
					amigoNovo.GetComponent<Amigo> ().personagemMasc = true;
					amigoNovo.GetComponent<Amigo> ().EscolherTudo ();
				} else {
					amigoNovo.GetComponent<Amigo> ().personagemMasc = false;
					amigoNovo.GetComponent<Amigo> ().EscolherTudo ();
				}

			}
		}

		StartCoroutine(ReordenarPosicoes());

		if (amigoNovo != null)
			return amigoNovo;
		else
			return null;
	}

	public void DestruirAmigos ()
	{
		for (int i = 2; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
		transform.GetChild(0).position = posicaoInicial;
	}

	public void DestruirAmigos (int qntAmigos, string sexo)
	{
		if (qntAmigos == 999) {
			for (int i = 2; i < transform.childCount; i++) {
				if (sexo == "masculino" || sexo == "homem") {
					if (transform.GetChild (i).GetComponent<Amigo> ().personagemMasc)
						Destroy (transform.GetChild (i).gameObject);
				} else if (sexo == "feminino" || sexo == "mulher") {
					if (!transform.GetChild (i).GetComponent<Amigo> ().personagemMasc)
						Destroy (transform.GetChild (i).gameObject);
				} else {
					Destroy (transform.GetChild (i).gameObject);
				}
			}
		} else {
			List<Transform> amigos = new List<Transform> ();

			if (sexo == "feminino" || sexo == "mulher") {
				//Adiciona todos os amigos em uma lista
				for (int i = 2; i < transform.childCount; i++) {
					amigos.Add (transform.GetChild (i));
				}

				amigos.RemoveAll (VerificarHomem);

				//Vê se a quantidade de amigos que tem que ser excluida não é maior que a quantidade de amigos existentes
				if (qntAmigos <= amigos.Count) {
					int amigoAleatorio;

					//Exclui a quantidade de amigos pedida
					for (int i = 0; i < qntAmigos; i++) {
						amigoAleatorio = Random.Range (0, amigos.Count);
						Destroy (transform.GetChild (amigos[amigoAleatorio].GetSiblingIndex()).gameObject);
						amigos.RemoveAt (amigoAleatorio);
					}
				}
			} else if (sexo == "masculino" || sexo == "homem") 
			{
				//Adiciona todos os amigos em uma lista
				for (int i = 2; i < transform.childCount; i++) {
					amigos.Add (transform.GetChild (i));
				}

				amigos.RemoveAll (VerificarMulher);

				//Vê se a quantidade de amigos que tem que ser excluida não é maior que a quantidade de amigos existentes
				if (qntAmigos <= amigos.Count) {
					int amigoAleatorio;

					//Exclui a quantidade de amigos pedida
					for (int i = 0; i < qntAmigos; i++) {
						amigoAleatorio = Random.Range (0, amigos.Count);
						Destroy (transform.GetChild (amigos[amigoAleatorio].GetSiblingIndex()).gameObject);
						amigos.RemoveAt (amigoAleatorio);
					}
				}
			} else 
			{
				//Adiciona todos os amigos em uma lista
				for (int i = 2; i < transform.childCount; i++) {
					amigos.Add (transform.GetChild (i));
				}

				if (qntAmigos <= amigos.Count) {
					int amigoAleatorio;

					//Exclui a quantidade de amigos pedida
					for (int i = 0; i < qntAmigos; i++) {
						amigoAleatorio = Random.Range (0, amigos.Count);
						Destroy (transform.GetChild (amigos[amigoAleatorio].GetSiblingIndex()).gameObject);
						amigos.RemoveAt (amigoAleatorio);
					}
				}
			}

		}

		StartCoroutine(ReordenarPosicoes());
	}

	public static bool VerificarHomem (Transform t)
	{
		return t.GetComponent<Amigo>().personagemMasc;
	}

	public static bool VerificarMulher (Transform t)
	{
		return !t.GetComponent<Amigo>().personagemMasc;
	}


	public void ResetarAmigos ()
	{
		for (int i = 1; i < transform.childCount; i++) 
		{
			transform.GetChild(i).GetComponent<Amigo>().EscolherTudo();
		}
	}

	public void VoltarPredios ()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).transform.position = posicaoInicial;
			transform.GetChild(i).gameObject.SetActive(false);
		}
	}

	public IEnumerator ReordenarPosicoes ()
	{
		yield return new WaitForSeconds(0.01f);
		
		float nextPosX = posicaoInicial.x;
		float nextPosY = posicaoInicial.y;
		int predioAtivado = 0;

		for (int i = 0; i < transform.childCount; i++) 
		{
			transform.GetChild(i).transform.position = posicaoInicial;
		}

		//Ver quais predios estão ativados e reordená-los
		for (int i = 2; i < transform.childCount; i++) 
		{
			if (transform.GetChild (i).gameObject.activeSelf) 
			{
				predioAtivado ++;
				transform.GetChild(i).transform.position = new Vector2 (nextPosX, nextPosY);

				//Seta o x e y da proxima posicao
				nextPosX += transform.GetChild(i).GetComponent<SpriteRenderer> ().bounds.size.x;

				if (predioAtivado % limLinha == 0)
				{
					nextPosY -= (transform.GetChild(transform.childCount-1).GetComponent<SpriteRenderer> ().bounds.size.y);
					nextPosX = posicaoInicial.x;
				}
			}
		}

		transform.GetChild(0).transform.position = new Vector2 (nextPosX, nextPosY);
	}

}
