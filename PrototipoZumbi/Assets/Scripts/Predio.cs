using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Predio : MonoBehaviour {

	public bool estaClicado = false;
	public GameObject infoPredioObjeto;
	public int qntZumbis;
	public int qntComidas;
	public int qntSobreviventes;
	private bool esperandoClique = false;
	public GameObject group;
	private Text dialogo;
	public GameObject deadbook;
	public int qntComida;
	public static GameObject ultimoAtivo;
	public GameObject amigos;
	public float volumeObjeto;
	public bool jaEntrou = false;
	[Range(0,100)]
	public float chanceDeSucesso;
	public int combateTotal;
	public Predio predio;
	private string infos;

	void Awake ()
	{
		predio = this.GetComponent<Predio>();
		group = GameObject.Find("Group");
	}


	// Use this for initialization
	void Start () {
		
		predio = this;
		group.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0);
		//infoPredioObjeto = transform.parent.parent.transform.transform.GetChild(0).GetChild(0).gameObject;
		group = transform.parent.parent.parent.parent.gameObject;
		infoPredioObjeto.GetComponent<Text>().text = group.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text;
		group.transform.GetChild(0).localScale = new Vector3(1,1,1);
		volumeObjeto = GetComponent<MeshRenderer>().bounds.size.x*GetComponent<MeshRenderer>().bounds.size.y*GetComponent<MeshRenderer>().bounds.size.z*2000;
		Debug.Log(volumeObjeto+ "Predio"+transform.name);
		qntZumbis = Random.Range(1,(int)volumeObjeto);
		qntComida = Random.Range(1,(int)volumeObjeto/10);
		qntSobreviventes = Random.Range(1,(int)volumeObjeto/20);

		infos = AtualizarTextoChance();
		Debug.Log(infos);
		AtualizarChanceSucesso();
	}

	void OnMouseDown ()
	{
		//Material material = new Material(GetComponent<MeshRenderer>().material);
		infos = AtualizarTextoChance();
		AtualizarTextoChance();
		AtualizarChanceSucesso ();
		if (!jaEntrou) {
			if (esperandoClique) {
				//EntrarPredio ();
			} else if (estaClicado) 
			{
				estaClicado = false;

				//Desabilita o ultimo predio ativo
				if (ultimoAtivo != null) 
				{
					if (!ultimoAtivo.GetComponent<Predio> ().jaEntrou)
						ultimoAtivo.GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.white);

					ultimoAtivo.GetComponent<Predio> ().estaClicado = false;
				}
				GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.white);

				infoPredioObjeto.transform.parent.gameObject.SetActive (false);
				ListaPersonagens.listaPersonagens.AtivarColliderPredios ();
			} else 
			{
				if (ultimoAtivo != null) {
					if (!ultimoAtivo.GetComponent<Predio> ().jaEntrou)
						ultimoAtivo.GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.white);

					ultimoAtivo.GetComponent<Predio> ().estaClicado = false;
				}
				ultimoAtivo = this.gameObject;
				estaClicado = true;
				StartCoroutine (EsperarClique ());
				//GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white);
				//				material.SetColor("_MainTexture", Color.red);
				//				GetComponent<MeshRenderer>().material = material;

				//Fetch the Renderer from the GameObject
				//MeshRenderer rend = GetComponent<MeshRenderer>();
				//rend.material.shader = Shader.Find("_Color");
				GetComponent<MeshRenderer> ().material.SetColor ("_Color", new Color (1f, 0f, 0f));

				//Set the main Color of the Material to green
				//		        rend.material.shader = Shader.Find("_Color");
				//		        rend.material.SetColor("_Color", Color.red);
				//
				//		        //Find the Specular shader and change its Color to red
				//		        rend.material.shader = Shader.Find("Specular");
				//		        rend.material.SetColor("_SpecColor", Color.white);

				//GetComponent<MeshRenderer>().
				//GetComponent<MeshRenderer>().material.
				TrocarTextoDialogo ();
				infoPredioObjeto.transform.parent.gameObject.SetActive (true);
				ListaPersonagens.listaPersonagens.DesativarColliderPredios (this.name);
			}
		}else
		{

			if (estaClicado) 
			{
				estaClicado = false;
				infoPredioObjeto.transform.parent.gameObject.SetActive (false);
			} else {
				if (ultimoAtivo != null) 
				{
					if (!ultimoAtivo.GetComponent<Predio>().jaEntrou)
						ultimoAtivo.GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.white);
					ultimoAtivo.GetComponent<Predio> ().estaClicado = false;
				}
				ultimoAtivo = this.gameObject;
				estaClicado = true;
				infoPredioObjeto.GetComponent<Text>().text = "Prédio "+ transform.name + "\n\nPrédio Conquistado!";
				infoPredioObjeto.transform.parent.gameObject.SetActive (true);
			}

		}
    }

    public float AtualizarChanceSucesso ()
	{
//		
		combateTotal = 0;
		for (int i = 0; i < Amigos.amigos.transform.childCount - 2; i++) {
			if (Amigos.amigos.amigosGobj.Length  > i) {
				if (Amigos.amigos.amigosGobj [i].name.EndsWith ("$")) {
					combateTotal += (int)(Amigos.amigos.amigosGobj [i].GetComponent<Amigo> ().listaHabilidades [5]);
					Debug.Log ("Total Combate: " + combateTotal);
				}
			}
		}

		float chanceSucesso = combateTotal/qntZumbis;

		return chanceSucesso;
	}

	public float ChanceSucesso ()
	{
		float maxZumbis, minZumbis;
		minZumbis = (Amigos.amigos.transform.childCount-1)*5;
		maxZumbis = (Amigos.amigos.transform.childCount-1)*15;
		chanceDeSucesso = 100 - (((((float)qntZumbis) - minZumbis)/(maxZumbis - minZumbis))*100);
		chanceDeSucesso = chanceDeSucesso.ToString().Length>4?float.Parse(chanceDeSucesso.ToString().Remove(4)):chanceDeSucesso;
		if (chanceDeSucesso < 0)
			chanceDeSucesso = 0;
		else if (chanceDeSucesso > 100)
			chanceDeSucesso = 100;

		return chanceDeSucesso;
	}

    public void EntrarPredio ()
	{
		jaEntrou = true;
		ultimoAtivo = null;

		//Ativa o DeadBook
		deadbook.SetActive (true);

		//Coletar itens
		OldController.oldController.AdicionarComida (qntComida);

		//Passar dia
		OldController.oldController.AdicionarDias ();

		if (chanceDeSucesso < 100) {
			//OldController.oldController.PerderAmigo((int)((100-chanceDeSucesso)/100f*(Amigos.amigos.transform.childCount-2)));

		}
		List<GameObject> amigosMissao = new List<GameObject>();
		for (int i = 0; i < qntSobreviventes; i++) 
		{
			amigosMissao.Add(Amigos.amigos.CriarAmigo ());
		}

		OldController.oldController.AdicionarTextoTimeLine("Encontramos " + ((qntSobreviventes>1)?qntSobreviventes+" sobreviventes":"um sobrevivente") + " no local...");

		estaClicado = false;
		esperandoClique = false;
		GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
		AtualizarChanceSucesso();
		//Voltar para DeadBook
		Camera.main.orthographic = false;
		group.SetActive(false);

		Amigos.amigos.ReordenarPosicoes();

	}

	public string AtualizarTextoChance ()
	{
//		if (infos != null) {
//			int removerAleatorio = (int)Random.Range (0, chanceDeSucesso * combateTotal);
//		if (infos.Length > 6 || infos == null)
//			infos = (float.Parse(dialogo.text.Remove (dialogo.text.Length, 4)) * chanceDeSucesso).ToString ();
//		infos = AtualizarTextoChance ();
//		}
//		else
//			infos = "";
		
	
		return infos;
	}

	void TrocarTextoDialogo()
	{
		//Achar dialogo e trocar texto
		infoPredioObjeto.GetComponent<Text>().text = "Prédio "+transform.name + "\n\nZumbis: " + qntZumbis + "\nComida: "+qntComida + "\nSobreviventes: "+qntSobreviventes+"\nChance de Sucesso: "+chanceDeSucesso+"%";
	}

    IEnumerator EsperarClique ()
	{
		esperandoClique = true;
		yield return new WaitForSeconds(0.3f);
		esperandoClique = false;
	}


}
