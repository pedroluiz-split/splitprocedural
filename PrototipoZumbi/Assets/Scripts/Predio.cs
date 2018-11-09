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
	private GameObject group;
	private Text dialogo;
	public GameObject deadbook;
	public int qntComida;
	private static GameObject ultimoAtivo;
	private float volumeObjeto;
	public bool jaEntrou = false;
	[Range(0,100)]
	public float chanceDeSucesso;

	// Use this for initialization
	void Start () {
		//infoPredioObjeto = transform.parent.parent.transform.transform.GetChild(0).GetChild(0).gameObject;
		group = transform.parent.parent.gameObject;
		infoPredioObjeto.GetComponent<Text>().text = group.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text;
		volumeObjeto = GetComponent<MeshRenderer>().bounds.size.x*GetComponent<MeshRenderer>().bounds.size.y*GetComponent<MeshRenderer>().bounds.size.z;
		qntZumbis = Random.Range(1,(int)volumeObjeto);
		qntComida = Random.Range(1,(int)volumeObjeto/10);
		qntSobreviventes = Random.Range(1,(int)volumeObjeto/20);
		AtualizarChanceSucesso();
	}

	void OnMouseDown ()
	{
		//Material material = new Material(GetComponent<MeshRenderer>().material);
		AtualizarChanceSucesso ();
		if (!jaEntrou) {
			if (esperandoClique) {
				EntrarPredio ();
			} else {
				if (estaClicado) {
					estaClicado = false;
//				//material.SetColor("_EmissionColor", Color.black);
//				material.SetColor("_MainTexture", Color.black);
//				GetComponent<MeshRenderer>().material = material;

					//Fetch the Renderer from the GameObject
					//MeshRenderer rend = GetComponent<MeshRenderer>();

					//Set the main Color of the Material to green
					//rend.material.shader = Shader.Find("_Color");
					if (ultimoAtivo != null) 
					{
						if (!ultimoAtivo.GetComponent<Predio>().jaEntrou)
							ultimoAtivo.GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.white);

						ultimoAtivo.GetComponent<Predio> ().estaClicado = false;
					}
					GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.white);

					infoPredioObjeto.transform.parent.gameObject.SetActive (false);
				} else {
					if (ultimoAtivo != null) {
						if (!ultimoAtivo.GetComponent<Predio>().jaEntrou)
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
				}
			}

		} else 
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

    public void AtualizarChanceSucesso ()
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
	}

    void EntrarPredio ()
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
			OldController.oldController.PerderAmigo((int)((100-chanceDeSucesso)/100f*(Amigos.amigos.transform.childCount-2)));
		}

		for (int i = 0; i < qntSobreviventes; i++) 
		{
			Amigos.amigos.CriarAmigo ();
		}

		OldController.oldController.AdicionarTextoTimeLine("Encontramos " + ((qntSobreviventes>1)?qntSobreviventes+" sobreviventes":"um sobrevivente") + " no local...");

		estaClicado = false;
		esperandoClique = false;
		GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
		AtualizarChanceSucesso();
		//Voltar para DeadBook
		Camera.main.orthographic = false;
		group.SetActive(false);

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
