using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Objeto : MonoBehaviour
{
    public string[] words;

    void Start()
    {
       
    }

    public string[]  dameDic(out int id)
    {
        id = 0;
        for (int i = 0; i < this.name.Length; i++) id += (int)this.name[i];
        return words;
    }
    void Update()
    {
       
    }

}
