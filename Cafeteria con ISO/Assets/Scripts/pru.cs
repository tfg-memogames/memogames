using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class pru : MonoBehaviour {
    public Sequence s;
    [SerializeField]
    public CustomCheck check = new CustomCheck();
	// Use this for initialization
	void Start () {
        if (s != null)
            return;


        s = ScriptableObject.CreateInstance<Sequence>();
        s.Root = s.CreateNode(Dialog.Create(
            new Fragment("Pepito", "Hola, qué tal andas?"),
            new Fragment("José", "Yo bien"),
            new Fragment("Pepito", "Juegas a la play?")
        ));

        s.Root.Childs[0] = s.CreateNode("options1", Options.Create(
            new Option("Sí"),
            new Option("No")
        ));

        // Async child setting (setting child that doesnt have a content yet or have been created)
        // By accessing to s[$name] you access the node or, if it doesnt exist, create a new node with that id
        s["options1"][0] = s["chose1"];
        s["options1"][1] = s["chose2"];

        // Async child content set
        s["chose1"].Content = Dialog.Create(new List<Fragment>()
        {
            new Fragment("José", "Tengo mazo de ganas de jugar!"),
            new Fragment("Pepito", "Pues vamos a enchufarla")
        });

        s["chose2"].Content = Dialog.Create(new List<Fragment>()
        {
            new Fragment("José", "No me apetece nada..."),
            new Fragment("Pepito", "Bueno... no pasa nada...")
        });

        
        s["chose1"][0] = s.CreateNode("switchFork", ISwitchFork.Create("playRota", ISwitchFork.ComparationType.Equal, false));

        // Anonymous nodes (no id is required for those)
        s["switchFork"][0] = s.CreateNode(Dialog.Create(new List<Fragment>()
        {
            new Fragment("Play", "*La play se enciende*"),
            new Fragment("Pepito", "Weeeeh")
        }));
        s["switchFork"][1] = s.CreateNode(Dialog.Create(new List<Fragment>()
        {
            new Fragment("Play", "*La play no responde*"),
            new Fragment("Pepito", "Jope")
        }));

    }

    [System.Serializable]
    public class CustomCheck : IFork
    {
        public bool boolValue;
        public bool check()
        {
            return boolValue;
        }
    }
    bool launched = false;
    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0) && !launched)
        {
            var ge = new GameEvent();
            ge.Name = "start sequence";
            ge.setParameter("sequence", s);
            Game.main.enqueueEvent(ge);
            launched = true;
        }
	}
}
