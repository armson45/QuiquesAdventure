using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulFollow : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Transform Quiquerman;
    [SerializeField] private float minDistance;
    [SerializeField] private float attackDistance;
    private int DamageAmount = 3;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distanciaAlJugador = Vector2.Distance(transform.position, Quiquerman.position);

        if (distanciaAlJugador < minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Quiquerman.position, velocidad * Time.deltaTime);
            if(distanciaAlJugador < attackDistance)
            {
                //Debug.Log("HA LLEGADOOOO
                Quiquerman.GetComponent<scriptTodo>().GetDamage(DamageAmount);
            }
        }
    }
}
