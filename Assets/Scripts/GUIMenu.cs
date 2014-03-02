using UnityEngine;
using System.Collections;

public class GUIMenu : MonoBehaviour {

	public GameObject player;
	
	void start() {
		OnGUI ();
	}


	void OnGUI() {
		GUI.Box(new Rect(5,5,90,320), "Main Menu");

		if(GUI.Button(new Rect(10,25,80,20), "Level " + 1)) {
			Application.LoadLevel("Level " + 1);
		}

		if(GUI.Button(new Rect(10,55,80,20), "Level " + 2)) {
			Application.LoadLevel("Level " + 2);
		}

		if(GUI.Button(new Rect(10,85,80,20), "Level " + 3)) {
			Application.LoadLevel("Level " + 3);
		}

		if(GUI.Button(new Rect(10,115,80,20), "Level " + 4)) {
			Application.LoadLevel("Level " + 4);
		}

		if(GUI.Button(new Rect(10,145,80,20), "Level " + 5)) {
			Application.LoadLevel("Level " + 5);
		}

		if(GUI.Button(new Rect(10,175,80,20), "Level " + 6)) {
			Application.LoadLevel("Level " + 6);
		}

		if(GUI.Button(new Rect(10,205,80,20), "Level " + 7)) {
			Application.LoadLevel("Level " + 7);
		}

		if(GUI.Button(new Rect(10,235,80,20), "Level " + 8)) {
			Application.LoadLevel("Level " + 8);
		}

		if(GUI.Button(new Rect(10,265,80,20), "Level " + 9)) {
			Application.LoadLevel("Level " + 9);
		}

		if(GUI.Button(new Rect(10,295,80,20), "Level " + 10)) {
			Application.LoadLevel("Level " + 10);
		}

	}
}