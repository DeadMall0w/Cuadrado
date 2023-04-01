using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cellules : MonoBehaviour
{

public SpriteRenderer SR;
public int couleur;
public Vector3 Position;
// black = 0, white = 1, magenta = 2, green = 3, red = 4

    // Start is called before the first frame update
    void Start()
    {
        
    }

	public void change_couleur(int la_couleur)
	{
		couleur = la_couleur;
		if (couleur == 0)
		{
			SR.color = Color.black;
		}
		else if (couleur == 1)
		{
			SR.color = Color.white;
		}
		else if (couleur == 2)
		{
			SR.color = Color.magenta;
		}
		else if (couleur == 3)
		{
			SR.color = Color.green;
		}
		else
		{
			SR.color = Color.red;
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}