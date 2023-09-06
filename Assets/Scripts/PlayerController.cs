using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerI;
    private InputAction move;
    private InputAction restart;
    private InputAction quit;
    private InputAction launch;

    [SerializeField] private float paddleSpeed;
    private bool paddleShouldBeMoving;
    [SerializeField] private GameObject paddle;
    private float moveDirection;

    [SerializeField] BallController ballController;

    public bool canReceiveInput;

    // Start is called before the first frame update
    void Start()
    {
        playerI.currentActionMap.Enable();

        move = playerI.currentActionMap.FindAction("MovePaddle");
        restart = playerI.currentActionMap.FindAction("RestartGame");
        quit = playerI.currentActionMap.FindAction("QuitGame");
        launch = playerI.currentActionMap.FindAction("Launch");

        move.started += Handle_MoveStarted;
        restart.performed += Handle_RestartPerformed;
        quit.performed += Handle_QuitPerformed;
        move.canceled += Handle_MoveCanceled;
        launch.performed += Handle_LaunchPerformed;

        paddleShouldBeMoving = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(paddleShouldBeMoving)
        {
            moveDirection = move.ReadValue<float>();
            paddle.GetComponent<Rigidbody2D>().velocity = new Vector2(paddleSpeed * moveDirection, 0);
        }
        else
        {
            paddle.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void Handle_MoveStarted(InputAction.CallbackContext obj)
    {
        if (canReceiveInput)
        {
            paddleShouldBeMoving = true;
        }
    }

    private void Handle_RestartPerformed(InputAction.CallbackContext obj)
    {
        print("Handled Restart Performed");
        SceneManager.LoadScene(0);
    }

    private void Handle_QuitPerformed(InputAction.CallbackContext obj)
    {
        print("Handled Quit Performed");
        Application.Quit();
    }

    private void Handle_MoveCanceled(InputAction.CallbackContext obj)
    {
        paddleShouldBeMoving = false;
    }

    private void Handle_LaunchPerformed(InputAction.CallbackContext obj)
    {
        if (canReceiveInput)
        {
            ballController.Launch();
        }
    }

    public void OnDestroy()
    {
        move.started -= Handle_MoveStarted;
        restart.performed -= Handle_RestartPerformed;
        quit.performed -= Handle_QuitPerformed;
        move.canceled -= Handle_MoveCanceled;
    }
}
