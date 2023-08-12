using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caminar : MonoBehaviour
{
    private Rigidbody2D rbdy2D;

    [Header("Movimiento")]
    private float movimientoHorizontal = 0f;
    [SerializeField] private float velocidadDeMovimiento;

    [SerializeField] private float suavizadoDeMovimiento;

    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;

    [Header("Suelo")]
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private LayerMask esSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;

    private bool salto = false;


    private void Start()
    {
        rbdy2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;
        if (Input.GetButtonDown("Jump"))
        {
            salto = true;
        }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, esSuelo);
        //Aqui haremos el movimiento
        Mover(movimientoHorizontal * Time.deltaTime, salto);
        salto = false;
    }

    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjeto = new Vector2(mover, rbdy2D.velocity.y);
        rbdy2D.velocity = Vector3.SmoothDamp(rbdy2D.velocity, velocidadObjeto, ref velocidad, suavizadoDeMovimiento);

        if (mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            Girar();
        }

        if(enSuelo && saltar)
        {
            enSuelo = false;
            rbdy2D.AddForce(new Vector2(0f, fuerzaSalto));
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;

        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}
