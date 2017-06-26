using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RAGE.Analytics;

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
    private const float _MAXTIME = 280.0f;
    private static float _time = 0;
    private bool _gameCompleted = false;
    private Counter _counter;

    private static int _currentStep = 0;
    private static DisplayPanel displayPanel;
    private static Step lastStep;

    private string level;

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
        _counter = _instanceTimer.GetComponent<Counter>();
        lastStep = new Step();

        _currentStep = 0;
        _counterHints = 0;
        _gameCompleted = false;
        
        _instanceButtonPanel = displayPanel.instantiatePanel(buttonPanel);
        _instanceRecipePanel = displayPanel.instantiatePanel(recipePanel);

        UpdateHintsCounter(MAX_HINTS);

        _instanceRecipePanel.SetActive(true);
        _instanceButtonPanel.SetActive(false);
        enableClickOnObjects(false);

        // Tracker: recogemos el nombre del nivel
        this.level = SceneManager.GetActiveScene().name;

        /*string var = "";

        for (int i = 0; i < this.steps.Count; i++)
        {
            var += i + ":" + steps[i].drag.name + "-" + steps[i].drop.name + "-" + steps[i].action;
        }

        Tracker.T.setVar("Pasos", var);*/

        Tracker.T.setVar("Tiempo", MAXTIME);
        Tracker.T.setVar("NumPasos", steps.Count);
        Tracker.T.Completable.Initialized(this.level);
    }

    void Update()
    {
        if (!_gameCompleted && _time < _MAXTIME)
        {
            _time += Time.deltaTime;
            _counter.PaintTheTime(_time, _MAXTIME);
        }
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
                    lastStep.drop.GetComponent<SpriteRenderer>().sprite = steps[_currentStep].sprite;

                }
                else
                {
                    lastStep.drag.GetComponent<SpriteRenderer>().sprite = steps[_currentStep].sprite;
                }
            }

            if (steps[_currentStep].action == Action.Ninguno)
            {
                Destroy(lastStep.drag);
            }

                //Mostramos éxito y lo tachamos de la receta
                Debug.Log("Éxito");

            _currentStep++;

            // Sacamos el objeto arrastrado de su padre (KitchenCupBoard) para que no se oculte al cerrar el armario
			if(lastStep.drag.transform.parent.name.Equals("KitchenCupBoard"))
				lastStep.drag.transform.SetParent(lastStep.drag.transform.parent.parent);

            // Tracker: notificar que ha hecho un paso correcto y el segundo en el que lo ha hecho
            NotifyStepToTracker(true);

            //Instanciar el tick del shadowEffect como que ha tenido exito
            displayPanel.instantiatePanel(correct, lastStep.drop);

            if (steps.Count == _currentStep)
            {
                Debug.Log("Has ganado");
                _gameCompleted = true;
                enableClickOnObjects(false);
                displayPanel.instantiatePanel(winPanel);
                // Tracker: notificar que el jugador ha ganado y el tiempo que le ha sobrado
                NotifyEndOfGameToTracker(_time);
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

            // Tracker: notificamos error, paso que tocaba hacer (numero), paso que ha hecho (drag.name + drop.name + action), tiempo actual
            NotifyStepToTracker(false);
        }
    }

    private void NotifyStepToTracker(bool correctStep)
    {
        Debug.Log("Time:" + _time);
        Tracker.T.setVar("Tiempo", _time);
        Tracker.T.setVar("ObjArrastrado", lastStep.drag.name);
        Tracker.T.setVar("ArrastradoA", lastStep.drop.name);
        Tracker.T.setVar("Accion", lastStep.action.ToString());

        string id = correctStep ? "PasoCorrecto" : "PasoEquivocado";

        Tracker.T.trackedGameObject.Interacted(id);
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

        GameObject[] cupBoards = GameObject.FindGameObjectsWithTag("KitchenCupBoard");

        foreach (GameObject go in cupBoards)
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
        Destroy(_instanceRecipePanel);
        Destroy(_instanceButtonPanel);
        // Tracker: ha perdido, y el numero de paso en el que se ha quedado
        NotifyEndOfGameToTracker(MAXTIME);
    }

    //Envia al tracker, el paso en el que se ha quedado, si ha ganado, el tiempo
    private void NotifyEndOfGameToTracker(float time)
    {
        Tracker.T.setVar("Time", time);
        Tracker.T.Completable.Completed(this.level, CompletableTracker.Completable.Level, _gameCompleted, _currentStep);
    }
    
    // Muestra el panel con los pasos que debe realizar
    public void ShowHints()
    {
        if (_counterHints >= MAX_HINTS)
            return;

        // Tracker: notificamos que ha abierto el panel de las recetas
        Tracker.T.trackedGameObject.Interacted("AbreReceta");

        enableClickOnObjects(false);

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
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void end(){
		//
		SceneManager.LoadScene("Black");

	}

    public float time
    {
        get { return _time; }
    }

    public float MAXTIME
    {
        get { return _MAXTIME; }
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