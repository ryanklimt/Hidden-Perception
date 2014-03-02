function Awake() {
    var gameMusic : GameObject = GameObject.Find("GameMusic");
    if (gameMusic) {
        Destroy(gameMusic);
    }
    DontDestroyOnLoad(gameObject);
}