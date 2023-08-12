
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    public GameObject playerGame;
    private Vector3 PlayerPosition;
    public float HaciaAdelante;
    public float Suavizado;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPosition = new Vector3(playerGame.transform.position.x, playerGame.transform.position.y,transform.position.z);
        //Derecha
        if(playerGame.transform.localScale.x == 1 )
        {
            PlayerPosition = new Vector3(PlayerPosition.x + HaciaAdelante, PlayerPosition.y, transform.position.z);
        }
        //Izquierda
        if (playerGame.transform.localScale.x == -1)
        {
            PlayerPosition = new Vector3(PlayerPosition.x - HaciaAdelante, PlayerPosition.y, transform.position.z);
        }

        transform.position = Vector3.Lerp(transform.position, PlayerPosition, Suavizado * Time.deltaTime);
    }
}
