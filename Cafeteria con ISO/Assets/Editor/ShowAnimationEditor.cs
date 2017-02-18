using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShowAnimationEditor : EventEditor {

    private SerializableGameEvent editing;

    public string EventName
    {
        get
        {
            return "show animation";
        }
    }

    public SerializableGameEvent Result
    {
        get
        {
            return editing;
        }
    }

    public EventEditor clone()
    {
        return new ShowAnimationEditor();
    }

    public void detachEvent(SerializableGameEvent ge)
    {

    }

    public void draw()
    {
        editing.Name = "show animation";
        editing.setParameter("entity", EditorGUILayout.TextField("Entity", (string) editing.getParameter("entity")));
        editing.setParameter("animation", EditorGUILayout.TextField("Animation", (string)editing.getParameter("animation")));
    }

    public void useEvent(SerializableGameEvent ge)
    {
        editing = ge;
    }
}
