using UnityEngine;
using System.IO;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject player;
	private GameObject currentPlayer;
	private GameCamera cam;
	private Vector3 checkpoint;

	public static int levelCount = 20;
	public static int currentLevel = 1;
	public static int currentProgress;

	public static int deathCount = 0;

	private Color bgColor;
	private bool blooping = true;

	void Start () {

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
		if (Input.GetButtonDown("Respawn") || GameObject.FindGameObjectWithTag("Player").transform.position.y < -50) {
			deathCount++;
			GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.FindGameObjectWithTag("Spawn").transform.position;
		}
		if(Input.GetButtonDown("Escape")) {
			Camera.main.SendMessage("fadeOut");
			Application.LoadLevel("MainMenu");
		}

		// Zoom in camera ScrollWheel
		Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel");
		if (Camera.main.orthographicSize <= 3) {
			Camera.main.orthographicSize = 3;
		}
		if (Camera.main.orthographicSize >= 25) {
			Camera.main.orthographicSize = 25;
		}
		if (Input.GetMouseButtonDown (2)) {
			if(Camera.main.orthographicSize >= 20) {
				Camera.main.orthographicSize = 6;
			} else {
				Camera.main.orthographicSize = 25;
			}
		}
	
	}

	public void SetCheckpoint(Vector3 cp) {
		checkpoint = cp;
	}

	public void EndLevel() {
		if (currentLevel >= currentProgress) {
			currentProgress = currentLevel + 1;
			OnSave(currentProgress);
		}
		if (currentLevel < levelCount) {
			currentLevel++;
			Camera.main.SendMessage("fadeOut");
			Application.LoadLevel("Level " + currentLevel);
		} else {
			Camera.main.SendMessage("fadeOut");
			Application.LoadLevel("FinishedGame");
		}
	}

	public void OnSave(int currentLevel) {
		StreamWriter sr = new StreamWriter(Application.dataPath + "/" + "LevelSave.txt");
		sr.Write(currentLevel);
		sr.Close();
	}
	
	void OnGUI() {
		GUI.Label(new Rect (Screen.width - 50,0,100,40), "Level " + currentLevel);
	}
}
