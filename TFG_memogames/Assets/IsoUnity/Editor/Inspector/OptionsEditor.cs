using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(Options))]
public class OptionsEditor : Editor {

    private ReorderableList optionsReorderableList;

    private void OnEnable()
    {
        optionsReorderableList = new ReorderableList(new ArrayList(), typeof(Option), true, true, true, true);
        //optionsReorderableList.elementHeight = 70;
        optionsReorderableList.drawHeaderCallback += DrawOptionsHeader;
        optionsReorderableList.drawElementCallback += DrawOption;
        optionsReorderableList.onAddCallback += AddOption;
        optionsReorderableList.onRemoveCallback += RemoveOption;
        optionsReorderableList.onReorderCallback += ReorderOptions;
    }


    private Options options;
    public override void OnInspectorGUI()
    {
        options = target as Options;

        //EditorGUILayout.HelpBox("Options are the lines between you have to choose at the end of the dialog. Leave empty to do nothing, put one to execute this as the dialog ends, or put more than one to let the player choose between them.", MessageType.None);
        if (optionsReorderableList.list != null)
        {
            int i = optionsReorderableList.count;
        }
        optionsReorderableList.list = options.Values;
        optionsReorderableList.DoLayoutList();
    }



    /**************************
     * OPTIONS LIST OPERATIONS
     ***************************/

    Rect labelRect = new Rect(0, 2, 35, 15);
    Rect optionRect = new Rect(40, 2, 185, 15);
    private void DrawOptionsHeader(Rect rect)
    {
        GUI.Label(rect, "Dialog options");
    }

    private void DrawOption(Rect rect, int index, bool active, bool focused)
    {
        Option opt = (Option)optionsReorderableList.list[index];

        EditorGUI.LabelField(moveRect(labelRect, rect), "Text: ");
        opt.Text = EditorGUI.TextField(moveRect(optionRect, rect), opt.Text);
    }

    private void AddOption(ReorderableList list)
    {
        options.AddOption();
    }

    private void RemoveOption(ReorderableList list)
    {
        options.removeOption(options.Values[list.index]);
    }

    private void ReorderOptions(ReorderableList list)
    {
        List<Option> l = (List<Option>)optionsReorderableList.list;
        options.Values = l;
    }

    /**
     * moveRect
     * */
     
    private Rect moveRect(Rect target, Rect move)
    {
        Rect r = new Rect(move.x + target.x, move.y + target.y, target.width, target.height);

        if (r.x + r.width > move.x + move.width)
        {
            r.width = (move.width + 25) - r.x;
        }

        return r;
    }
}
