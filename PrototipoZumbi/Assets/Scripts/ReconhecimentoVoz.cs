using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class ReconhecimentoVoz : MonoBehaviour {
	
	[SerializeField]
	private string [] keywords = new string[]{"bla", "blei", "blou"};

	private DictationRecognizer dict;
	public ConfidenceLevel confidence = ConfidenceLevel.Medium;
	public GameObject textGobj;
	//protected PhraseRecognizer recognizer;
	public float speed = 1;
	public Image target;
	public string word;
	private KeywordRecognizer recognizer;
	public int palavraAtual = 0;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (ReconhecerFala (5f));
		dict = new DictationRecognizer ();
		StartCoroutine (ReconhecerFala (5f));
		recognizer = new KeywordRecognizer(keywords);

//		if (words != null) {
//			recognizer = new KeywordRecognizer(words, confidence);
//			recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
//			StartCoroutine (ReconhecerFala (10));
//			recognizer.Start();
//		}
	}

	void Update ()
	{ 
//		var x = target.transform.position.x;
//		var y = target.transform.position.y;
//
//		switch (palavraAtual) {
//			case 0:
//			textGobj.GetComponent<Text>().text = words[palavraAtual];
//				break;
//			case 1:
//			textGobj.GetComponent<Text>().text = words[palavraAtual];
//				break;
//		}
		


//		target.transform.position = new Vector3(x, y, 0);
	}

	public IEnumerator ReconhecerFala (float duracao)
	{
		if (recognizer != null) {
			recognizer.Start ();
			textGobj.GetComponent<Text> ().text = recognizer.ToString ();
			yield return new WaitForSeconds (duracao);
			recognizer.Stop ();
			yield return new WaitForSeconds (duracao / 2);
		}
		StartCoroutine (ReconhecerFala (5f));

//		dict.DictationResult =>
//		{
//			Debug.Log(text);
//		};
	}

	private void Recognizer_OnPhraseRecognized (PhraseRecognizedEventArgs args)
	{
		word  = args.text;
		Debug.Log(word);
	}



}
