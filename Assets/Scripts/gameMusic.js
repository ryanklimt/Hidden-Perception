function Awake() {
    var menuMusic : GameObject = GameObject.Find("MenuMusic");
    if (menuMusic) {
        Destroy(menuMusic);
    }
    DontDestroyOnLoad(gameObject);
}