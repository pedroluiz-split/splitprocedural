using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldController : MonoBehaviour {

	public GameObject [] personagens;
	public int diasPassados;
	public GameObject diasText;
	public GameObject fomeSlider;
	public GameObject fadigaSlider;
	public GameObject morteText;
	public GameObject fomeTimeline;
	public GameObject fadigaTimeline;
	public GameObject fomeAvisoFab;
	public GameObject fadigaAvisoFab;
	public GameObject amigos;
	public int spriteMaxDay = 0;
	public int spriteAtual = 0;
	public float avisoFomeLim = 0.5f;
	public float avisoFadigaLim = 0.5f;
	private GameObject viewportInicial;
	
	// Use this for initialization
	void Start ()
	{
		viewportInicial = fomeTimeline.transform.parent.gameObject;
		//coloca espaços no array de acordo com os filhos do personagem
		personagens = new GameObject[transform.childCount];

		//Coloca cada personagem no seu lugar
		for (int i = 0; i < transform.childCount; i++) {
			personagens [i] = transform.GetChild (i).gameObject;
			personagens [i].SetActive(false);


		}
		personagens [spriteAtual].SetActive(true);
		spriteMaxDay = int.Parse(transform.GetChild(1).name.Replace("day",""));

		fomeTimeline.transform.parent.GetChild (4).GetComponent<Text> ().text = "DIA 1\n\nComeçou o apocalipse.";
	}


	public void AdicionarDias ()
	{
		diasPassados += 1;
		diasText.GetComponent<Text> ().text = "DIA " + diasPassados;

		//Aumentar fome e fadiga
		fomeSlider.GetComponent<Slider> ().value = fomeSlider.GetComponent<Slider> ().value + 0.1f;
		fadigaSlider.GetComponent<Slider> ().value = fadigaSlider.GetComponent<Slider> ().value + 0.05f;


		if ((diasPassados > spriteMaxDay) && (spriteAtual < transform.childCount - 1)) {
			//mudar o sprite
			spriteAtual++;
			personagens [spriteAtual].SetActive (true);
			if (spriteAtual > 0)
				personagens [spriteAtual - 1].SetActive (false);
			//atualizar spritemaxday
			if (spriteAtual + 1 < transform.childCount)
				spriteMaxDay = int.Parse (personagens [spriteAtual + 1].name.Replace ("day", ""));
		}

		//Vê se o sprite correto está entre o dia de início de o dia de início do próximo
//		for (int i = 0; i < personagens.Length; i++) 
//		{
//			if ( diasPassados > spriteDay)
//			sprited 
//		}

		

		MostrarAvisoFomeFadiga ();

		bool[] roleta = RoletaAmigos ();

		if (roleta [1]) 
		{
			if (fomeTimeline.transform.parent.GetChild (4).GetComponent<Text> ().text.Contains ("DIA " + diasPassados))
				fomeTimeline.transform.parent.GetChild (4).GetComponent<Text> ().text = fomeTimeline.transform.parent.GetChild (4).GetComponent<Text> ().text.Replace ("DIA " + diasPassados, "DIA " + diasPassados + "\n\nPerdi um querido amigo...");
			else
				fomeTimeline.transform.parent.GetChild (4).GetComponent<Text> ().text = "DIA " + diasPassados + "\n\nPerdi um querido amigo...\n\n" + fomeTimeline.transform.parent.GetChild (4).GetComponent<Text> ().text;
		}
		if (roleta [0]) {
			if (fomeTimeline.transform.parent.GetChild (4).GetComponent<Text> ().text.Contains ("DIA " + diasPassados))
				fomeTimeline.transform.parent.GetChild (4).GetComponent<Text> ().text = fomeTimeline.transform.parent.GetChild (4).GetComponent<Text> ().text.Replace ("DIA " + diasPassados, "DIA " + diasPassados + "\n\nGanhei um amigo!");
			else
				fomeTimeline.transform.parent.GetChild (4).GetComponent<Text> ().text = "DIA " + diasPassados + "\n\nGanhei um amigo!\n\n" + fomeTimeline.transform.parent.GetChild (4).GetComponent<Text> ().text;
		}

		//Reseta caso fome ou fadiga estejam no maximo
		if (fomeSlider.GetComponent<Slider> ().value >= 1 || fadigaSlider.GetComponent<Slider> ().value >= 1) {
//			ResetarDias ();
//			CharacterController.controller.Gerar_Personagem();
			Morreu ();
		}
	}

	public bool[] RoletaAmigos ()
	{
		bool ganhou = false;
		bool perdeu = false;
		List<GameObject> listaAmigosAtivados = new List<GameObject> ();
		List<GameObject> listaAmigosDesativados = new List<GameObject> ();

		//Roda a chance de perder um amigo
		if (Random.Range (0, 10) > 7) {
			//Correr pelos filhos de "Amigos" (sem contar o 0) e ver qual está ativo e desativar
			for (int i = 1; i < amigos.transform.childCount; i++) {
				//Caso o filho esteja ativado, desativá-lo
				if (amigos.transform.GetChild (i).gameObject.activeSelf) {
					listaAmigosAtivados.Add (amigos.transform.GetChild (i).gameObject);

					//amigos.transform.GetChild (i).gameObject.SetActive (false);
					//i = amigos.transform.childCount;
				}
			}

			//Veja se a lista de amigos ativados tem algum, e escolhe um aleatoriamente e desativa
			if (listaAmigosAtivados.Count > 0) {
				listaAmigosAtivados [Random.Range (0, listaAmigosAtivados.Count)].SetActive (false);
				perdeu = true;

				//Reordenar posições dos amigos
				amigos.GetComponent<Predios> ().ReordenarPosicoes ();
			}

		}

		//Roda a chance de ganhar um amigo
		if (Random.Range (0, 10) > 7) {
			//Correr pelos filhos de "Amigos" (sem contar o 0) e ver qual está desativado e ativar
			for (int i = 1; i < amigos.transform.childCount; i++) {
				//Caso o filho esteja ativado, desativá-lo
				if (!amigos.transform.GetChild (i).gameObject.activeSelf) {
					listaAmigosDesativados.Add (amigos.transform.GetChild (i).gameObject);
//					ganhou = true;
//					amigos.transform.GetChild (i).gameObject.SetActive (true);
//					i = amigos.transform.childCount;
				}


			}

			if (listaAmigosDesativados.Count > 0) 
			{
				int amigoAleatorio = Random.Range(0, listaAmigosDesativados.Count);
				listaAmigosDesativados[amigoAleatorio].SetActive(true);
				listaAmigosDesativados[amigoAleatorio].GetComponent<Amigo>().EscolherTudo();
				ganhou = true;

				//Reordenar posições dos amigos
				amigos.GetComponent<Predios>().ReordenarPosicoes();
			}


		}

		return new bool[2]{ganhou,perdeu};
	}

	public void MostrarAvisoFomeFadiga ()
	{
		//Caso a fome esteja acima do limite, mostrar o aviso de fome
		if (fomeSlider.GetComponent<Slider> ().value >= avisoFomeLim && Random.Range(0,10) > 6) 
		{

			fomeTimeline.transform.parent.GetChild(4).GetComponent<Text>().text = "DIA "+diasPassados + "\n\nEstou com fome...\n\n"+fomeTimeline.transform.parent.GetChild(4).GetComponent<Text>().text;
		}

		//Caso a fadiga esteja acima do limite, mostrar o aviso de fadiga
		if (fadigaSlider.GetComponent<Slider> ().value >= avisoFadigaLim && Random.Range(0,10) > 6) 
		{
			if (fadigaTimeline.transform.parent.GetChild(4).GetComponent<Text>().text.Contains("DIA "+diasPassados))
			{
				if (fadigaTimeline.transform.parent.GetChild(4).GetComponent<Text>().text.Contains("fome"))
				{
					fadigaTimeline.transform.parent.GetChild(4).GetComponent<Text>().text = fomeTimeline.transform.parent.GetChild(4).GetComponent<Text>().text.Replace("DIA " + diasPassados + "\n\nEstou com fome","DIA "+diasPassados+"\n\nEstou quase morto de fome e cansaço") ;
				}
			}
			else
				fadigaTimeline.transform.parent.GetChild(4).GetComponent<Text>().text = "DIA "+diasPassados + "\n\nEstou cansado...\n\n"+fomeTimeline.transform.parent.GetChild(4).GetComponent<Text>().text;
		}
	}

	public void ResetarDias ()
	{
		fomeTimeline.SetActive (false);
		fadigaTimeline.SetActive (false);

		if (fadigaTimeline.transform.parent.childCount > 5) {
			StartCoroutine(ResetarTimeline());
		}

		//fomeTimeline.transform.parent.gameObject = viewportInicial;
		//Debug.Log(fadigaTimeline.transform.parent.GetChild(fadigaTimeline.transform.parent.childCount-2).transform.name);

		diasPassados = 1;
		diasText.GetComponent<Text> ().text = "DIA " + diasPassados;

		fomeTimeline.transform.parent.GetChild(4).GetComponent<Text>().text = "Dias "+diasPassados+"\n\nComeçou o apocalipse.";

		fomeSlider.GetComponent<Slider>().value = 0;
		fadigaSlider.GetComponent<Slider>().value = 0;

		//resetar o sprite
		personagens [spriteAtual].SetActive(false);
		spriteAtual = 0;
		spriteMaxDay = int.Parse(transform.GetChild(1).name.Replace("day",""));
		personagens [spriteAtual].SetActive(true);
	}

	public IEnumerator ResetarTimeline ()
	{
		Destroy(fadigaTimeline.transform.parent.GetChild(fadigaTimeline.transform.parent.childCount-2).gameObject);

		yield return new WaitForSeconds (0.01f);

		if (fadigaTimeline.transform.parent.childCount > 5) {
			StartCoroutine(ResetarTimeline());
		}
	}

	public void Morreu ()
	{
		morteText.SetActive (true);
		//morteText.GetComponent<Text>().text = 
		if (fomeSlider.GetComponent<Slider> ().value >= 1) 
		{
//			fomeTimeline.SetActive (true);
//			fomeTimeline.transform.GetChild(0).GetComponent<Text>().text = fomeTimeline.transform.GetChild(0).GetComponent<Text>().text.Replace("$",diasPassados.ToString());
			fomeTimeline.transform.parent.GetChild(4).GetComponent<Text>().text = "DIA "+diasPassados+ "\n\nSeu personagem morreu de fome.\n\n" +fomeTimeline.transform.parent.GetChild(4).GetComponent<Text>().text;
		} else if (fadigaSlider.GetComponent<Slider> ().value >= 1) 
		{
//			fadigaTimeline.SetActive (true);
//			fadigaTimeline.transform.GetChild(0).GetComponent<Text>().text = fadigaTimeline.transform.GetChild(0).GetComponent<Text>().text.Replace("$",diasPassados.ToString());
			fomeTimeline.transform.parent.GetChild(4).GetComponent<Text>().text = "DIA "+diasPassados+ "\n\nSeu personagem morreu de fadiga.\n\n" +fomeTimeline.transform.parent.GetChild(4).GetComponent<Text>().text;
		}
	}

	public void MostrarAviso (string tipoAviso)
	{
		if (tipoAviso.Contains("fome")) {
			
		}
		if (tipoAviso.Contains("fadiga")) {
			
		}
	}
}
