﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance;

    public GameObject ovenPanel;
    public GameObject tapPanel;
    public GameObject ceramicHobPanel;
    public GameObject tablePanel;
    public GameObject refrigeratorPanel;
    public GameObject correct;
    public GameObject mistake;
    public GameObject timer;
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public GameObject buttonPanel;
    public GameObject recipePanel;

    // Numero de veces que puede ver el panel de las recetas
    private const int MAX_HINTS = 3;
    private static int _counterHints = 0;
    private static GameObject _instanceButtonPanel;
    private static GameObject _instanceRecipePanel;

    private static GameObject _instanceTimer;
    
    //Recipe
    public List<Step> steps;
    private float _time = 280.0f;


    private static int _currentStep = 0;
    private static DisplayPanel displayPanel;
    private static Step lastStep;

    // Use this for initialization
    void Awake()
    {

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        // Load the timer
        displayPanel = GetComponent<DisplayPanel>();
        _instanceTimer = displayPanel.instantiatePanel(timer);
        lastStep = new Step();

        _currentStep = 0;
        _counterHints = 0;
        
        _instanceButtonPanel = displayPanel.instantiatePanel(buttonPanel);
        _instanceRecipePanel = displayPanel.instantiatePanel(recipePanel);

        UpdateHintsCounter(MAX_HINTS);

        _instanceRecipePanel.SetActive(true);
        _instanceButtonPanel.SetActive(false);
        enableClickOnObjects(false);
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
        Debug.Log(_currentStep);
        //Si era el paso que tenia que hacer
        if (lastStep.Equals(steps[_currentStep]))
        {
            //Cambiamos el sprite
            if (steps[_currentStep].sprite != null)
            {
                if (steps[_currentStep].action == Action.Ninguno)
                {
                    //steps[0].drop.GetComponent<SpriteRenderer>().sprite = steps[0].sprite;
                    lastStep.drop.GetComponent<SpriteRenderer>().sprite = steps[_currentStep].sprite;

                }
                else
                {
                    //steps[0].drag.GetComponent<SpriteRenderer>().sprite = steps[0].sprite;
                    lastStep.drag.GetComponent<SpriteRenderer>().sprite = steps[_currentStep].sprite;
                }
            }

            if (steps[_currentStep].action == Action.Ninguno)
            {
                Destroy(lastStep.drag);
            }

                //Eliminamos de la receta el paso realizado correctamente
                //steps.RemoveAt(0);


                //Mostramos éxito y lo tachamos de la receta
                Debug.Log("Éxito");
            _currentStep++;

            //Instanciar el tick del shadowEffect como que ha tenido exito
            displayPanel.instantiatePanel(correct, lastStep.drop);

            if (steps.Count == _currentStep)
            {
                Debug.Log("Has ganado");
                enableClickOnObjects(false);
                displayPanel.instantiatePanel(winPanel);
                Destroy(_instanceTimer);
            }
        }
        else
        {
            Debug.Log("Error");

            //Como ha tenido un error devolvemos el objeto a su posicion inicial
            lastStep.drag.GetComponent<DragObject>().returnToStartPoint();

            //Instanciamos la X del shadowEffect
            displayPanel.instantiatePanel(mistake, lastStep.drop);
            lastStep.drag.GetComponent<DragObject>().returnToStartPoint();
        }
    }

    public void ButtonPressed(string action)
    {
        lastStep.action = stringToAction(action);

        displayPanel.DestroyPanel();

        enableClickOnObjects(true);

        CheckStep();
    }

    // Cunaod hay un menu abierto y el usuario pulsa la X
    public void CancelAction()
    {
        enableClickOnObjects(true);
        lastStep.drag.GetComponent<DragObject>().returnToStartPoint();
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
        enableClickOnObjects(false);
        displayPanel.instantiatePanel(gameOverPanel);
    }
    
    // Muestra el panel con los pasos que debe realizar
    public void ShowHints()
    {
        if (_counterHints >= MAX_HINTS)
            return;

        enableClickOnObjects(false);

        Debug.Log("Mostrar (" + _counterHints + " veces abierto)");

        _instanceRecipePanel.SetActive(true);
        _counterHints++;

        // Actualizamos el contador del boton y lo escondemos
        UpdateHintsCounter(MAX_HINTS - _counterHints);
        _instanceButtonPanel.SetActive(false);
    }

    private void UpdateHintsCounter(int value)
    {
        _instanceButtonPanel.GetComponentInChildren<Text>().text = "" + value;
    }

    public void HideHints()
    {
        enableClickOnObjects(true);
        _instanceRecipePanel.SetActive(false);
        _instanceButtonPanel.SetActive(true);
        Debug.Log("Ocultar (" + _counterHints + " veces abierto)");
    }

    public float time
    {
        get { return _time; }
    }

    public static Action stringToAction(string action)
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

        return a;
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