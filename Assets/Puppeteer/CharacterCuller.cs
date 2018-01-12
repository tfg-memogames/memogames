using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCuller : MonoBehaviour {

    public GameObject character;
	private Dictionary<GameObject, int> characterLayer = new Dictionary<GameObject, int>();

    private void OnPreCull()
    {
        if (character)
        {
			characterLayer.Clear();
            var bounds = character.GetComponent<BoxCollider2D>().bounds;
            var point = bounds.center + new Vector3(0, bounds.extents.y * .66f);

            Camera.current.transform.position = point - Vector3.forward;
            Camera.current.orthographicSize = bounds.size.x / 2f;
			characterLayer.Add(character, character.layer);
			character.layer = character.layer | LayerMask.NameToLayer("Dialog");
			foreach (var child in character.GetComponentsInChildren<Renderer>())
			{
				if (characterLayer.ContainsKey(child.gameObject))
				{
					characterLayer[child.gameObject] = child.gameObject.layer;
				} else
				{
					characterLayer.Add(child.gameObject, child.gameObject.layer);
				}
				child.gameObject.layer = child.gameObject.layer | LayerMask.NameToLayer("Dialog");
			}
        }
    }

    private void OnPostRender()
    {
        if (character)
		{
			character.layer = LayerMask.NameToLayer("Default");
			foreach (var child in character.GetComponentsInChildren<Renderer>())
			{
				child.gameObject.layer = LayerMask.NameToLayer("Default");
			}
		}
    }

}
