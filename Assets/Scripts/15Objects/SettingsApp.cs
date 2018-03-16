using AssetPackage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RAGE.Analytics;
using RAGE.Net;

public class SettingsApp : MonoBehaviour
{

    public GameObject confirmExitPanel;
    public bool forceExit = false;
    public UnityEngine.UI.Text infoPanel;

    public string dataFile;

    // Use this for initialization
    void Start()
    {
        dataFile = Application.persistentDataPath;
        if (!dataFile.EndsWith("/"))
        {
            dataFile += "/";
        }
        dataFile += "tracesRaw.csv";

        if (confirmExitPanel)
        {
            confirmExitPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ExitButton()
    {
        if (confirmExitPanel)
        {
            confirmExitPanel.SetActive(true);
        }
        else
        {
            ExitGameConfirmed();
        }
    }

    public void ExitGameConfirmed()
    {
        //Tracker.T.RequestFlush();
        //Application.Quit();
        if (PlayerPrefs.HasKey("LimesurveyToken") && PlayerPrefs.GetString("LimesurveyToken") != "ADMIN" && PlayerPrefs.HasKey("LimesurveyPost"))
        {
            try
            {
                string path = Application.persistentDataPath;

                if (!path.EndsWith("/"))
                {
                    path += "/";
                }

                Dictionary<string, string> headers = new Dictionary<string, string>();

                Net net = new Net(this);

                WWWForm data = new WWWForm();

                data.AddField("token", PlayerPrefs.GetString("username"));

                string traces = System.IO.File.ReadAllText(dataFile);
                data.AddBinaryData("traces", System.Text.Encoding.UTF8.GetBytes(traces));

                data.headers.Remove("Content-Type");// = "multipart/form-data";
                System.IO.File.AppendAllText(path + PlayerPrefs.GetString("username") + ".csv", System.IO.File.ReadAllText(dataFile));

                net.POST(PlayerPrefs.GetString("LimesurveyHost") + "classes/collector", data, new SavedTracesListener(this, this.infoPanel));
                PlayerPrefs.SetString("CurrentSurvey", "post");
                /*
				if (PlayerPrefs.GetString("CurrentSurvey").Equals("post")) {
					PlayerPrefs.SetString("CurrentSurvey", "end");
				}*/
            }
            catch (Exception e)
            {
                ChangeLevel();
                Debug.LogError(e);
            }
        }
        else
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
        }
    }

    void ChangeLevel()
    {
        if (PlayerPrefs.GetString("CurrentSurvey").Equals("end") || forceExit)
        {
            if (Application.isEditor == false)
            {
                Application.Quit();
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }
        else if (PlayerPrefs.GetString("CurrentSurvey").Equals("post"))
        {
            SceneManager.LoadScene("_Survey");
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void ExitGameCanceled()
    {
        confirmExitPanel.SetActive(false);
    }

    class SavedTracesListener : Net.IRequestListener
    {
        private UnityEngine.UI.Text infoPanel;
        private SettingsApp app;

        public SavedTracesListener(SettingsApp app, UnityEngine.UI.Text info)
        {
            this.infoPanel = info;
            this.app = app;
        }

        public void Result(string data)
        {
            app.ChangeLevel();
        }

        public void Error(string error)
        {
            if (this.infoPanel)
            {
                infoPanel.text = error;
            }
        }
    }
}
