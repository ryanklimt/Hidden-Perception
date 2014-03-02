using UnityEngine;
using System.IO;
using System;
using System.Collections;

public class GUIMenu : MonoBehaviour {

	private int currentProgress;
	private int maxLevels = 10;

	void Start() {
		OnLoad();
	}

	void OnLoad() {
		var sr = new StreamReader(Application.dataPath + "/" + "LevelSave.txt");
		var fileContents = sr.ReadToEnd();
		sr.Close();
		currentProgress = int.Parse(fileContents.Split("\n"[0])[0]);
	}


	void OnGUI() {
		GUI.Box(new Rect(5,5,90,Math.Min(currentProgress, maxLevels) * 30 + 20), "Main Menu");

		int i = 1;
		float yOffset = 0f;
		while(i <= currentProgress && i <= maxLevels) {
			if(GUI.Button(new Rect(10,25 + yOffset,80,20), "Level " + i)) {
				Application.LoadLevel("Level " + i);
			}
			i++;
			yOffset += 30;
		}

	}
}