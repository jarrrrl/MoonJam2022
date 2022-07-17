using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class PauseController : MonoBehaviour
{
    public void MenuAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameState currentGameState = GameStateManager.Instance.CurrentGameState;
            GameState newGameState = currentGameState == GameState.GamePlay
                ? GameState.Paused
                : GameState.GamePlay;

            GameStateManager.Instance.SetState(newGameState);
        }
    }
}
