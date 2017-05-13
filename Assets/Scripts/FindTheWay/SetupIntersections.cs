using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupIntersections : MonoBehaviour {

    private enum dirs {NE, NW, SE, SW};
    private static string path = "Prefabs/FindTheWay/Arrow";

	// Use this for initialization
	void Start () {

        GameObject[] arrows = Resources.LoadAll<GameObject>(path);

        GameObject arrow_NE = Resources.Load<GameObject>(path + "/Arrow_NE");
        GameObject arrow_NW = Resources.Load<GameObject>(path + "/Arrow_NW");
        GameObject arrow_SE = Resources.Load<GameObject>(path + "/Arrow_SE");
        GameObject arrow_SW = Resources.Load<GameObject>(path + "/Arrow_SW");

        GameObject[] intersections = GameObject.FindGameObjectsWithTag("I");		
        foreach (GameObject i in intersections)
        {
            Intersection inter = i.AddComponent<Intersection>();

            for (int count = 0; count < i.transform.childCount; count++)
            {
                GameObject child = i.transform.GetChild(count).gameObject;
                child.AddComponent<Arrow>();
                

                if (child.name == dirs.NE.ToString())
                {
                    GameObject a = Instantiate(arrow_NE, child.transform);
                    a.transform.localPosition = Vector3.zero;
                    
                    a.SetActive(false);
                    inter.AddArrow(a);

                } else if (child.name == dirs.NW.ToString())
                {
                    GameObject a = Instantiate(arrow_NW, child.transform);
                    a.transform.localPosition = Vector3.zero;
                    
                    a.SetActive(false);
                    inter.AddArrow(a);
                }
                else if (child.name == dirs.SE.ToString())
                {
                    GameObject a = Instantiate(arrow_SE, child.transform);
                    a.transform.localPosition = Vector3.zero;
                    
                    a.SetActive(false);
                    inter.AddArrow(a);
                }
                else if (child.name == dirs.SW.ToString())
                {
                    GameObject a = Instantiate(arrow_SW, child.transform);
                    a.transform.localPosition = Vector3.zero;
                    
                    a.SetActive(false);
                    inter.AddArrow(a);
                }

            }
        }
    }
	

	
}
