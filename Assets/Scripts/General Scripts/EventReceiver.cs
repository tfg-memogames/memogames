using IsoUnity.Events;
using UnityEngine;

public class EventReceiver : EventManager
{

    public GameObject phone;
    //La pareja debe desaparecer cuando aparece el teléfono.
    public GameObject parent;

    public RecipeAppearing recipe;
    //public GameObject ;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public override void ReceiveEvent(IGameEvent ev)
    {
        if (ev.Name == "Phone Appears")
        {
            phone.SetActive(true);
            parent.SetActive(false);
        }
        else if (ev.Name == "First_Step")
        {
            recipe.showFirstStep();
        }
        else if (ev.Name == "Second_Step")
        {
            recipe.showSecondtStep();
        }
        else if (ev.Name == "Third_Step")
        {
            recipe.showThirdStep();
        }
        else if (ev.Name == "Fourth_Step")
        {
            recipe.showFourthStep();
        }
    }

    public override void Tick() { }
}
