using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

[System.Serializable]
public class Sequence : ScriptableObject, ISerializationCallbackReceiver {

    [SerializeField]
    private bool inited = false;

	[SerializeField]
    protected SequenceNode root;


    protected Dictionary<string, SequenceNode> nodeDict;


    void Awake()
    {
        if(this.nodes == null)
            this.nodes = new List<SequenceNode>();
        if (this.ids == null)
            this.ids = new List<string>();
        if (this.nodeDict == null)
            this.nodeDict = new Dictionary<string, SequenceNode>();
    }

	public SequenceNode Root
    {
		get{ return root;}
		set{ root = value;}
	}

    public SequenceNode[] Nodes
    {
        get { return nodes.ToArray() as SequenceNode[]; }
    }

    public SequenceNode this[string node]
    {
        get
        {
            if (!nodeDict.ContainsKey(node)) CreateNode(node, null);
            return nodeDict[node];
        }
    }

    public virtual SequenceNode CreateNode(string id, object content = null, int childSlots = 0)
    {
        var node = CreateInstance<SequenceNode>();
        node.init(this);
        this.nodeDict.Add(id, node);
        node.Content = content;
        return node;
    }

    public virtual SequenceNode CreateNode(object content = null, int childSlots = 0)
    {
        var node = CreateInstance<SequenceNode>();
        node.init(this);
        this.nodeDict.Add(node.GetInstanceID().ToString(), node);
        node.Content = content;
        return node;
    }

    public virtual bool RemoveNode(SequenceNode node)
    {
        var id = string.Empty;
        foreach(var kv in nodeDict)
        {
            if(kv.Value == node)
            {
                id = kv.Key;
                break;
            }
        }

        return string.IsNullOrEmpty(id) ? false : RemoveNode(id);
    }

    public virtual bool RemoveNode(string id)
    {
        var contains = nodeDict.ContainsKey(id);
        if (contains)
        {
            var node = nodeDict[id];
            nodeDict.Remove(id);
            SequenceNode.DestroyImmediate(node, true);
        }
        return contains;
    }

    private void findNodes(SequenceNode node, Dictionary<SequenceNode, bool> checkList)
    {
        if (node == null)
            return;

        if (checkList.ContainsKey(node))
            checkList[node] = true;

        foreach (var c in node.Childs)
            findNodes(c, checkList);
    }


    public int FreeNodes
    {
        get
        {
            Dictionary<SequenceNode, bool> found = new Dictionary<SequenceNode, bool>();
            foreach (SequenceNode sn in nodes)
                found.Add(sn, false);

            findNodes(root, found);

            int free = 0;
            foreach (var v in found.Values) if (!v) free++;

            return free;
        }
    }


    /**************************
     * Serialization
     * ***********************/
    [SerializeField]
    private List<SequenceNode> nodes;
    [SerializeField]
    private List<string> ids;

    public virtual void OnBeforeSerialize()
    {
        this.nodes = nodeDict.Values.ToList();
        this.ids = nodeDict.Keys.ToList();
    }

    public virtual void OnAfterDeserialize()
    {
        nodeDict = new Dictionary<string, SequenceNode>();

        using (var n = nodes.GetEnumerator())
        using (var i = ids.GetEnumerator())
        {
            while (n.MoveNext() && i.MoveNext())
                nodeDict.Add(i.Current, n.Current);
        }
    }

    /*public Rect getRectFor(SecuenceNode node)
    {
        int i = nodes.IndexOf(node);
        Rect r = positions[i];
        if (r == null || r.width == 0)
        {
            // TODO reposition
            r = new Rect(10, 10, 300, 0);
            positions[i] = r;
        }

        return r;
    }

    public void setRectFor(SecuenceNode node, Rect rect)
    {
        int i = nodes.IndexOf(node);
        positions[i] = rect;
    }*/
}
