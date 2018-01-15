using IsoUnity.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class MovementManager : EventedEventManager
{
    public string Zone;
    public List<GameObject> Places;

    private Camera camera;
    private Blackout blackout;
    private Blur blur;

    public void Start()
    {
        camera = Camera.main;
        blackout = camera.GetComponent<Blackout>();
        blur = camera.GetComponent<Blur>();
    }

    [GameEvent(true, false)]
    public IEnumerator Blackout()
    {
        blackout.enabled = true;
        blur.enabled = true;

        int started = 0;

        Go.addTween(new GoTween(blackout, 0.1f, new GoTweenConfig()
            .floatProp("RampOffset", -1f)
            .setDelay(0.2f)
            .setEaseType(GoEaseType.Linear),
            (tween) => started--));
        started++;

        Go.addTween(new GoTween(blur, 0.3f, new GoTweenConfig()
            .floatProp("BlurSpread", 1)
            .intProp("Iterations", 10)
            .setEaseType(GoEaseType.Linear),
            (tween) => started--));
        started++;

        yield return new WaitUntil(() => started == 0);
    }

    [GameEvent(true, false)]
    public IEnumerator Backin()
    {
        int started = 0;

        Go.addTween(new GoTween(blackout, 0.3f, new GoTweenConfig()
            .floatProp("RampOffset", 0)
            .setEaseType(GoEaseType.Linear),
            (tween) => started--));
        started++;

        Go.addTween(new GoTween(blur, 0.5f, new GoTweenConfig()
            .floatProp("BlurSpread", 0)
            .intProp("Iterations", 0)
            .setEaseType(GoEaseType.Linear),
            (tween) => started--));
        started++;

        yield return new WaitUntil(() => started == 0);
        
        blackout.enabled = false;
        blur.enabled = false;
    }

    [GameEvent(true, false)]
	public IEnumerator Move(string zone, string place)
    {
        var go = Places.Find(p => p.name.Equals(place, System.StringComparison.InvariantCultureIgnoreCase));

        if (zone == this.Zone && go)
        {
            blackout.enabled = true;
            blur.enabled = true;

            int started = 0;

            Go.addTween(new GoTween(blackout, 0.1f, new GoTweenConfig()
                .floatProp("RampOffset", -.3f)
                .setDelay(0.2f)
                .setEaseType(GoEaseType.Linear),
                (tween) => started--));
            started++;

            Go.addTween(new GoTween(blur, 0.3f, new GoTweenConfig()
                .floatProp("BlurSpread", 1)
                .intProp("Iterations", 10)
                .setEaseType(GoEaseType.Linear),
                (tween) => started--));
            started++;

            yield return new WaitUntil(() => started == 0);

            var pos = camera.transform.position;
            pos.x = go.transform.position.x;
            pos.y = go.transform.position.y;
            camera.transform.position = pos;
            
            Go.addTween(new GoTween(blackout, 0.1f, new GoTweenConfig()
                .floatProp("RampOffset", 0)
                .setEaseType(GoEaseType.Linear),
                (tween) => started--));
            started++;

            Go.addTween(new GoTween(blur, 0.3f, new GoTweenConfig()
                .floatProp("BlurSpread", 0)
                .intProp("Iterations", 0)
                .setEaseType(GoEaseType.Linear),
                (tween) => started--));
            started++;

            yield return new WaitUntil(() => started == 0);

            blackout.enabled = false;
            blur.enabled = false;
        }
        else
        {
            yield return null;
        }
    }
}
