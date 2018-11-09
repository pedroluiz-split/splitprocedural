using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleApi : MonoBehaviour {

	string url;

	public float lat, lon;

	LocationInfo li;

	public int zoom = 14;
	public int mapWidth = 640;
	public int mapHeight = 640;

	public enum mapType {roadmap, satellite,hybrid,terrain}
	public mapType mapSelected;
	public int scale;
	public GameObject slider;
	public GameObject[] buttons;
	WWW www;

	IEnumerator Map ()
	{
		zoom = (int)slider.GetComponent<Slider>().value;
		url = "https://maps.googleapis.com/maps/api/staticmap?center=@,$&zoom=*&size=600x300&maptype=§&markers=color:blue%7Clabel:S%7C40.702147,-74.015794&markers=color:green%7Clabel:G%7C40.711614,-74.012318&markers=color:red%7Clabel:C%7C40.718217,-73.998284&key=AIzaSyBw-wgs974Sc_6Q7_iauT19yKglqklN35w".Replace("@",lat.ToString()).Replace("$",lon.ToString()).Replace("*",zoom.ToString()).Replace("§",mapSelected.ToString());
		www = new WWW(url);
		yield return www;
		GetComponent<RawImage>().texture = www.texture;

		GetComponent<RawImage>().SetNativeSize();

		yield return new WaitForSeconds(0.001f);
		StartCoroutine(Map());
		buttons[0].GetComponent<Button>().onClick.AddListener(delegate{OnClick("Cima");});
		buttons[1].GetComponent<Button>().onClick.AddListener(delegate{OnClick("Baixo");});
		buttons[2].GetComponent<Button>().onClick.AddListener(delegate{OnClick("Esquerda");});
		buttons[3].GetComponent<Button>().onClick.AddListener(delegate{OnClick("Direita");});
	}

	// Use this for initialization
	void Start () {
		
		StartCoroutine(Map());
		StartCoroutine(ShowData());
	}

	IEnumerator ShowData ()
	{
		yield return new WaitForSeconds (5f);
		if (www.isDone) {
			Debug.Log(www.text);
		}
		else
			StartCoroutine(ShowData());
		
	}

	public void OnClick (string name)
	{

		if (name.Contains ("Cima"))
			lat += 0.0001f;
		else if (name.Contains ("Baixo"))
			lat -= 0.0001f;
		else if (name.Contains ("Esquerda"))
			lon -= 0.0001f;
		else if (name.Contains ("Direita"))
			lon += 0.0001f;

		
	}

}
