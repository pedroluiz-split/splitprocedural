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
	public GameObject amigos;

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
		if (efeito.Contains ("Karma")) {
			//karmaText.GetComponent<Text> ().text = "Karma: " + (float.Parse (karmaText.GetComponent<Text> ().text.Replace ("Karma: ", "")) + float.Parse (efeito.Split (new string[]{ "|" }, System.StringSplitOptions.None) [0].Replace ("Karma", ""))).ToString ();
			float efeitoKarma = 0;


			string[] efeitos = efeito.ToLower ().Split (new string[]{ "|" }, System.StringSplitOptions.None);
			for (int i = 0; i < efeitos.Length; i++) {
				if (efeitos [i].Contains ("karma")) {
					efeitoKarma = float.Parse(efeitos[i].Replace("karma",""));
				}
			}

			efeitoKarma = float.Parse (karmaText.GetComponent<Text> ().text.Replace ("Karma: ", "")) + efeitoKarma;

			karmaText.GetComponent<Text> ().text = "Karma: " + efeitoKarma;
		}

		if (efeito.Contains ("NovoAmigo")) {
			if (efeito.ToLower ().Contains ("novoamigo(mulher)")) {
				Amigos.amigos.CriarAmigo ("mulher");
			} else if (efeito.ToLower ().Contains ("novoamigo(homem)")) {
				Amigos.amigos.CriarAmigo ("homem");
			} else
				Amigos.amigos.CriarAmigo ();
		}

		if (efeito.Contains ("PerderAmigos")) {
			string argsEfeitos = efeito.ToLower ().Split (new string[]{ "perderamigos(" }, System.StringSplitOptions.None) [1].Split (new string[]{ ")" }, System.StringSplitOptions.None) [0];
			int qnt = int.Parse (argsEfeitos.Split (new string[]{ "," }, System.StringSplitOptions.None) [0]);
			string sexo = argsEfeitos.Split (new string[]{ "," }, System.StringSplitOptions.None) [1];

			Amigos.amigos.DestruirAmigos (qnt, sexo);
		}

		if (efeito.Contains ("GanharComida")) {
			string argsEfeitos = efeito.ToLower().Split (new string[]{ "ganharcomida(" }, System.StringSplitOptions.None) [1].Split (new string[]{ ")" }, System.StringSplitOptions.None) [0];
			int qnt = int.Parse (argsEfeitos);
			OldController.oldController.AdicionarComida (qnt);
		}

		if (efeito.Contains ("PerderComida")) {
			string argsEfeitos = efeito.ToLower().Split (new string[]{ "perdercomida(" }, System.StringSplitOptions.None) [1].Split (new string[]{ ")" }, System.StringSplitOptions.None) [0];
			int qnt = int.Parse (argsEfeitos);
			OldController.oldController.PerderComida (qnt);
		}

		if (efeito.Contains ("TrocarArma")) {
			string argsEfeitos = efeito.ToLower().Split (new string[]{ "trocararma(" }, System.StringSplitOptions.None) [1].Split (new string[]{ ")" }, System.StringSplitOptions.None) [0];
			OldController.oldController.TrocarArma(argsEfeitos.Substring(0,1).ToUpper() + argsEfeitos.Remove(0,1));
		}

		if (efeito.Contains ("AlterarHabilidadePlayer")) {
			string argsEfeitos = efeito.ToLower().Split (new string[]{ "alterarhabilidadeplayer(" }, System.StringSplitOptions.None) [1].Split (new string[]{ ")" }, System.StringSplitOptions.None) [0];
			//AlterarHabilidadePlayer(int.Parse(argsEfeitos.Split(new string[]{","}, System.StringSplitOptions.None)[0]), float.Parse(argsEfeitos.Split(new string[]{","}, System.StringSplitOptions.None)[1]));
			AlterarHabilidadePlayer(argsEfeitos.Split(new string[]{";"}, System.StringSplitOptions.None));
		}
	}

	public void AlterarHabilidadePlayer (int item, float value)
	{
		if (item == 0) {
			OldController.oldController.listaHabilidades[item] += value;
			OldController.oldController.listaHabilidades[4] -= value;
		} else if (item == 2) {
			OldController.oldController.listaHabilidades[item] += value;
			OldController.oldController.listaHabilidades[6] -= value;
		} else if (item == 4) {
			OldController.oldController.listaHabilidades[item] += value;
			OldController.oldController.listaHabilidades[0] -= value;
		} else if (item == 6) {
			OldController.oldController.listaHabilidades[item] += value;
			OldController.oldController.listaHabilidades[2] -= value;
		}
		else
			OldController.oldController.listaHabilidades[item] += value;

		//Limitar a no maximo 100 e minimo 0
		if (OldController.oldController.listaHabilidades[item] > 100)
			OldController.oldController.listaHabilidades[item] = 100;
		else if (OldController.oldController.listaHabilidades[item] < 0)
			OldController.oldController.listaHabilidades[item] = 0;
		OldController.oldController.AtualizarRadar();
	}

	public void AlterarHabilidadePlayer (string[] itens)
	{
		int[] itensIndices = new int[itens.Length];
		float[] values = new float[itens.Length];

		for (int i = 0; i < itens.Length; i++) {
			itensIndices [i] = int.Parse(itens [i].Split (new string[]{ "," }, System.StringSplitOptions.None) [0]);
			values [i] = float.Parse(itens [i].Split (new string[]{ "," }, System.StringSplitOptions.None) [1]);

			if (itensIndices [i] == 0) {
				if (OldController.oldController.listaHabilidades [itensIndices [i]] + values [i] < 100) {
					OldController.oldController.listaHabilidades [itensIndices [i]] += values [i];
					OldController.oldController.listaHabilidades [4] -= values [i];
				} else {
					OldController.oldController.listaHabilidades [itensIndices [i]] = 100;
					OldController.oldController.listaHabilidades [4] = 0;
				}

			} else if (itensIndices [i] == 2) {
				if (OldController.oldController.listaHabilidades [itensIndices [i]] + values [i] < 100) {
					OldController.oldController.listaHabilidades [itensIndices [i]] += values [i];
					OldController.oldController.listaHabilidades [6] -= values [i];
				} else {
					OldController.oldController.listaHabilidades [itensIndices [i]] = 100;
					OldController.oldController.listaHabilidades [6] = 0;
				}
			} else if (itensIndices [i] == 4) {
				if (OldController.oldController.listaHabilidades [itensIndices [i]] + values [i] < 100) {
					OldController.oldController.listaHabilidades [itensIndices [i]] += values [i];
					OldController.oldController.listaHabilidades [0] -= values [i];
				}else {
					OldController.oldController.listaHabilidades [itensIndices [i]] = 100;
					OldController.oldController.listaHabilidades [0] = 0;
				}
			} else if (itensIndices [i] == 6) {
				if (OldController.oldController.listaHabilidades [itensIndices [i]] + values [i] < 100) {
					OldController.oldController.listaHabilidades [itensIndices [i]] += values [i];
					OldController.oldController.listaHabilidades [2] -= values [i];
				}else {
					OldController.oldController.listaHabilidades [itensIndices [i]] = 100;
					OldController.oldController.listaHabilidades [2] = 0;
				}
			}
			else
				if (OldController.oldController.listaHabilidades[itensIndices [i]] + values[i] < 100)
					OldController.oldController.listaHabilidades[itensIndices [i]] += values[i];
				else
					OldController.oldController.listaHabilidades[itensIndices [i]] = 100;
		}


		OldController.oldController.AtualizarRadar();
	}

	public void LancarEvento ()
	{
		paiEvento.SetActive(true);
		amigos.SetActive(false);


		//Escolher uma linha aleatória do arquivo Eventos.txt
		int numAleat = Random.Range(1,textoEventos.Length);
		textoEventoAleatorio = textoEventos[numAleat];

		descricaoEvento = textoEventoAleatorio.Split(new string[]{ "\n" }, System.StringSplitOptions.None)[0];

		//Cria um array de strings para armazenar os textos de cada variavel
		textoBotoes = new string[textoEventoAleatorio.Split(new string[]{ "\n" }, System.StringSplitOptions.None).Length-1];
		outcomes = new string[textoEventoAleatorio.Split(new string[]{ "\n" }, System.StringSplitOptions.None).Length-1];
		efeitos = new string[textoEventoAleatorio.Split(new string[]{ "\n" }, System.StringSplitOptions.None).Length-1];

		//Preencher as variaveis
		for (int i = 1; i < textoBotoes.Length+1; i++) 
		{
			textoBotoes[i-1] = textoEventoAleatorio.Split(new string[]{ "\n" }, System.StringSplitOptions.None)[i].Split(new string[]{ "$" }, System.StringSplitOptions.None)[0];
			outcomes[i-1] = textoEventoAleatorio.Split(new string[]{ "\n" }, System.StringSplitOptions.None)[i].Split(new string[]{ "$" }, System.StringSplitOptions.None)[1];
			efeitos[i-1] = textoEventoAleatorio.Split(new string[]{ "\n" }, System.StringSplitOptions.None)[i].Split(new string[]{ "$" }, System.StringSplitOptions.None)[2];
		}

		//Rodar o método para organizar a tela e ativar tudo
		OrganizarTela();

		List<string> textoEvento = new List<string>();
		textoEvento.AddRange(textoEventos);
		textoEvento.Remove(textoEventos[numAleat]);
		textoEventos = textoEvento.ToArray();
	}

	public void AcessarArquivo ()
	{
		//Separar os eventos e colocar no array de eventos
		textoEventos = (Resources.Load ("Eventos") as TextAsset).text.Split (new string[]{ "==========================" }, System.StringSplitOptions.None);

		//Tirar espaços iniciais e finais
		for (int i = 1; i < textoEventos.Length; i++) {
			if (textoEventos [i] != "" && textoEventos [i] != null) {
				textoEventos[i] = textoEventos[i].Remove(0,2);
				textoEventos[i] = textoEventos[i].Remove(textoEventos[i].Length-2,2);
			}
			else
			{
				System.Array.Resize(ref textoEventos,textoEventos.Length-1);
			}
		}

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
