using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public RecipeManager gameManager;

    void Awake()
    {
        if (RecipeManager.instance == null)
            Instantiate(gameManager);
    }
}
