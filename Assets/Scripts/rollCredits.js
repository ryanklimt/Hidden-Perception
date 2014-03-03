#pragma strict

var crawling = false;

function Start () {
 	var tc = GetComponent(GUIText);
    var creds = "You truly are a wizard!\n";
    creds += "Thanks for playing!\n";
    creds += "Created by Ryan Klimt\n";
    creds += "Game Jam 2014\n";
    creds += "\nSources:\n";
    creds += "http://www.dafont.com/\n";
    creds += "https://soundcloud.com/culley905\n";
    creds += "https://www.dropbox.com/s/yojhw46nr4q9548/clynos%20-%20ancient%20heroes.wav\n";
    creds += "https://mega.co.nz/#!z5RhxQIS!QyboyCYP7eGUi5hNFIZ40kgbTR4HDnW396kv837ECnY\n";
    creds += "https://www.youtube.com/watch?v=NueLZC2XWXU&list=RDtNOsY7Sv70U\n";    
    tc.text= creds;
	crawling = true;
}

function Update (){
    if (!crawling)
        return;
    transform.Translate(Vector3.up * Time.deltaTime * .1);
    if(Input.GetButtonDown("Escape") || gameObject.transform.position.y > 3) {
    	Application.LoadLevel("MainMenu");
    }
}