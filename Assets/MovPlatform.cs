using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPlatform : MonoBehaviour
{
    public GameObject plataforma;
    public Transform StartPoint;
    public Transform SecondPoint;
    public Transform ThirdPoint;
    public Transform EndPoint;

    public float velocidad;
    private Vector3 MoverHacia;

    void Start()
    {
        MoverHacia = EndPoint.position;
    }

    void Update()
    {
        plataforma.transform.position = Vector3.MoveTowards(plataforma.transform.position, MoverHacia, velocidad * Time.deltaTime);

        if (plataforma.transform.position == StartPoint.position)
        {
            MoverHacia = SecondPoint.position;
        }
        if (plataforma.transform.position == SecondPoint.position)
        {
            MoverHacia = ThirdPoint.position;
        }
        if (plataforma.transform.position == ThirdPoint.position)
        {
            MoverHacia = EndPoint.position;
        }
        if (plataforma.transform.position == EndPoint.position)
        {
            MoverHacia = StartPoint.position;
        }
    }
}
