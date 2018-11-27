using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
 
public class RadarGraph : MonoBehaviour {
 
 
	public float width;
	public float height;
	public float raio;
	public int qntItens;
	public float [] habilidades;
	public Vector2 posicaoInicial;

	void Start ()
	{  
		posicaoInicial = transform.position;
		//CriarGrafico();
		Esperar(0.01f);
		//Teste();
		//mesh.SetColors(new List<Color>(4){Color.blue,Color.blue,Color.blue,Color.blue});
	}


	IEnumerator Esperar (float segundos)
	{
		yield return new WaitForSeconds(segundos);
		DebugDrawPolygon(posicaoInicial, raio, qntItens);
	}

	public void AjustarNormaisEUVs (Mesh mesh)
	{
		Vector3 [] vertices = mesh.vertices;
		Vector3[] normals = new Vector3[vertices.Length];

		for (int i = 0; i < normals.Length; i++) {
			normals[i] = -Vector3.forward;
		}
	    
	    mesh.normals = normals;
	    
		Vector2[] uv = new Vector2[vertices.Length];

		var uvs = new Vector2[mesh.vertices.Length];
		 for (var i=0; i<uvs.Length; i++) {
			uvs[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].z);
		 }
		mesh.uv = uvs;
	}



	public void DebugDrawPolygon (Vector2 center, float radius, int numSides)
	{
		MeshFilter mf = GetComponent<MeshFilter> ();
		Mesh mesh = new Mesh ();
		mf.mesh = mesh;

		Vector3[] vertices = new Vector3[numSides + 1];
		List<int> listaTriangulos = new List<int> ();
		int[] triangulos = new int[numSides * 3];

		float raioAtual = habilidades[0];
		// The corner that is used to start the polygon (parallel to the X axis).
		Vector2 startCorner = new Vector2 (raioAtual/100, 0) + center;

		// The "previous" corner point, initialised to the starting corner.
		Vector2 previousCorner = startCorner;

		vertices [0] = center;
		vertices [1] = previousCorner;


		// For each corner after the starting corner...
		for (int i = 1; i < numSides; i++) {
			raioAtual = habilidades[i];
			// Calculate the angle of the corner in radians.
			float cornerAngle = 2f * Mathf.PI / (float)numSides * i;

			// Get the X and Y coordinates of the corner point.
			//Vector2 currentCorner = new Vector2 (Mathf.Cos (cornerAngle) * radius, Mathf.Sin (cornerAngle) * radius) + center;
			Vector2 currentCorner = new Vector2 (Mathf.Cos (cornerAngle) * raioAtual/100, Mathf.Sin (cornerAngle) * raioAtual/100) + center;

			// Draw a side of the polygon by connecting the current corner to the previous one.
			Debug.DrawLine (currentCorner, previousCorner);

			vertices [i + 1] = currentCorner;

			// Having used the current corner, it now becomes the previous corner.
			previousCorner = currentCorner;


		}

		Debug.Log("Vertice 1: "+vertices[1]+" Vertice 2: "+vertices[2]);

		mesh.vertices = vertices;

		for (int i = 0; i < (numSides)*3; i+=3) 
		{
			triangulos[i] = 0;
			if ((i/3)+2 > 8)
				triangulos[i+1] = 1;
			else
				triangulos[i+1] = (i/3)+2;
			triangulos[i+2] =(i/3)+1;
		}

		Debug.Log(triangulos[triangulos.Length-1]);

        mesh.triangles = triangulos;

        // Draw the final side by connecting the last corner to the starting corner.
        Debug.DrawLine(startCorner, previousCorner);

        AjustarNormaisEUVs(mesh);

        mesh.RecalculateNormals();
    }
}