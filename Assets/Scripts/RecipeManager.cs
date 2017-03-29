using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour {

    public static RecipeManager instance;  

    public GameObject ovenPanel;
    public GameObject tapPanel;
    public GameObject ceramicHobPanel;
    public GameObject tablePanel;
    public GameObject refrigeratorPanel;
    public List<Step> steps;

    private DisplayPanel displayPanel;
    private static Step lastStep; //??? static

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        displayPanel = GetComponent<DisplayPanel>();
        lastStep = new Step();
	}

    public void ItemWasDropped(GameObject drag, GameObject drop)
    {
        lastStep.drag = drag;
        lastStep.drop = drop;

        switch (drop.name)
        {
            case "Oven":
                displayPanel.instantiatePanel(ovenPanel, drag);
                break;
            case "Tap":
                displayPanel.instantiatePanel(tapPanel, drag);
                break;
            case "CeramicHob":
                displayPanel.instantiatePanel(ceramicHobPanel, drag);
                break;
            case "Table":
                displayPanel.instantiatePanel(tablePanel, drag);
                break;
            case "Refrigerator":
                displayPanel.instantiatePanel(refrigeratorPanel, drag);
                break;
            default:
                lastStep.action = Action.Ninguno;

                CheckStep();
               
                break;
        }
        
    }

    //Compara lastStep con el paso que tocaba realizar y actualiza el juego
    private void CheckStep()
    {
        //Si era el paso que tenia que hacer
        if (lastStep.Equals(steps[0]))
        {
            //Cambiamos el sprite
            if (steps[0].sprite != null)
            {
                if (steps[0].action == Action.Ninguno)
                {
                    steps[0].drop.GetComponent<SpriteRenderer>().sprite = steps[0].sprite;

                } else
                {
                    steps[0].drag.GetComponent<SpriteRenderer>().sprite = steps[0].sprite;
                }
            }

            //Eliminamos de la receta el paso realizado correctamente
            steps.RemoveAt(0);
            //Mostramos éxito y lo tachamos de la receta
            Debug.Log("Éxito"); 

            if (steps.Count == 0)
            {
                Debug.Log("Has ganado");
            }
        }
        else
        {
            Debug.Log("Error");
        }
    }

    public void ButtonPressed(string action)
    {
        //Debug.Log(lastStep.drag);
        //Debug.Log(lastStep.drop);

        switch (action)
        {
            case "Bake":
                lastStep.action = Action.Hornear;
                break;
            case "Broil":
                lastStep.action = Action.Gratinar;
                break;
            case "FillWithWater":
                lastStep.action = Action.Llenar;
                break;
            case "Empty":
                lastStep.action = Action.Vaciar;
                break;
            case "Grill":
                lastStep.action = Action.ALaPlancha;
                break;
            case "Boil":
                lastStep.action = Action.Hervir;
                break;
            case "Fry":
                lastStep.action = Action.Freir;
                break;
            case "Chop":
                lastStep.action = Action.Picar;
                break;
            case "Peel":
                lastStep.action = Action.Pelar;
                break;
            case "Cut":
                lastStep.action = Action.Cortar;
                break;
            case "Cool":
                lastStep.action = Action.Enfriar;
                break;
            case "Freeze":
                lastStep.action = Action.Congelar;
                break;
            default:
                lastStep.action = Action.Ninguno;
                break;
        }

        CheckStep();

        displayPanel.DestroyPanel();
    }

    /*
    #region ButtonsFunctions

    #region OvenFunctions

    //Llamada cuando se pulsa el boton hornear
    public void Bake()
    {
        lastStep.action = Action.Hornear;
        displayPanel.DestroyPanel();
    }

    //Llamada cuando se pulsa el boton gratinar
    public void Broil()
    {
        lastStep.action = Action.Gratinar;
        displayPanel.DestroyPanel();
    }

    #endregion

    #region TapFunctions

    //Llamada cuando se da al boton añadir del grifo
    public void FillWithWater()
    {
        lastStep.action = Action.Llenar;
    }

    //Llamada cuando se da al boton vaciar del grifo
    public void Empty()
    {
        lastStep.action = Action.Vaciar;
    }

    #endregion

    #region CeramicHobFunctions

    //Llamada cuando se pulsa el boton de hacer a la plancha
    public void Grill()
    {
        lastStep.action = Action.ALaPlancha;
    }

    //Llamada cuando se pulsa el boton de hervir
    public void Boil()
    {
        lastStep.action = Action.Hervir;
    }

    //Llamada cuando se pulsa el boton de freir
    public void Fry()
    {
        lastStep.action = Action.Freir;
    }

    #endregion

    #region TableMenu

    //Llamada cuando se pulsa el boton picar
    public void Chop()
    {
        lastStep.action = Action.Picar;
    }

    //Llamada cuando se pulsa el boton pelar
    public void Peel()
    {
        lastStep.action = Action.Pelar;
    }

    //Lamada cuando se pulsa el boton cortar
    public void Cut()
    {
        lastStep.action = Action.Cortar;
    }

    #endregion

    #region Refrigerator
    
    //Llamada cuando se pulsa el boton enfriar
    public void Cool()
    {
        lastStep.action = Action.Enfriar;
    }

    //Llamada cuando se pulsa el boton congelar
    public void Freeze()
    {
        lastStep.action = Action.Congelar;
    }

    #endregion

    #endregion
    */
}

[System.Serializable]
public class Step
{
    public GameObject drag;
    public GameObject drop;
    public Action action;
    public Sprite sprite;

    public Step()
    {
        drag = null;
        drop = null;
        action = Action.Ninguno;
        sprite = null;
    }

    public Step(GameObject drag, GameObject drop, Action action, Sprite sprite)
    {
        this.drag = drag;
        this.drop = drop;
        this.action = action;
        this.sprite = sprite;
    }

    // override object.Equals
    public override bool Equals(object obj)
    {
        //       
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237  
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        // TODO: write your implementation of Equals() here
        Step s = obj as Step;
        return (s.drag.name == drag.name && s.drop.name == drop.name && s.action == action);
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        // TODO: write your implementation of GetHashCode() here
        return base.GetHashCode();
    }
}

[System.Serializable]
public enum Action
{
    Hervir, Freir, ALaPlancha, Hornear, Gratinar, Picar, Pelar, Cortar, Enfriar, Congelar, Llenar, Vaciar, Ninguno
}