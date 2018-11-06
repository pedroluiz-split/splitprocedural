using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amigos : MonoBehaviour 
{
	public GameObject [] predios;
	public Vector2 posicaoInicial;
	public int limLinha;
	public GameObject amigoPrefab;
	public int limiteCriacaoAmigos;
	public static Amigos amigos;

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
		//Criar um prefab de um amigo novo
		GameObject amigoNovo = Instantiate (amigoPrefab, this.transform);
		amigoNovo.SetActive(true);

		//Pegar Imagem aleatoria de amigo na pasta Resources/Amigos e colocar no amigo
		object[] spriteList;
		spriteList = Resources.LoadAll ("Amigos", typeof(Sprite));
		Sprite spriteEscolhido = (Sprite)spriteList [Random.Range (0, spriteList.Length)];
		amigoNovo.GetComponent<SpriteRenderer> ().sprite = spriteEscolhido;

		//De acordo com o sexo da pessoa da imagem, escolhe os atributos apropriados
		if (spriteEscolhido.name.Contains ("H")) {
			amigoNovo.GetComponent<Amigo> ().personagemMasc = true;
			amigoNovo.GetComponent<Amigo> ().EscolherTudo ();
		} else 
		{
			amigoNovo.GetComponent<Amigo> ().personagemMasc = false;
			amigoNovo.GetComponent<Amigo> ().EscolherTudo ();
		}

		return amigoNovo;
	}

	public void DestruirAmigos ()
	{
		for (int i = 2; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
		transform.GetChild(0).position = posicaoInicial;
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
	
//	public void OrganizarPredios ()
//	{
//		VoltarPredios();
//		
//		float nextPosX = posicaoInicial.x;
//		float nextPosY = posicaoInicial.y;
//
//		List<int> listaDePredios = new List<int> ();
//		float predioAtual = 0;
//
//		int pularEm = limLinha;
//
//		int rand = Random.Range (1, predios.Length);
//
////		while (rand == 0) {
////			rand = Random.Range (0, predios.Length);
////		}
//
//		int qntAmigos = predios.Length-1-(Random.Range(0,predios.Length-1));
//
//		//Fazer um for para ir preenchendo os espaços seguintes
//		for (int i = 0; i < qntAmigos; i++) {
//			//Criar um random aleatorio de predios que nao tenha sido escolhido
//			//rand = Random.Range (0, predios.Length);
//			//Enquanto o predio aleatorio estiver na lista de predios, ele escolhe outro
//			while (listaDePredios.Contains (rand)) {
//
//				rand = Random.Range (1, predios.Length);
//				
//			}
//
//			//Adiciona esse predio escolhido na lista
//			listaDePredios.Add (rand);
//
//			//Setar proxima posicao X baseado na largura do sprite
//			if (i != 0)
//			{
//				//nextPosX = predios [i - 1].transform.position.x + predios [i - 1].GetComponent<SpriteRenderer> ().bounds.size.x;
//				nextPosX += predios [i - 1].GetComponent<SpriteRenderer> ().bounds.size.x;
//				if (i % pularEm == 0)
//				{
////					nextPosY = posicaoInicial.y - (predios [i - 1].GetComponent<SpriteRenderer> ().bounds.size.y);
//					nextPosY -= (predios [i - 1].GetComponent<SpriteRenderer> ().bounds.size.y);
//					nextPosX = posicaoInicial.x;
//				}
//
//				//nextPosX = posicaoInicial.x + (i * predios[i-1].GetComponent<SpriteRenderer>().bounds.size.x);
//			}
//
//			Debug.Log("rand: "+rand+ " i: "+ i);
//
//			//Colocar o predio aleatorio na proxima posicao X
//			predios[rand].transform.position = new Vector2 (nextPosX, nextPosY);
//			//predios[rand].transform.position = new Vector2 (nextPosX, predios[rand].transform.position.y);
//			predios[rand].SetActive(true);
//
//
//		}
//
//		nextPosX += predios [predios.Length-1].GetComponent<SpriteRenderer> ().bounds.size.x;
//		if (qntAmigos % pularEm == 0) 
//		{
////			nextPosY = posicaoInicial.y - (predios [i - 1].GetComponent<SpriteRenderer> ().bounds.size.y);
//			nextPosY -= (predios [predios.Length-1].GetComponent<SpriteRenderer> ().bounds.size.y);
//			nextPosX = posicaoInicial.x;
//		}
//
//		predios[0].transform.position = new Vector2 (nextPosX, nextPosY);
//		predios[0].SetActive(true);
//
//
//	}
}
