using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayerMask;

    [SerializeField]
    private LayerMask objectLayerMask;

    [SerializeField]
    private LayerMask prohibitedLayerMask;

    private GameObject selectedObject;
    private Color originalColor;
    private bool selected;

    private void Start()
    {
        selectedObject = null;
        originalColor = Color.white;
        selected = false;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (selected)
            {
                if (!Physics.Raycast(ray, float.MaxValue, prohibitedLayerMask))
                {
                    if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, groundLayerMask))
                    {
                        selectedObject.transform.position = new Vector3(hit.point.x, selectedObject.transform.position.y, hit.point.z);
                        selectedObject.GetComponent<Renderer>().material.color = originalColor;
                        selected = false;
                    }
                }
            }
            else if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, objectLayerMask))
            {
                selectedObject = hit.collider.gameObject;
                originalColor = selectedObject.GetComponent<Renderer>().material.color;
                selectedObject.GetComponent<Renderer>().material.color = Color.yellow;
                selected = true;
            }
        }
    }

    /*
    private float force = 5;

    private void LaunchIntoAir(Rigidbody rb)
    {
        rb.AddForce(rb.transform.up * force, ForceMode.Impulse);
    }
    */
}
