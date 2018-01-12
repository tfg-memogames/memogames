using System;
using IsoUnity.Events;
using UnityEngine;
using System.Collections;

public class EffectsEventManager : EventedEventManager {

    // #######################
    // Element Handler stuff
    // #######################

    #region ElementHanlders 
    private interface ElementHandler
    {
        GameObject Target { get;  set; }
        float Transparency { get; set; }
        Vector3 Position { get; set; }
        Vector3 Scale { get; set; }
        Vector3 Rotation { get; set; }
    }

    private abstract class AbstractTransformHandler : ElementHandler
    {
        private GameObject target;
        public GameObject Target
        {
            get { return target; }
            set
            {
                target = value;
                OnChangeTarget(target);
            }
        }
        
        public Vector3 Position { get { return target.transform.position; } set { target.transform.position = value; } }
        public Vector3 Scale { get { return target.transform.localScale; } set { target.transform.localScale = value; } }
        public Vector3 Rotation { get { return target.transform.rotation.eulerAngles; } set { target.transform.rotation = Quaternion.Euler(value); } }
        
        public abstract float Transparency { get; set; }
        protected abstract void OnChangeTarget(GameObject target);
    }

    private class SpriteElementHandler : AbstractTransformHandler
    {
        private SpriteRenderer spriteRenderer;

        protected override void OnChangeTarget(GameObject target)
        {
            spriteRenderer = target.GetComponent<SpriteRenderer>();
        }

        public override float Transparency
        {
            get { return spriteRenderer.color.a; }
            set {
                var c = spriteRenderer.color;
                c.a = value;
                spriteRenderer.color = c;
            }
        }

    }

    private class ElementHandlerFactory
    {
        private static ElementHandlerFactory instance;
        public static ElementHandlerFactory Instance { get { return instance == null ? instance = new ElementHandlerFactory() : instance; } }

        public ElementHandler CreateHandlerFor(GameObject target)
        {
            if (target.GetComponent<SpriteRenderer>())
            {
                var r = new SpriteElementHandler();
                r.Target = target;
                return r;
            }

            return null;
        }
    }
    #endregion

    // #######################
    // Events
    // #######################

    #region Events

    [GameEvent(true, false)]
    public IEnumerator Transition(string name, GoEaseType type, Vector3 destination, Vector3 scale, Vector3 rotation, float transparency, float duration)
    {
        var go = GameObject.Find(name);
        if (!go)
        {
            yield return null;
        }
        else
        {
            var ended = false;
            Go.addTween(new GoTween(ElementHandlerFactory.Instance.CreateHandlerFor(go), duration, new GoTweenConfig()
                .setEaseType(type)
                .vector3Prop("Position", destination)
                .vector3Prop("Scale", scale)
                .vector3Prop("Rotation", rotation)
                .floatProp("Transparency", transparency),
                (tween) => ended = true
                ));
            yield return new WaitUntil(() => ended);
        }
    }

    [GameEvent(true, false)]
    public IEnumerator FadeOut(string name, Vector3 movement, Vector3 sizeChange, float duration = 2f, GoEaseType type = GoEaseType.QuintInOut)
    {
        var go = GameObject.Find(name);
        if (!go)
        {
            yield return null;
        }
        else
        {
            var ended = false;
            var handler = ElementHandlerFactory.Instance.CreateHandlerFor(go);

            Go.addTween(new GoTween(handler, duration, new GoTweenConfig()
                .setEaseType(type)
                .vector3Prop("Position", movement, true)
                .vector3Prop("Scale", sizeChange, true)
                .floatProp("Transparency", 0),
                (tween) => ended = true
                ));
            yield return new WaitUntil(() => ended);

            handler.Position = handler.Position - movement;
            handler.Scale = handler.Scale - sizeChange;
        }
    }

    [GameEvent(true, false)]
    public IEnumerator Flip(string name, bool horizontal = true, bool vertical = false, float duration = 1f, GoEaseType type = GoEaseType.QuadInOut)
    {
        var go = GameObject.Find(name);
        if (!go)
        {
            yield return null;
        }
        else
        {
            var ended = false;
            var handler = ElementHandlerFactory.Instance.CreateHandlerFor(go);

            Go.addTween(new GoTween(handler, duration, new GoTweenConfig()
                .setEaseType(type)
                .vector3Prop("Scale", new Vector3(
                    (horizontal ? -1 : 1) * handler.Scale.x,
                    (vertical   ? -1 : 1) * handler.Scale.y,
                    transform.localScale.z)),
                (tween) => ended = true
                ));
            yield return new WaitUntil(() => ended);
        }
    }

    [GameEvent(true, false)]
    public IEnumerator FadeIn(string name, Vector3 movement, Vector3 sizeChange, float duration = 2f, GoEaseType type = GoEaseType.QuintInOut)
    {
        var go = GameObject.Find(name);
        if (!go)
        {
            yield return null;
        }
        else
        {
            var ended = false;
            var handler = ElementHandlerFactory.Instance.CreateHandlerFor(go);

            handler.Position = handler.Position - movement; 
            handler.Scale = handler.Scale - sizeChange;

            Go.addTween(new GoTween(handler, duration, new GoTweenConfig()
                .setEaseType(type)
                .vector3Prop("Position", movement, true)
                .vector3Prop("Scale", sizeChange, true)
                .floatProp("Transparency", 1),
                (tween) => ended = true
                ));
            yield return new WaitUntil(() => ended);
            
        }
    }

    #endregion 
}
