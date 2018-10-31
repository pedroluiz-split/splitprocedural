using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorEventos : MonoBehaviour {

	public string descricaoEvento;

	public string [] textoBotoes;
	public string [] outcomes;
	public string [] efeitos;

	public string [] textoEventos;
	public string textoEventoAleatorio;
	public GameObject paiEvento;
	public GameObject [] botoes;

	[Range(0,100)]
	public float chanceDeRodarEvento;

	public static ControladorEventos controladorEventos;

	public GameObject karmaText;

	void Awake ()
	{
		if (controladorEventos == null) {
			controladorEventos = this;
		} else {
			Destroy(this);
		}
	}

	// Use this for initialization
	void Start ()
	{
		paiEvento = transform.GetChild (0).gameObject;

		paiEvento.SetActive(false);

		AcessarArquivo ();

		//SortearEvento ();

		botoes [0].GetComponent<Button> ().onClick.AddListener (delegate {BotaoOnClick (0);});
		botoes [1].GetComponent<Button> ().onClick.AddListener (delegate {BotaoOnClick (1);});
		botoes [2].GetComponent<Button> ().onClick.AddListener (delegate {BotaoOnClick (2);});
		botoes [3].GetComponent<Button> ().onClick.AddListener (delegate {BotaoOnClick (3);});
		
	}

	public void SortearEvento ()
	{
		if (Random.Range (0, 100) <= chanceDeRodarEvento) 
		{
			LancarEvento();
		}
	}

	public void BotaoOnClick (int botaoNum)
	{
		paiEvento.SetActive(false);
		//Debug.Log("Botao clicado numero: "+botaoNum);
		OldController.oldController.AdicionarTextoTimeLine(outcomes[botaoNum]);
		//Fazer o efeito do botão escolhido
		Fazer_Efeito(efeitos[botaoNum]);
	}

	public void Fazer_Efeito (string efeito)
	{
		if (efeito.Contains ("Karma")) 
		{
			karmaText.GetComponent<Text>().text = "Karma: "+(float.Parse(karmaText.GetComponent<Text>().text.Replace("Karma: ","")) + float.Parse(efeito.Split(new string[]{"|"},System.StringSplitOptions.None)[0].Replace("Karma","")) ).ToString();
		}
	}

	public void LancarEvento ()
	{
		paiEvento.SetActive(true);
		//Escolher uma linha aleatória do arquivo Eventos.txt
		textoEventoAleatorio = textoEventos[Random.Range(1,textoEventos.Length)];

		descricaoEvento = textoEventoAleatorio.Split(new string[]{ ";" }, System.StringSplitOptions.None)[0];

		//Pegar o que é cada coisa daquela linha e colocar na devida variavel
		textoBotoes = new string[textoEventoAleatorio.Split(new string[]{ ";" }, System.StringSplitOptions.None).Length-1];
		outcomes = new string[textoEventoAleatorio.Split(new string[]{ ";" }, System.StringSplitOptions.None).Length-1];
		efeitos = new string[textoEventoAleatorio.Split(new string[]{ ";" }, System.StringSplitOptions.None).Length-1];

		//Preencher as variaveis
		for (int i = 1; i < textoEventoAleatorio.Split(new string[]{ ";" }, System.StringSplitOptions.None).Length; i++) 
		{
			textoBotoes[i-1] = textoEventoAleatorio.Split(new string[]{ ";" }, System.StringSplitOptions.None)[i].Split(new string[]{ "," }, System.StringSplitOptions.None)[0];
			outcomes[i-1] = textoEventoAleatorio.Split(new string[]{ ";" }, System.StringSplitOptions.None)[i].Split(new string[]{ "," }, System.StringSplitOptions.None)[1];
			efeitos[i-1] = textoEventoAleatorio.Split(new string[]{ ";" }, System.StringSplitOptions.None)[i].Split(new string[]{ "," }, System.StringSplitOptions.None)[2];
		}

		//Rodar o método para organizar a tela e ativar tudo
		OrganizarTela();
	}

	public void AcessarArquivo ()
	{
		//Separar os eventos e colocar no array de eventos
		textoEventos = (Resources.Load ("Eventos") as TextAsset).text.Split (new string[]{ "\n" }, System.StringSplitOptions.None);

	}

	public void OrganizarTela ()
	{
		//Colocar o conteúdo das variáveis nos texts corretos
		for (int i = 0; i < textoBotoes.Length + 1; i++) 
		{
			if (i == 0) {
				paiEvento.transform.GetChild (0).GetComponent<Text> ().text = descricaoEvento;
			} else {
				paiEvento.transform.GetChild (i).GetChild (0).GetComponent<Text> ().text = textoBotoes [i - 1];
				paiEvento.transform.GetChild (i).gameObject.SetActive(true);
			}
		}

		//Sumir o restante dos botões não utilizados
		for (int i = 0; i < (paiEvento.transform.childCount-1) - textoBotoes.Length; i++) 
		{
			paiEvento.transform.GetChild((paiEvento.transform.childCount-1)-i).gameObject.SetActive(false);
		}
	}


}
