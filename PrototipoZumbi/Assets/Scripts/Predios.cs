using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predios : MonoBehaviour {

	public GameObject [] predios;
	public Vector2 posicaoInicial;
	public int limLinha;

	// Use this for initialization
	void Start () {
		predios = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
		{
			predios[i] = transform.GetChild(i).gameObject;
		}
		posicaoInicial = transform.GetChild(0).transform.position;

		OrganizarPredios();
	}

	public void VoltarPredios ()
	{
		for (int i = 0; i < predios.Length; i++) 
		{
			predios[i].transform.position = posicaoInicial;
			predios[i].SetActive(false);
		}
	}

	public void ReordenarPosicoes ()
	{
		float nextPosX = posicaoInicial.x;
		float nextPosY = posicaoInicial.y;
		int predioAtivado = 0;

		for (int i = 0; i < predios.Length; i++) 
		{
			predios[i].transform.position = posicaoInicial;
		}

		//Ver quais predios estão ativados e reordená-los
		for (int i = 1; i < transform.childCount; i++) 
		{
			if (transform.GetChild (i).gameObject.activeSelf) 
			{
				predioAtivado ++;
				predios [i].transform.position = new Vector2 (nextPosX, nextPosY);
				//Seta o x e y da proxima posicao
				nextPosX += predios [i].GetComponent<SpriteRenderer> ().bounds.size.x;
				if (predioAtivado % limLinha == 0) 
				{
					nextPosY -= (predios [predios.Length-1].GetComponent<SpriteRenderer> ().bounds.size.y);
					nextPosX = posicaoInicial.x;
				}
			}
		}
		predios [0].transform.position = new Vector2 (nextPosX, nextPosY);
	}
	
	public void OrganizarPredios ()
	{
		VoltarPredios();
		
		float nextPosX = posicaoInicial.x;
		float nextPosY = posicaoInicial.y;

		List<int> listaDePredios = new List<int> ();
		float predioAtual = 0;

		int pularEm = limLinha;

		int rand = Random.Range (1, predios.Length);

//		while (rand == 0) {
//			rand = Random.Range (0, predios.Length);
//		}

		int qntAmigos = predios.Length-1-(Random.Range(0,predios.Length-1));

		//Fazer um for para ir preenchendo os espaços seguintes
		for (int i = 0; i < qntAmigos; i++) {
			//Criar um random aleatorio de predios que nao tenha sido escolhido
			//rand = Random.Range (0, predios.Length);
			//Enquanto o predio aleatorio estiver na lista de predios, ele escolhe outro
			while (listaDePredios.Contains (rand)) {

				rand = Random.Range (1, predios.Length);
				
			}

			//Adiciona esse predio escolhido na lista
			listaDePredios.Add (rand);

			//Setar proxima posicao X baseado na largura do sprite
			if (i != 0)
			{
				//nextPosX = predios [i - 1].transform.position.x + predios [i - 1].GetComponent<SpriteRenderer> ().bounds.size.x;
				nextPosX += predios [i - 1].GetComponent<SpriteRenderer> ().bounds.size.x;
				if (i % pularEm == 0)
				{
//					nextPosY = posicaoInicial.y - (predios [i - 1].GetComponent<SpriteRenderer> ().bounds.size.y);
					nextPosY -= (predios [i - 1].GetComponent<SpriteRenderer> ().bounds.size.y);
					nextPosX = posicaoInicial.x;
				}

				//nextPosX = posicaoInicial.x + (i * predios[i-1].GetComponent<SpriteRenderer>().bounds.size.x);
			}

			Debug.Log("rand: "+rand+ " i: "+ i);

			//Colocar o predio aleatorio na proxima posicao X
			predios[rand].transform.position = new Vector2 (nextPosX, nextPosY);
			//predios[rand].transform.position = new Vector2 (nextPosX, predios[rand].transform.position.y);
			predios[rand].SetActive(true);


		}

		nextPosX += predios [predios.Length-1].GetComponent<SpriteRenderer> ().bounds.size.x;
		if (qntAmigos % pularEm == 0) 
		{
//			nextPosY = posicaoInicial.y - (predios [i - 1].GetComponent<SpriteRenderer> ().bounds.size.y);
			nextPosY -= (predios [predios.Length-1].GetComponent<SpriteRenderer> ().bounds.size.y);
			nextPosX = posicaoInicial.x;
		}

		predios[0].transform.position = new Vector2 (nextPosX, nextPosY);
		predios[0].SetActive(true);


	}
}
