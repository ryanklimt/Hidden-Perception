﻿var bgColor : Color;    
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
        while( t < 1.0 ){
            Camera.main.backgroundColor = Color.Lerp(currentColor, bgColor, t );
            yield WaitForSeconds(.075);
            t += Time.deltaTime;
        }
    }
}