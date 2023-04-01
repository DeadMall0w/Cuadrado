using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Vector2 startPos;
    public Vector2 dstTouch;
    public Camera cam;
    bool isclicking = false;
    Vector2 currentMousePos;

   // int dir = -1; // 0 up, 1 down, 2 right, 3 left
    bool isUpDown;
    public Quadrillage quad;
    public GameObject g;
    private void Start()
    {
        g.SetActive(false);
    }
    private void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            startPos = cam.ScreenToWorldPoint(Input.mousePosition);
            isclicking = true;

        }

        if (isclicking)
        {
            currentMousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            dstTouch = startPos - currentMousePos;
        }

        if (Input.GetMouseButtonUp(0))
        { // lache le click et envoie le jouer dans les airs
            isclicking = false;
            dstTouch = startPos - currentMousePos;

            print(dstTouch);
            if(!quad.isPlaying){
                return;
            }
            if (Mathf.Abs(dstTouch.x) > Mathf.Abs(dstTouch.y))
            {
                //  if(dstTouch.x > 4 && dstTouch.x < 4){
                //     print("trop petit");
                //     return;
                // }
                // x axis:
                if (dstTouch.x > 4)
                {
                    quad.Bouge_Gauche();
                }
                else if (dstTouch.x < -4)
                {
                    quad.Bouge_Droite();

                }
                else
                {

                }
            }
            else
            {
                //y axis:
                // if(dstTouch.y > 4 && dstTouch.y < 4){
                //     print("trop petit");
                //     return;
                // }
                if (dstTouch.y > 4)
                {
                    quad.Bouge_Bas();

                }
                else if (dstTouch.y < -4)
                {
                    quad.Bouge_Haut();

                }
                else
                {

                }
            }

        }
    }


}