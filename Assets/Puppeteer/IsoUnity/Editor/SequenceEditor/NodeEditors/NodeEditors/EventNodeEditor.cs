using UnityEngine;
using UnityEditor;
using IsoUnity.Events;

namespace IsoUnity.Sequences {
	public class EventNodeEditor : NodeEditor {

		private SequenceNode node;
	    private EventEditor currentEditor;
        private string previousName = "";

		public void draw(){
			SerializableGameEvent ge = (SerializableGameEvent)node.Content;
			string[] editors = EventEditorFactory.Intance.CurrentEventEditors;
            int editorSelected = 0;

			for (int i = 1; i< editors.Length; i++)
				if (ge != null && editors [i].ToLower () == ge.Name.ToLower ())
					editorSelected = i;

            var was = editorSelected;
            editorSelected = EditorGUILayout.Popup (editorSelected, EventEditorFactory.Intance.CurrentEventEditors);
	        if (currentEditor == null || previousName != ge.Name.ToLower() || editorSelected != was)
            {
	            if(currentEditor != null && ge != null)
	                currentEditor.detachEvent(ge);

	            if(editorSelected != was && ge != null)
	                ge.Name = "";

	            currentEditor = EventEditorFactory.Intance.createEventEditorFor(editors[editorSelected]);
	            currentEditor.useEvent(ge);
	        }

	        currentEditor.draw();

	        /**
	         *  Game event synchronization
	         * */

	        if (!(ge.getParameter("synchronous") is bool)) ge.setParameter("synchronous", false);
			ge.setParameter("synchronous", EditorGUILayout.Toggle("Synchronous", 
			    (ge.getParameter("synchronous") == null)?false:(bool) ge.getParameter("synchronous")));

			if((bool)ge.getParameter("synchronous"))
				EditorGUILayout.HelpBox("Notice that if there is no EventFinished event, the game will stuck.", MessageType.Warning);
	        /**
	         * Synchronization end
	         * */

			node.Content = currentEditor.Result;
			
			if (Event.current.type != EventType.Layout){
	            node.ChildSlots = 1;
			}

            previousName = ge.Name.ToLower();

        }
		
		public SequenceNode Result { get{ return node; } }
		public string NodeName{ get { return "GameEvent"; } }

	    public string[] ChildNames
	    {
	        get
	        {
	            return new string[]{ "default" };
	        }
	    }

	    public NodeEditor clone(){ return new EventNodeEditor(); }
		
		public bool manages(SequenceNode c) { return c.Content != null && c.Content is SerializableGameEvent; }
		public void useNode(SequenceNode c) {
			if (c.Content == null || !(c.Content is SerializableGameEvent)) {
				c.Content = ScriptableObject.CreateInstance<SerializableGameEvent> ();
			}
			var sge = c.Content as SerializableGameEvent;
			if(sge.Name == null) sge.Name = "";

			this.node = c;
		}
	}
}