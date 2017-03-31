using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class Test : MonoBehaviour {

	private const string wifePath = "Prefabs/wife";
	private const string husbandPath = "Prefabs/husband";

    private int level;
	private GameState _gs;
	private GameObject _couple;

	// Use this for initialization
	void Start () {

		this._gs = GameObject.FindObjectOfType<GameState> ();

		if (_gs.playerGender == GameState.Gender.M)
			this._couple = Resources.Load <GameObject> (wifePath);
		else
			this._couple = Resources.Load <GameObject> (husbandPath);

		Instantiate (_couple);
	}

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.level = level;
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            level = data.level;
        }
    }


}

[System.Serializable]
class PlayerData
{
    public int level;
}
