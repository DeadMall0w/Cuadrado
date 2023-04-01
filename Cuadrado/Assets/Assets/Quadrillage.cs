using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quadrillage : MonoBehaviour
{
//7B5B5B
//Faire l'IA

public Cellules[,] Grille;
public int cote;
public GameObject Prefab;
public int Taille;
public float decalage_cote;
public Camera Cam;
public int A_deja_clicke;
public int x,y;
public TMP_Text Texte_score;
public int Score;
public TMP_Text Score_final;
public TMP_Text Carre_txt;
public TMP_Text Pourcentage_txt;
public int Pourcentage;
public string Seed;
public bool A_perdu;
public GameObject Menu_defaite;
public Animator Animation;
public bool Seed_random;
public GameObject Menu_parametres;
public GameObject Home;
public GameObject Rejouer;
public GameObject Param;
public bool isPlaying;
 public Camera Cam2;
 public GameObject Rejouer_Alea;
 public GameObject Couleur_Fond;
 public Transform Grille_Parent;

    public void Start ()
	{
		Home.SetActive(false);
		//Couleur_Fond.SetActive(false);
		Rejouer_Alea.SetActive(false);
		Rejouer.SetActive(false);
		Menu_defaite.SetActive(false);
		cote = 20;
		Carre_txt.text = cote.ToString() + " x " + cote.ToString();
		Pourcentage = 50;
		Pourcentage_txt.text = Pourcentage.ToString() + "%";
		Texte_score.gameObject.SetActive(false);
	}
    public void Commencer()
    {
			Texte_score.gameObject.SetActive(true);
	Param.SetActive(true);
	//Couleur_Fond.SetActive(true);
	Grille_Parent.gameObject.SetActive(true);
	if(Seed_random == true)
	{
		Seed = Random.Range(-9999,9999).ToString();
	}
	Rejouer.SetActive(true);
	Home.SetActive(true);
	Rejouer_Alea.SetActive(true);
	Score = 0;
	A_deja_clicke = 0;
	decalage_cote = cote - 1;
	Cam.orthographicSize = cote;
	Cam2.orthographicSize = cote;
	//Cam.orthographicSize = cote+4+(cote/11);
	//Cam.transform.position = new Vector3(6.14f + ((49f-14f)/40 * cote), -3.2f - (cote/12f), -10);
        Grille = new Cellules[cote,cote];
	for(int cote_x = 0;cote_x < cote;cote_x++)
	{

		for(int cote_y = 0;cote_y < cote;cote_y++)
		{
			Vector3 Coordonnees = new Vector3(cote_x*Taille - decalage_cote,cote_y*Taille - decalage_cote,0);
			GameObject Objet = Instantiate(Prefab,Coordonnees,Quaternion.identity);
			Cellules cellule = Objet.GetComponent<Cellules>();
			cellule.Position = new Vector3(cote_x,cote_y);
			Grille[cote_x,cote_y] = cellule;
			Objet.transform.parent = Grille_Parent;
		}
	}
	System.Random Black_n_white = new System.Random(Seed.GetHashCode());
	for(int cote_x = 0;cote_x < cote;cote_x++)
	{

		for(int cote_y = 0;cote_y < cote;cote_y++)
		{
			if(Black_n_white.Next(0,100) < Pourcentage)
			{
				Grille[cote_x,cote_y].change_couleur(1);
			}
		}
	}

    }

    // Update is called once per frame
    void Update()
    {
	if(A_perdu == true)
	{
		return;
	}
        if(Input.GetMouseButtonDown(0))
	{
		Vector3 Psouris = Cam2.ScreenToWorldPoint(Input.mousePosition);
print(Psouris);
		Collider2D[] Collisions = Physics2D.OverlapCircleAll(Psouris,0.1f);
		if(Collisions.Length > 0)
		{
			Cellules Click = Collisions[0].GetComponent<Cellules>();
			Vector3 Positions = Click.Position;
			if(Click.couleur == 0 && A_deja_clicke == 0)
			{
				Grille[Mathf.RoundToInt(Positions.x),Mathf.RoundToInt(Positions.y)].change_couleur(2);
				A_deja_clicke++;
				x = Mathf.RoundToInt(Positions.x);
				y = Mathf.RoundToInt(Positions.y);
				Score = 1;
				Texte_score.text = "1";
				isPlaying = true;
				Defaite();
			}
		}
	}
	if(Input.GetKeyDown(KeyCode.LeftArrow))
	{
		x--;
		if (x < 0)
		{
			x++;
			return;
		}
		Cellules Sel = Grille[x,y];
		if(Sel.couleur == 0)
		{
			if(Grille[x + 1,y].couleur != 2)
			{
				Grille[x + 1,y].change_couleur(3);
			}
				Grille[x,y].change_couleur(4);
				Score++;
				Texte_score.text = Score.ToString();
				Defaite();
		}
		else
		{
			x++;
		}
	}
	else if(Input.GetKeyDown(KeyCode.RightArrow))
	{
		x++;
		if (x > cote-1)
		{
			x--;
			return;
		}
		Cellules Sel = Grille[x,y];
		if(Sel.couleur == 0)
		{
			if(Grille[x - 1,y].couleur != 2)
			{
				Grille[x - 1,y].change_couleur(3);
			}
			Grille[x,y].change_couleur(4);
			Score++;
			Texte_score.text = Score.ToString();
			Defaite();
		}
		else
		{
			x--;
		}
	}
	else if(Input.GetKeyDown(KeyCode.DownArrow))
	{
		y--;
		if (y < 0)
		{
			y++;
			return;
		}
		Cellules Sel = Grille[x,y];
		if(Sel.couleur == 0)
		{
			if(Grille[x,y + 1].couleur != 2)
			{
				Grille[x,y + 1].change_couleur(3);
			}
			
			Grille[x,y].change_couleur(4);
			Score++;
			Texte_score.text = Score.ToString();
			Defaite();
		}
		else
		{
			y++;
		}
	}
	else if(Input.GetKeyDown(KeyCode.UpArrow))
	{
		y++;
		if (y > cote-1){
			y--;
			return;
		}
		Cellules Sel = Grille[x,y];
		if(Sel.couleur == 0)
		{
		if(Grille[x,y - 1].couleur != 2)
			{
				Grille[x,y - 1].change_couleur(3);
			}
			Grille[x,y].change_couleur(4);
			Score++;
			Texte_score.text = Score.ToString();
			Defaite();
		}
		else
		{
			y--;
		}
	}
    }

	public void Bouge_Droite()
    {
        x++;
        if (x > cote - 1)
        {
            x--;
            return;
        }
        Cellules Sel = Grille[x, y];
        if (Sel.couleur == 0)
        {
            if (Grille[x - 1, y].couleur != 2)
            {
                Grille[x - 1, y].change_couleur(3);
            }
            Grille[x, y].change_couleur(4);
            Score++;
            Texte_score.text = Score.ToString();
            Defaite();
        }
        else
        {
            x--;
        }
    }

	public void Bouge_Gauche(){
		x--;
		if (x < 0)
		{
			x++;
			return;
		}
		Cellules Sel = Grille[x,y];
		if(Sel.couleur == 0)
		{
			if(Grille[x + 1,y].couleur != 2)
			{
				Grille[x + 1,y].change_couleur(3);
			}
				Grille[x,y].change_couleur(4);
				Score++;
				Texte_score.text = Score.ToString();
				Defaite();
		}
		else
		{
			x++;
		}
	}

	public void Bouge_Bas(){
		y--;
		if (y < 0)
		{
			y++;
			return;
		}
		Cellules Sel = Grille[x,y];
		if(Sel.couleur == 0)
		{
			if(Grille[x,y + 1].couleur != 2)
			{
				Grille[x,y + 1].change_couleur(3);
			}
			
			Grille[x,y].change_couleur(4);
			Score++;
			Texte_score.text = Score.ToString();
			Defaite();
		}
		else
		{
			y++;
		}
	}

	public void Bouge_Haut(){
		y++;
		if (y > cote-1){
			y--;
			return;
		}
		Cellules Sel = Grille[x,y];
		if(Sel.couleur == 0)
		{
		if(Grille[x,y - 1].couleur != 2)
			{
				Grille[x,y - 1].change_couleur(3);
			}
			Grille[x,y].change_couleur(4);
			Score++;
			Texte_score.text = Score.ToString();
			Defaite();
		}
		else
		{
			y--;
		}
	}

	public void Carre(float valeur)
	{
		cote = Mathf.RoundToInt(valeur);
		Carre_txt.text = cote.ToString() + " x " + cote.ToString();
	}
	public void Blanc(float valeur)
	{
		Pourcentage = Mathf.RoundToInt(valeur);
		Pourcentage_txt.text = Pourcentage.ToString() + "%";
	}
	public void Defaite()
	{
		A_perdu = true;
		if(x < cote - 1)
		{
			if(Grille[x + 1,y].couleur == 0)
			{
				A_perdu = false;
			}
		}
		if(x > 0)
		{
			if(Grille[x - 1,y].couleur == 0)
			{
				A_perdu = false;
			}
		}
		if(y < cote - 1)
		{
			if(Grille[x,y + 1].couleur == 0)
			{
				A_perdu = false;
			}
		}
		if(y > 0)
		{
			if(Grille[x,y - 1].couleur == 0)
			{
				A_perdu = false;
			}
		}
		if(A_perdu == true)
		{
			Rejouer.SetActive(false);
			Home.SetActive(false);
			//Couleur_Fond.SetActive(false);
			Rejouer_Alea.SetActive(false);
			Menu_defaite.SetActive(true);
			Animation.SetBool("joue_anim",true);
			isPlaying = false;
			Score_final.text = Score.ToString();
			Grille_Parent.gameObject.SetActive(false);
		}
	}
	public void Reessayer()
	{
		Seed_random = false;
		Rejouer_Alea.SetActive(false);
		A_deja_clicke = 0;
		A_perdu = false;
		Menu_defaite.SetActive(false);
		Score = 0;
		Texte_score.text = Score.ToString();
		for(int cote_x = 0;cote_x < cote;cote_x++)
		{
			for(int cote_y = 0;cote_y < cote;cote_y++)
			{
				Destroy(Grille[cote_x,cote_y].gameObject);
			}
		}
		Commencer();
	}

	public void Reessayer_Aleatoire()
	{
		Seed_random = true;
		A_deja_clicke = 0;
		A_perdu = false;
		Menu_defaite.SetActive(false);
		Score = 0;
		Texte_score.text = Score.ToString();
		for(int cote_x = 0;cote_x < cote;cote_x++)
		{
			for(int cote_y = 0;cote_y < cote;cote_y++)
			{	
				Destroy(Grille[cote_x,cote_y].gameObject);
			}
		}
		Commencer();
	}
	public void Nouvelle_seed()
	{
		Seed_random = true;
	}
	public void Parametres()
	{
		Texte_score.gameObject.SetActive(false);
		isPlaying = false;
		Menu_parametres.SetActive(false);
		Rejouer_Alea.SetActive(false);
		Param.SetActive(false);
		Rejouer.SetActive(false);
		Seed_random = false;
		A_deja_clicke = 0;
		A_perdu = false;
		Menu_defaite.SetActive(false);
		Score = 0;
		Texte_score.text = Score.ToString();
		//Couleur_Fond.SetActive(false);
		for(int cote_x = 0;cote_x < cote;cote_x++)
		{
			for(int cote_y = 0;cote_y < cote;cote_y++)
			{
				Destroy(Grille[cote_x,cote_y].gameObject);
			}
		}
		Menu_parametres.SetActive(true);
	}
}