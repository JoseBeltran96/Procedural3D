using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public LayerMask mascaraSuelo;

    void Start()
    {
        // Raycast hacia arriba y alrededor, desactivamos el renderer si hay otra caja
        if (Physics.Raycast(transform.position, transform.up, 1, mascaraSuelo) &&
            Physics.Raycast(transform.position, transform.forward, 1, mascaraSuelo) &&
            Physics.Raycast(transform.position, transform.forward * -1, 1, mascaraSuelo) &&
            Physics.Raycast(transform.position, transform.right, 1, mascaraSuelo) &&
            Physics.Raycast(transform.position, transform.right * -1, 1, mascaraSuelo))
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void actualizaEntorno()
    {
        Ray abajo = new Ray(transform.position, transform.up *- 1);
        RaycastHit HitAbajo;
        if (Physics.Raycast(abajo, out HitAbajo, 1, mascaraSuelo))
        {
            HitAbajo.collider.GetComponent<MeshRenderer>().enabled = true;

        }

        Ray Delante = new Ray(transform.position, transform.forward * 1);
        RaycastHit HitDelante;
        if (Physics.Raycast(Delante, out HitDelante, 1, mascaraSuelo))
        {
            HitDelante.collider.GetComponent<MeshRenderer>().enabled = true;

        }

        Ray Detras = new Ray(transform.position, transform.forward *- 1);
        RaycastHit HitDetras;
        if (Physics.Raycast(Detras, out HitDetras, 1, mascaraSuelo))
        {
            HitDetras.collider.GetComponent<MeshRenderer>().enabled = true;

        }

        Ray Derecha = new Ray(transform.position, transform.right * 1);
        RaycastHit HitDerecha;
        if (Physics.Raycast(Derecha, out HitDerecha, 1, mascaraSuelo))
        {
            HitDerecha.collider.GetComponent<MeshRenderer>().enabled = true;

        }

        Ray Izquierda = new Ray(transform.position, transform.right *- 1);
        RaycastHit HitIzq;
        if (Physics.Raycast(Izquierda, out HitIzq, 1, mascaraSuelo))
        {
            HitIzq.collider.GetComponent<MeshRenderer>().enabled = true;

        }

        Destroy(gameObject, 0.2f);
    }

}
