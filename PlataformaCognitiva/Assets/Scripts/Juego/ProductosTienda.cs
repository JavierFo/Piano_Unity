using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductosTienda : MonoBehaviour
{
    public GameObject [] productos;
    public Transform[] posiciones;
    List<GameObject> productosMostrados = new List<GameObject>();
    List<GameObject> productosComprados = new List<GameObject>();
    Juego j;

    private void Awake()
    {
        for (int i = 0; i < productos.Length; i++)
        {
            productos[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {        
        j = transform.parent.GetComponentInChildren<Juego>();  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && productosComprados.Count < j.productosFaltantes)
        {
            int layerMask = 1 << 8;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                productosComprados.Add(hit.collider.gameObject);
                hit.collider.gameObject.SetActive(false);
            }
        }
    }

    public List<GameObject> GetListaProductosComprados()
    {
        return productosComprados;
    }
    public int GetCuentaLPC()
    {
        return productosComprados.Count;
    }

    public void SetTienda(List<GameObject> productosFaltantes)
    {
        //Ordena aletoriamente los productos
        for (int i = 0; i < productos.Length; i++)
        {
            GameObject tempGO = productos[i];
            int randomIndex = Random.Range(i, productos.Length);
            productos[i] = productos[randomIndex];
            productos[randomIndex] = tempGO;
        }

        //Ordena aleatoriamente las posiciones
        for (int i = 0; i < posiciones.Length; i++)
        {
            Transform tempT = posiciones[i];
            int randomIndex = Random.Range(i, posiciones.Length);
            posiciones[i] = posiciones[randomIndex];
            posiciones[randomIndex] = tempT;
        }

        //Selecciona (n - productos faltantes) y los agrega a la lista de productos mostrados
        for (int a = 0; a < productosFaltantes.Count; a++)
        {
            for (int i = 0; i < productos.Length; i++)
            {
                if (productos[i].tag != productosFaltantes[a].tag && productosMostrados.Count < (j.productosMostrados - j.productosFaltantes))
                {
                    productosMostrados.Add(productos[i]);
                }
                /*
                else if (productos[i].tag == productosFaltantes[a].tag)
                {
                    productosMostrados.Add(productos[i]);
                }
                */
            }
        }

        //Selecciona los productos faltantes de la lista de productos y los agrega a la lista de productos mostrados
        for (int a = 0; a < productosFaltantes.Count; a++)
        {
            for (int i = 0; i < productos.Length; i++)
            {
                if (productos[i].tag == productosFaltantes[a].tag)
                {
                    productosMostrados.Add(productos[i]);
                }
            }
        }
        
        Debug.Log(productosMostrados.Count);

        //Pone activos los primeros n prodcutos que se deben mostrar y les asigna una posicion
        for (int i = 0; i < productosMostrados.Count; i++)
        {
            productosMostrados[i].SetActive(true);
            productosMostrados[i].transform.position = posiciones[i].position;
        }
    }
}
