using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Picar : MonoBehaviour
{
    Camera camara;
    public LayerMask mascaraSuelo;
    private NavigationBaker nav;
    // Start is called before the first frame update
    void Start()
    {
        camara = Camera.main;
        nav = GetComponent<NavigationBaker>();
    }

    public void OnPicarInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Ray rayo = camara.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit lanzarRayo;

            if (Physics.Raycast(rayo, out lanzarRayo, 0.9f, mascaraSuelo))
            {
                if (lanzarRayo.collider.gameObject.CompareTag("cubo"))
                {

                    lanzarRayo.collider.GetComponent<Cube>().actualizaEntorno();
                    //nav.cooking();

                }
            }
        }
    }
}
