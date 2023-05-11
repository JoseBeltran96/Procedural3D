using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

public class controlEnemigo : MonoBehaviour
{
    /*
    [Header("Estadisticas")]
    public int vidasActual;
    public int vidasMax;
    public int puntuacionEnemigo;
    */

    [Header("Movimiento")]
    public float velocidad;
    public float rangoAtaque;
    public float yPathOffset;
    public bool siemprePersigue;
    public float rangoPerseguir;

    private List<Vector3> listaCaminos;
    private playerController objetivo;

    private void Start()
    {
        objetivo = FindObjectOfType<playerController>();
        //vidasActual = vidasMax;

        //Cada medio segundo repite la creacion de la lista de caminos.
        InvokeRepeating("actualizarCaminos", 0.0f, 0.5f);
    }

    private void Update()
    {
        //Distacia entre enemigo y jugador.
        float distancia = Vector3.Distance(transform.position, objetivo.transform.position);

        //Nos persigue si el rango de ataque es mayor al establecido.
        if (distancia > rangoAtaque)
        {
            if (siemprePersigue)
            {
                perseguirObjetivo();
            }
            else if (distancia < rangoPerseguir)
            {
                perseguirObjetivo();
            }
        }


        //Rota el enemigo para que mire en direccion al jugador.
        Vector3 direccion = (objetivo.transform.position - transform.position).normalized;
        float angulo = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.up * angulo;

    }

    /*
    public void quitarVidaEnemigo(int cantidad)
    {
        vidasActual -= cantidad;

        if (vidasActual <= 0)
        {
            Destroy(gameObject);
        }
    }
    */

    private void perseguirObjetivo()
    {
        if (listaCaminos.Count == 0)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, listaCaminos[0] + new Vector3(0, yPathOffset, 0), velocidad * Time.deltaTime);

        if (transform.position == listaCaminos[0] + new Vector3(0, yPathOffset, 0))
        {
            listaCaminos.RemoveAt(0);
        }
    }

    void actualizarCaminos()
    {
        NavMeshPath caminoCalculado = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, objetivo.transform.position,NavMesh.AllAreas, caminoCalculado);

        listaCaminos = caminoCalculado.corners.ToList();
    }
}
