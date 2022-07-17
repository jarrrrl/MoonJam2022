public enum GameState
{
    GamePlay,
    Paused
}


//SUBSCRIPTION TO PAUSE EVENT

//subscribe to event

// void Awake()
// {
//     GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
// }




// in this event you need to reference: 
//rigidbody's "simulated" if exists,
//animator's "enabled" if exists,
//all script's "enabled" of this gameobject and its childen (so you don't have to subscribe in every other script)

// private void OnGameStateChanged(GameState newGameState)
// {
//     bool stateSwitch = newGameState == GameState.GamePlay;
//     enabled = stateSwitch;
//     gameObject.GetComponent<Rigidbody2D>().simulated = stateSwitch;
//     gameObject.GetComponent<HealthController>().enabled = stateSwitch;
//     gameObject.GetComponentInChildren<AttackController>().enabled = stateSwitch;
// }




//when destroyed, unsubscribe from event

// private void OnDestroy(){
//     GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
// }