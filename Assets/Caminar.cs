using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Caminar : MonoBehaviour
{
    private Rigidbody2D QuiquePlayer;

    [Header("Movimiento")]
    private float movimientoHorizontal = 0f;
    [SerializeField] private float velocidadDeMovimiento;
    [SerializeField] private float suavizadoDeMovimiento;
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;


    [Header("Suelo")]
    private float fuerzaSalto = 175f;
    [SerializeField] private LayerMask esSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;

    private bool salto = false;

    public Slider barraDeVida;
    public int ghoulDamage = 10;


    private void Start()
    {
        QuiquePlayer = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (barraDeVida.value <= 10)
        {
            Destroy(QuiquePlayer);
        }

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
        Vector3 velocidadObjeto = new Vector2(mover, QuiquePlayer.velocity.y);
        QuiquePlayer.velocity = Vector3.SmoothDamp(QuiquePlayer.velocity, velocidadObjeto, ref velocidad, suavizadoDeMovimiento);

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
            QuiquePlayer.AddForce(new Vector2(0f, fuerzaSalto));
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
        //Gizmos.color = Color.green;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            barraDeVida.value -= ghoulDamage;
        }
    }
}