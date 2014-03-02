using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject player;
	private GameObject currentPlayer;
	private GameCamera cam;
	private Vector3 checkpoint;

	public static int levelCount = 10;
	public static int currentLevel = 1;

	void Start () {
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
			Application.LoadLevel("Level " + currentLevel);
		}
	}

	public void SetCheckpoint(Vector3 cp) {
		checkpoint = cp;
	}

	public void EndLevel() {
		if (currentLevel < levelCount) {
			currentLevel++;
			Application.LoadLevel("Level " + currentLevel);
		}
		else {
			Application.LoadLevel ("MainMenu");
		}
	}
}
