using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPa : MonoBehaviour
{

    [SerializeField] private float velocidad;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private float distancia;
    [SerializeField] private bool moviendoDerecha;
    private Rigidbody2D enemigo;


    void Start()
    {
        //aqui se hace referencia a la propiedad rigidbody que tiene el enemigo
        enemigo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D haySuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia);
        enemigo.velocity = new Vector2(velocidad, enemigo.velocity.y);

        if (haySuelo == false)
        {
            //girar
            Girar();
        }
    }

    private void Girar()
    {
        moviendoDerecha = !moviendoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);
    }
}
