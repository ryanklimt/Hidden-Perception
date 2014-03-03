using UnityEngine;
using System.IO;
using System;
using System.Collections;

public class GUIMenu : MonoBehaviour {

	private int currentProgress;
	private int maxLevels = 20;

	void Start() {
		OnLoad();
	}

	void OnLoad() {
		if(File.Exists(Application.dataPath + "/" + "LevelSave.txt")) {
			var sr = new StreamReader(Application.dataPath + "/" + "LevelSave.txt");
			var fileContents = sr.ReadToEnd();
			sr.Close();
			currentProgress = int.Parse(fileContents.Split("\n"[0])[0]);
		} else {
			currentProgress = 1;
		}
	}


	void OnGUI() {
		GUI.Box(new Rect(5,5,90,Math.Min(currentProgress, maxLevels) * 30 + 20), "Main Menu");

		int i = 1;
		float yOffset = 0f;
		while(i <= currentProgress && i <= maxLevels) {
			if(GUI.Button(new Rect(10,25 + yOffset,80,20), "Level " + i)) {
				Camera.main.SendMessage("fadeOut");
				Application.LoadLevel("Level " + i);
			}
			i++;
			yOffset += 30;
		}

	}
}