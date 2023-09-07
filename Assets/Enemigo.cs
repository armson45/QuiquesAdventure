using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator animacion;
    public int direccion;
    public float speed_walk;
    public float speed_run;
    public GameObject objetivo;
    public bool atacando;


    // Start is called before the first frame update
    void Start()
    {
        animacion = GetComponent<Animator>();
        objetivo = GameObject.Find("john");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Comportamiento()
    {
        animacion.SetBool("caminar_anim", false);
        cronometro += 1 * Time.deltaTime;
        if (cronometro >= 4)
        {
            rutina = UnityEngine.Random.Range(0, 2);
            cronometro = 0;
        }

        switch(rutina)
        {
            case 0:
                animacion.SetBool("caminar_anim", false );
                break;
        }
    }
}
