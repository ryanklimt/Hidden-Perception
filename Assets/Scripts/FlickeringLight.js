var FlashingLight: Light;
FlashingLight.enabled = true;

function FixedUpdate() {
	var RandomNumber = Random.value;

	if(RandomNumber <= .7) {
		FlashingLight.enabled = true;
	} else {
		FlashingLight.enabled = false;
	}

}