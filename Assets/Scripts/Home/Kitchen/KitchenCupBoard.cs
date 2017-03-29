using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class KitchenCupBoard : MonoBehaviour
{

    public Sprite openned;
    public Sprite closed;

    private SpriteRenderer sr;

    private bool isClose;


    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }


    public void OpenCloseDoor()
    {
        if (isClose)
        {

            isClose = false;

            //Change Sprite
            sr.sprite = openned;

            //Show child object (show objects inside in)

        }
        else
        {
            isClose = true;

            //Change Sprite 
            sr.sprite = closed;

            //Hide child object (it can't be seen because the door ir close)
        }

        ShowChildObjects(!isClose);
    }

    private void ShowChildObjects(bool show)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(show);
        }
    }

}