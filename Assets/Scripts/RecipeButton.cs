using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeButton : MonoBehaviour
{ 
    public RecipeManager recipeManager;

    public void ButtonPressed(string action)
    {
        Action a;

        switch (action)
        {
            case "Bake":
                a = Action.Hornear;
                break;
            case "Broil":
                a = Action.Gratinar;
                break;
            case "FillWithWater":
                a = Action.Llenar;
                break;
            case "Empty":
                a = Action.Vaciar;
                break;
            case "Grill":
                a = Action.ALaPlancha;
                break;
            case "Boil":
                a = Action.Hervir;
                break;
            case "Fry":
                a = Action.Freir;
                break;
            case "Chop":
                a = Action.Picar;
                break;
            case "Peel":
                a = Action.Pelar;
                break;
            case "Cut":
                a = Action.Cortar;
                break;
            case "Cool":
                a = Action.Enfriar;
                break;
            case "Freeze":
                a = Action.Congelar;
                break;
            default:
                a = Action.Ninguno;
                break;

        }

        //recipeManager.ButtonPressed(a);
    }
}
