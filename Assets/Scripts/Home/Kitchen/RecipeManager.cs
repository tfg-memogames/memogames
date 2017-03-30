using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{

    public static RecipeManager instance;

    public GameObject ovenPanel;
    public GameObject tapPanel;
    public GameObject ceramicHobPanel;
    public GameObject tablePanel;
    public GameObject refrigeratorPanel;
    public List<Step> steps;
    public GameObject timer;

    private static DisplayPanel displayPanel;
    private static Step lastStep; 

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        Instantiate(timer);
    }

    void Start()
    {
        displayPanel = GetComponent<DisplayPanel>();
        lastStep = new Step();
    }

    public void ItemWasDropped(GameObject drag, GameObject drop)
    {
        lastStep.drag = drag;
        lastStep.drop = drop;

        enableClickOnObjects(false);

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

                enableClickOnObjects(true);

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

                }
                else
                {
                    steps[0].drag.GetComponent<SpriteRenderer>().sprite = steps[0].sprite;
                }
            }

            //Eliminamos de la receta el paso realizado correctamente
            steps.RemoveAt(0);
            //Mostramos éxito y lo tachamos de la receta
            Debug.Log("Éxito");

            //Instanciar el tick del shadowEffect como que ha tenido exito

            if (steps.Count == 0)
            {
                Debug.Log("Has ganado");
                enableClickOnObjects(false);
            }
        }
        else
        {
            Debug.Log("Error");

            //Como ha tenido un error devolvemos el objeto a su posicion inicial
            lastStep.drag.GetComponent<DragObject>().returnToStartPoint();

            //Instanciamos la X del shadowEffect
        }
    }

    public void ButtonPressed(string action)
    {

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

        displayPanel.DestroyPanel();

        enableClickOnObjects(true);

        CheckStep();
    }

    private void enableClickOnObjects(bool flag)
    {
        GameObject[] drags = GameObject.FindGameObjectsWithTag("Item");

        foreach (GameObject go in drags)
        {
            if (flag) go.layer = 0; //Default
            else go.layer = 2; //Ignore raycast
        }
    }

    public void GameOver()
    {
        Debug.Log("You lose");
    }
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