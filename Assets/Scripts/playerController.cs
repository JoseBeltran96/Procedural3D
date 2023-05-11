using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private Vector2 ratonDelta;

    [Header("Vista Camara")]
    public Transform camara;
    public float minXVista;
    public float maxXVista;
    private float rotacionActualCamara;
    public float sensibilidadRaton;

    [Header("Movimientos")]
    public float velocidadMovimiento;
    private Vector2 movimientoActual;
    public float fuerzaSalto;
    public LayerMask capaSuelo;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        movimiento();
    }

    private void LateUpdate()
    {
        vistaCamara();
    }

    public void OnVistaInput(InputAction.CallbackContext context)
    {
        ratonDelta = context.ReadValue<Vector2>();
    }

    public void OnMovimientoInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            movimientoActual = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            movimientoActual = Vector2.zero;
        }
    }

    public void OnSaltoInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (estaEnSuelo())
            {
                rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
               
            }
        }
    }

    private void movimiento()
    {
        Vector3 direccion = transform.forward * movimientoActual.y + transform.right * movimientoActual.x;
        direccion *= velocidadMovimiento;
        direccion.y = rb.velocity.y;

        rb.velocity = direccion;
    }

    public void vistaCamara()
    {
        rotacionActualCamara += ratonDelta.y * sensibilidadRaton;
        rotacionActualCamara = Mathf.Clamp(rotacionActualCamara, minXVista, maxXVista);
        camara.localEulerAngles = new Vector3(-rotacionActualCamara, 0, 0);

        transform.eulerAngles += new Vector3(0, ratonDelta.x * sensibilidadRaton, 0);
    }

    bool estaEnSuelo()
    {
        Ray[] rayos = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (Vector3.up * 0.1f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.1f), Vector3.down),
        };

        for (int i = 0; i < rayos.Length; i++)
        {
            if (Physics.Raycast(rayos[i], 0.4f, capaSuelo))
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.1f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f) + (Vector3.up * 0.1f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.1f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.1f), Vector3.down);
    }
}
