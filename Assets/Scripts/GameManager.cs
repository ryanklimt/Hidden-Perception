using UnityEngine;
using System.IO;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject player;
	private GameObject currentPlayer;
	private GameCamera cam;
	private Vector3 checkpoint;

	public static int levelCount = 10;
	public static int currentLevel = 1;
	public static int currentProgress;

	public static int deathCount = 0;

	void Start () {

		OnLoad();

		var currLevel = int.Parse(Application.loadedLevelName.Split(' ')[Application.loadedLevelName.Split(' ').Length-1]);
		if (currLevel <= levelCount) currentLevel = currLevel;

		cam = GetComponent<GameCamera>();
		if (GameObject.FindGameObjectWithTag("Spawn")) {
			checkpoint = GameObject.FindGameObjectWithTag("Spawn").transform.position;
		}

		SpawnPlayer(checkpoint);
	}
	
	// Spawn player
	private void SpawnPlayer(Vector3 spawnPos) {
		currentPlayer = Instantiate(player,spawnPos,Quaternion.Euler(0, 180, 0)) as GameObject;
		cam.SetTarget(currentPlayer.transform);
	}

	private void Update() {
		if (!currentPlayer) {
			if (Input.GetButtonDown("Respawn")) {
				SpawnPlayer(checkpoint);
			}
		}
		if (GameObject.FindGameObjectWithTag("Player").transform.position.y < -50) {
			deathCount++;
			Application.LoadLevel("Level " + currentLevel);
		}
		if(Input.GetButtonDown("Escape")) {
			Application.LoadLevel("MainMenu");
		}
	}

	public void SetCheckpoint(Vector3 cp) {
		checkpoint = cp;
	}

	public void EndLevel() {
		if (currentLevel >= currentProgress) {
			currentProgress = currentLevel + 1;
		}
		OnSave(currentProgress);
		if (currentLevel < levelCount) {
			currentLevel++;
			Application.LoadLevel("Level " + currentLevel);
		}
		else {
			Application.LoadLevel("MainMenu");
		}
	}

	public void OnSave(int currentLevel) {
		StreamWriter sr = new StreamWriter(Application.dataPath + "/" + "LevelSave.txt");
		sr.Write(currentLevel);
		sr.Close();
	}

	public void OnLoad() {
		var sr = new StreamReader(Application.dataPath + "/" + "LevelSave.txt");
		var fileContents = sr.ReadToEnd();
		sr.Close();
		currentProgress = int.Parse(fileContents.Split("\n"[0])[0]);
	}
	
	void OnGUI() {
		GUI.Box (new Rect (Screen.width - 100,0,100,40), "Deaths: " + deathCount);
		GUI.Box (new Rect (Screen.width - 100,40,100,40), "Current Level: " + currentLevel);
		GUI.Box (new Rect (Screen.width - 100,80,100,40), "Max Level: " + currentProgress);
	}
}
