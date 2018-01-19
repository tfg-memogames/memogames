using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupIntersections : MonoBehaviour
{

    private enum dirs { NE, NW, SE, SW };
    private static string path = "Prefabs/FindTheWay/Arrow";

    // Use this for initialization
    void Start()
    {


        //En vez de añadir el script Arrow a la baldosa de la intersección añadimos únicamente el prefab
        //Este prefab ya tendrá el script Arrow

        GameObject arrow = Resources.Load<GameObject>(path + "/General_Arrow");


        GameObject[] intersections = GameObject.FindGameObjectsWithTag("I");
        foreach (GameObject i in intersections)
        {
            Intersection inter = i.AddComponent<Intersection>();

            for (int count = 0; count < i.transform.childCount; count++)
            {
                GameObject child = i.transform.GetChild(count).gameObject;

                //No incluimos el script Arrow a las baldosas
                child.AddComponent<Arrow>();

                //Ahora incluimos otro script que comprueba si una flecha ha sido pulsada

                if (child.name == dirs.NE.ToString())
                {
                    GameObject a = Instantiate(arrow, child.transform);
                    a.transform.localPosition = Vector3.zero;
                    //Rotación

                    a.transform.rotation = Quaternion.Euler(180, 0, 0);

                    a.SetActive(false);
                    inter.AddArrow(a);

                }
                else if (child.name == dirs.NW.ToString())
                {
                    GameObject a = Instantiate(arrow, child.transform);
                    a.transform.localPosition = Vector3.zero;
                    //Rotación

                    a.transform.rotation = Quaternion.Euler(180, 180, 0);


                    a.SetActive(false);
                    inter.AddArrow(a);
                }
                else if (child.name == dirs.SE.ToString())
                {
                    GameObject a = Instantiate(arrow, child.transform);
                    a.transform.localPosition = Vector3.zero;
                    a.transform.rotation = Quaternion.Euler(0, 0, 0);
                    a.SetActive(false);
                    inter.AddArrow(a);
                }
                else if (child.name == dirs.SW.ToString())
                {
                    GameObject a = Instantiate(arrow, child.transform);
                    a.transform.localPosition = Vector3.zero;
                    //Rotación

                    a.transform.rotation = Quaternion.Euler(0, 180, 0);

                    a.SetActive(false);
                    inter.AddArrow(a);
                }

            }
        }
    }



}