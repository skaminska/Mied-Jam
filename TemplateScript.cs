using System.Collections;
using UnityEngine;

public class TemplateScript : MonoBehaviour
{
    [SerializeField] public GameObject finalObject;
    [SerializeField] LayerMask buildingLayer;
    [SerializeField] bool isBig;

    Vector3 right, top, diagonal;
    bool build;

    private void Start()
    {
        right = new Vector3(1, 0);
        top = new Vector3(0, 1);
        diagonal = new Vector3(1, 1);
        build = false;
    }

    void Update()
    {
        if(GameManager.Instance.GetMousePosition().x > -20 && GameManager.Instance.GetMousePosition().x < 20 && GameManager.Instance.GetMousePosition().y > -14 && GameManager.Instance.GetMousePosition().y < 13)
            transform.position = GameManager.Instance.GetMousePosition();

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(GameManager.Instance.GetMousePosition(), Vector2.zero, Mathf.Infinity);

            if(!GameManager.Instance.buildingsPositions.Contains(transform.position) && !isBig)
            {

                GameObject tmp = Instantiate(finalObject, transform.position, Quaternion.identity);
                GameManager.Instance.buildingsPositions.Add(transform.position);

                GameManager.Instance.buildMode = false;
                Destroy(gameObject);
            }
            else if(!GameManager.Instance.buildingsPositions.Contains(transform.position) && !GameManager.Instance.buildingsPositions.Contains(transform.position + top) && !GameManager.Instance.buildingsPositions.Contains(transform.position + right) && !GameManager.Instance.buildingsPositions.Contains(transform.position + diagonal))
            {
                Instantiate(finalObject, transform.position, Quaternion.identity);
                GameManager.Instance.buildingsPositions.Add(transform.position);
                GameManager.Instance.buildingsPositions.Add(transform.position + right);
                GameManager.Instance.buildingsPositions.Add(transform.position + top);
                GameManager.Instance.buildingsPositions.Add(transform.position + diagonal);
                GameManager.Instance.buildMode = false;
                Destroy(gameObject);
            }
        }
    }
}
