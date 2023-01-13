using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float speedEnemy;
    [SerializeField]
    private float speedDown = 10;
    [SerializeField]
    private int winner = 0;

    
    private Vector2 facingRight;
    private Vector2 facingLeft;

    private int passos = 0;
    private float down;
    public float Down { get => down; set => down = value; }
    private float chao = -4.1f;
    private float enemyGoal = 7f;
   
    private float window;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        facingRight = transform.localScale;
        facingLeft = transform.localScale;
        facingLeft.x = facingLeft.x * -1;

    }

    private void FixedUpdate()
    {
        moveEnemy();
    }
 
    private void moveEnemy()
    {
        switch (passos)
        {
            // Inimigo entra na tela e segue para direita, ao chegar a posição de queda, Case1;
            case 0:
                transform.localScale = facingLeft;
                rb.MovePosition(rb.position + new Vector2(speedEnemy * Time.deltaTime, 0));


                if (rb.position.x >= Down)
                {
                    rb.MovePosition(new Vector2(Down, rb.position.y));

                    passos = 1;
                }
                break;

            // Inimigo faz o movimento de cair, ao chegar no chão e chama Case2;
            case 1:
                rb.MovePosition(rb.position + new Vector2(0, speedDown * Time.deltaTime * -1));
                if (rb.position.y <= chao)
                {
                    rb.MovePosition(new Vector2(rb.position.x, chao));
                    passos = 2;
                }
                break;

            // Inimigo se move para direita novamente, ao chegar em seu objetivo, o contador de inimigos sobe, e chama Case3;
            case 2:
                rb.MovePosition(rb.position + new Vector2(speedEnemy * Time.deltaTime, 0));
                
                if (rb.position.x >= enemyGoal)
                {
                    rb.MovePosition(new Vector2(enemyGoal, rb.position.y));

                    GameController.Instance.EnemyCounter++;
                    winner = GameController.Instance.EnemyCounter;
                    transform.localScale = facingRight;

                    switch (winner)
                    {
                        case 1:
                            window = -3.5f;
                            break;
                        case 2:
                            window = -1.5f;
                            break;
                        case 3:
                            window = 0.5f;
                            break;
                        case 4:
                            window = 2.5f;
                            break;
                        case 5:
                            window = 3.6f;
                            break;
                        default:
                            break;
                    }

                    passos = 3;
                }
                break;

            // Nesse novo switch criamos o novo valor de window, baseado em quantos inimigos já chegaram no objetivo, depois disso chama case 4;
            case 3:

                rb.MovePosition(rb.position + new Vector2(0, speedDown * Time.deltaTime * 1));
                if (rb.position.y >= window)
                {
                    rb.MovePosition(new Vector2(enemyGoal, window));

                    if (winner == 5)
                    {
                        passos = 5;
                    }
                    else
                    {
                        passos = 4;
                    }
                }
                break;
            case 4:

                break;

            case 5:
                transform.localScale = facingLeft;
                rb.MovePosition(rb.position + new Vector2(speedEnemy * Time.deltaTime * 0.5f, 0));
                GameController.Instance.gameOver.SetActive(true);
                Time.timeScale = 0;
                break;
            default:
                break;

        }
    }
}
