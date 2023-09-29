using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class scriptTodo : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Animator quiquerun;
    private Rigidbody2D quiquermanPlayer;
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
    public bool invencible = false;
    public float tiempoInvencible = 1f;
    private SpriteRenderer quiquerman;
    private Color colorNormal;
    //public int enemyDamage = 0;

    // Start is called before the first frame update
    void Start()
    {
        quiquermanPlayer = GetComponent<Rigidbody2D>();
        quiquerun = GetComponent<Animator>();
        quiquerman = GetComponent<SpriteRenderer>();
        colorNormal = quiquerman.color;
    }

    // Update is called once per frame
    void Update()
    {
        //si el valor de la barra de vida del jugador es menor o igual a 10
        //se destruira el personaje(muerte)
        if (barraDeVida.value <= 10)
        {
            Destroy(quiquermanPlayer);
        }

        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;
        if (Input.GetButtonDown("Jump"))
        {
            salto = true;
        }
        quiquerun.SetFloat("VelMovimiento", Mathf.Abs(movimientoHorizontal));
    }


    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjeto = new Vector2(mover, quiquermanPlayer.velocity.y);
        quiquermanPlayer.velocity = Vector3.SmoothDamp(quiquermanPlayer.velocity, velocidadObjeto, ref velocidad, suavizadoDeMovimiento);

        if (mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            Girar();
        }

        if (enSuelo && saltar)
        {
            enSuelo = false;
            quiquermanPlayer.AddForce(new Vector2(0f, fuerzaSalto));
        }

    }
    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, esSuelo);
        //Aqui haremos el movimiento
        Mover(movimientoHorizontal * Time.deltaTime, salto);
        salto = false;
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
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //se le resta a la barra de vida el valor de enemyDamage
        if (collision.gameObject.tag == "PlataformaMovible")
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaMovible")
        {
            transform.parent = null;
        }
    }

    private void RestaurarColor()
    {
        Debug.Log("Restaurando color");
        quiquerman.color = colorNormal;
    }


    public void GetDamage(int damage)
    {
        if (!invencible && barraDeVida.value > 0)
        {
            //estableciendo el color
            Color colorDamage = new Color(255f / 255f, 100f / 255f, 100f / 255f);
            quiquerman.color = colorDamage;
            barraDeVida.value -= damage;
            StartCoroutine(Invulnerabilidad());
        }
        //estableciendo el color normal
        //Color colorNormal = new Color(255f / 255f, 255f / 255f, 255f / 255f);
        Invoke("RestaurarColor", 0.3f);
    }

    IEnumerator Invulnerabilidad()
    {
        invencible = true;
        yield return new WaitForSeconds(tiempoInvencible);
        invencible = false;
    }
}