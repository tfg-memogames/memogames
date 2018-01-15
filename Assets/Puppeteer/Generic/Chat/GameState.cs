using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace h4g2
{
    public class GameState
    {
        private static GameState instance;

        public static GameState S
        {
            get
            {
                if (instance == null)
                    instance = new GameState();
                return instance;
            }
        }

        public class SceneDef
        {
            public string title;
            public string subtitle;
            public string next_scene;

            public SceneDef(string title, string subtitle, string next_scene)
            {
                this.title = title;
                this.subtitle = subtitle;
                this.next_scene = next_scene;
            }
        }

        private List<SceneDef> scenes;
        private GameState()
        {
            scenes = new List<SceneDef>();
        }

        SceneDef nextscene;

        public void setNext(string tittle, string subtittle, string next)
        {
            this.nextscene = new SceneDef(tittle, subtittle, next);
        }

        public SceneDef nextScene()
        {
            return nextscene;
        }
    }
}