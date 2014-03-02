#pragma strict

var crawling = false;

function Start () {
 	var tc = GetComponent(GUIText);
    var creds = "Thanks for playing!\n";
    creds += "Created by Ryan Klimt\n";
    creds += "Game Jam 2014\n";
    tc.text= creds;
	crawling = true;
}

function Update (){
    if (!crawling)
        return;
    transform.Translate(Vector3.up * Time.deltaTime * .1);
    if(Input.GetButtonDown("Escape") || gameObject.transform.position.y > 2) {
    	Application.LoadLevel("MainMenu");
    }
}