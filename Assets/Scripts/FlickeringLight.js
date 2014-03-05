var bgColor : Color;    
var looping : boolean = true;
 
function Start () {
    StartCoroutine(bgColorShifter());
}
 
function bgColorShifter(){
    while (looping){
        bgColor.r = Random.value;
        bgColor.g = Random.value;
        bgColor.b = Random.value;
        bgColor.a = 1.0;
        var t: float = 0f;
        var currentColor = Camera.main.backgroundColor;
        while( t < 5.0 ){
            Camera.main.backgroundColor = Color.Lerp(currentColor, bgColor, t );
            yield WaitForSeconds(.03);
            t += Time.deltaTime;
        }
    }
}

function Update() {
	if (Input.GetKeyDown ("m")) {
		GameObject.Find("GameMusic").audio.mute = !GameObject.Find("GameMusic").audio.mute;
	}
}