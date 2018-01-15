using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System;
using IsoUnity.Events;
using IsoUnity;

namespace ImportantManager
{
    public class ImportantMomentManager : EventedEventManager
    {

        public GameObject im, opts, white;
        GameObject canvas, mainworld, grayscaleworld;
        Grayscale g;
        public Sleep sleep;

        enum IMState { NONE, FLASHING, COUNTING, HIDING, HIDING2, OPTIONS }

        IMState state = IMState.NONE;
        bool tofade = false;

        [GameEvent(true, false)]
        public IEnumerator Sleep()
        {
            this.sleep.sleep();
            yield return new WaitForSeconds(1f);
        }

        [GameEvent(true, false)]
        public IEnumerator Wake()
        {
            this.sleep.wake();
            yield return new WaitForSeconds(1f);
        }

        [GameEvent(false, false)]
        public void Important(string o1, string o2, string o3, string o4)
        {
            opts.GetComponent<Options>().setText(o1, o2, o3, o4);

            this.ev = Current;
            show(); // TODO improve this code by using coroutine
        }

        // Use this for initialization
        void Start()
        {
            g = Camera.main.GetComponent<Grayscale>();
            g.enabled = false;
            canvas = GameObject.Find("Canvas");
            opts.SetActive(false);
            im.SetActive(false);
            white.SetActive(false);
        }

        // Update is called once per frame
        float flashtime = -1, waithalf = 0;
        float showtime = 4f, time_to_show = 0;
        bool up = true;
        
        void Update()
        {
            if (tofadewhite)
            {
                foreach (Transform t in mainworld.transform)
                {
                    Fade f = t.GetComponent<Fade>();
                    if (f != null && f.image != null)
                    {
                        tofade = false;
                        f.FadeOut();
                    }
                }
            }

            switch (state)
            {
                case IMState.FLASHING:
                    if (up)
                    {
                        flashtime += Time.deltaTime * 4f;
                        g.rampOffset = flashtime > 1f ? 1f : flashtime;
                        if (flashtime > 1f)
                        {
                            up = false;
                            flashtime = 1f;
                        }
                    }
                    else {
                        flashtime -= Time.deltaTime * 4f;

                        g.rampOffset = flashtime < 0f ? 0f : flashtime;
                        if (g.rampOffset == 0f)
                            state = IMState.COUNTING;
                        time_to_show = 0;
                    }
                    break;
                case IMState.COUNTING:
                    time_to_show += Time.deltaTime;
                    if (time_to_show > showtime)
                    {
                        showOptions();
                        state = IMState.OPTIONS;
                    }
                    break;
                case IMState.HIDING:
                    if (up)
                    {
                        flashtime += Time.deltaTime * 4f;
                        g.rampOffset = flashtime > 1f ? 1f : flashtime;
                        if (flashtime > 1f)
                        {
                            up = false;
                            flashtime = 1f;
                            state = IMState.HIDING2;
                            g.enabled = false;
                            white.SetActive(true);
                            white.GetComponent<Image>().CrossFadeAlpha(0f, 0.5f, false);
                        }
                    }
                    break;
                case IMState.HIDING2:
                    waithalf += Time.deltaTime;
                    if (waithalf > 0.5f)
                    {
                        state = IMState.NONE;
                        up = false;
                        Game.main.eventFinished(ev);
                        waithalf = 0;
                    }
                    break;
                default:
                    break;
            }
        }

        public void hide()
        {
            im.gameObject.SetActive(false);
            g.rampOffset = -1;
            deflash();
        }

        public void show()
        {
            g.enabled = true;
            im.gameObject.SetActive(true);
            g.rampOffset = -1;
            flash();
        }

        public void showOptions()
        {
            opts.SetActive(true);
            opts.GetComponent<Options>().fade_out_instant();
            opts.GetComponent<Options>().fade_in();
        }

        public void flash()
        {
            state = IMState.FLASHING;
            up = true;
            flashtime = -1;
        }

        public void deflash()
        {
            state = IMState.HIDING;
            up = true;
            flashtime = 0;
        }

        bool tofadewhite = false;
        public void fadewhite()
        {
            tofadewhite = true;
        }

        IGameEvent ev;
        public override void ReceiveEvent(IGameEvent ev)
        {
            base.ReceiveEvent(ev);
            if (ev.Name == "important")
            {
                string o1, o2, o3, o4;
                o1 = (string)ev.getParameter("o1");
                o2 = (string)ev.getParameter("o2");
                o3 = (string)ev.getParameter("o3");
                o4 = (string)ev.getParameter("o4");

                opts.GetComponent<Options>().setText(o1, o2, o3, o4);

                this.ev = ev;
                show();
            }
        }
    }
}