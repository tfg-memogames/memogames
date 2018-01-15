using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum SequenceNodeType {SMS, DIALOG, ACTION};

namespace h4g
{
    public class SequenceNode
    {
        public SequenceNodeType type;
        public bool receive;
        public string text;
        public string talker;
        public bool raro;
        public int[] talkers;


        public SequenceNode(string action)
        {
            this.type = SequenceNodeType.ACTION;
            this.text = action;
        }

        public SequenceNode(string text, bool receive, string talker = "", bool raro = false)
        {
            this.type = SequenceNodeType.SMS;
            this.text = text;
            this.talker = talker;
            this.receive = receive;
            this.raro = raro;
        }

        public SequenceNode(string text, int[] talkers)
        {
            this.type = SequenceNodeType.DIALOG;
            this.text = text;
            this.talkers = talkers;
        }
    }

    public class SequenceManager : MonoBehaviour
    {
        static public SequenceManager S;
        public GameObject bola;
        public AudioClip laugh;
        public AudioClip laughs;
        public AudioClip teacherSteps;
        public AudioSource efxSource;
        public Sprite bg;

        public List<SequenceNode> current_sequence;

        void Awake()
        {
            S = this;
            this.current_sequence = null;
        }

        void Start()
        {
        }

        // Update is called once per frame


        private float msg_time = 2f;
        private float time_since_last_msg = 0;

        bool eyes_closed = false;
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (this.current_sequence != null && this.current_sequence.Count > 0)
                {
                    SequenceNode current = current_sequence[0];
                    current_sequence.Remove(current);
                    runNode(current);
                }
                else
                    Chat.S.fade_out(GameObject.Find("content"));
            }

            if (eyes_closed)
            {
                time_since_last_msg += Time.deltaTime;
                if (time_since_last_msg > msg_time)
                {
                    time_since_last_msg = 0;
                }
            }
        }

        void runNode(SequenceNode sn)
        {
            switch (sn.type)
            {
                case SequenceNodeType.SMS:
                    if (!sn.raro)
                    {
                        if (sn.receive)
                        {
                            Chat.S.receiveMessage(sn.text, sn.talker);
                        }
                        else {
                            Chat.S.sendMessage(sn.text);
                        }
                    }
                    else {
                        switch (sn.text)
                        {
                            case "amistad":
                                Chat.S.mensajeAmistad(sn.talker);
                                break;
                            case "borrar":
                                Chat.S.mensajeBorrar();
                                break;
                            case "notificacion":
                                Chat.S.mensajeAlerta(sn.talker);
                                break;
                        }

                    }
                    break;
                case SequenceNodeType.DIALOG:
                    Chat.S.talk(sn.talkers, sn.text);
                    break;
                case SequenceNodeType.ACTION:
                    switch (sn.text)
                    {
                        case "Sleep":
                            closeEyes();
                            break;
                        case "Laugh":
                            ReproduceSound(laugh);
                            break;
                        case "Laughs":
                            ReproduceSound(laughs);
                            break;
                        case "Teacher":
                            EnterTeacher(teacherSteps);
                            break;
                        case "Scene":
                            ChangeBg();
                            break;
                        case "Position":
                            ChangeBg();
                            break;
                        case "Transition":
                            loadTransition();
                            break;
                        default: break;
                    }
                    break;
            }
        }

        void closeEyes()
        {
            GameObject.Find("Eyes").GetComponent<Sleep>().sleep();
            eyes_closed = true;
        }

        void ReproduceSound(AudioClip clip)
        {
            efxSource.clip = clip;

            efxSource.Play();
        }

        void EnterTeacher(AudioClip clip)
        {
            efxSource.clip = clip;
            efxSource.Play();
        }

        void ChangeBg()
        {
            GameObject.Find("BgImage").GetComponent<SpriteRenderer>().sprite = bg;
        }

        void loadTransition()
        {
            SceneManager.LoadScene("transition");
        }
    }
}