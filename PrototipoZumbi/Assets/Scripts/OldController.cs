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
	public int spriteMaxDay = 0;
	public int spriteAtual = 0;
	
	// Use this for initialization
	void Start ()
	{
		//coloca espaços no array de acordo com os filhos do personagem
		personagens = new GameObject[transform.childCount];

		//Coloca cada personagem no seu lugar
		for (int i = 0; i < transform.childCount; i++) {
			personagens [i] = transform.GetChild (i).gameObject;
			personagens [i].SetActive(false);


		}
		personagens [spriteAtual].SetActive(true);
		spriteMaxDay = int.Parse(transform.GetChild(1).name.Replace("day",""));
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

		//Reseta caso fome ou fadiga estejam no maximo
		if (fomeSlider.GetComponent<Slider> ().value >= 1 || fadigaSlider.GetComponent<Slider> ().value >= 1) {
//			ResetarDias ();
//			CharacterController.controller.Gerar_Personagem();
			Morreu();
		}
	}

	public void ResetarDias ()
	{
		fomeTimeline.SetActive(false);
		fadigaTimeline.SetActive(false);

		diasPassados = 1;
		diasText.GetComponent<Text> ().text = "DIA " + diasPassados;

		fomeSlider.GetComponent<Slider>().value = 0;
		fadigaSlider.GetComponent<Slider>().value = 0;

		//resetar o sprite
		personagens [spriteAtual].SetActive(false);
		spriteAtual = 0;
		spriteMaxDay = int.Parse(transform.GetChild(1).name.Replace("day",""));
		personagens [spriteAtual].SetActive(true);
	}

	public void Morreu ()
	{
		morteText.SetActive (true);
		//morteText.GetComponent<Text>().text = 
		if (fomeSlider.GetComponent<Slider> ().value >= 1) 
		{
			fomeTimeline.SetActive (true);
			fomeTimeline.transform.GetChild(0).GetComponent<Text>().text = fomeTimeline.transform.GetChild(0).GetComponent<Text>().text.Replace("$",diasPassados.ToString());
		} else if (fadigaSlider.GetComponent<Slider> ().value >= 1) 
		{
			fadigaTimeline.SetActive (true);
			fadigaTimeline.transform.GetChild(0).GetComponent<Text>().text = fadigaTimeline.transform.GetChild(0).GetComponent<Text>().text.Replace("$",diasPassados.ToString());
		}
	}
}
