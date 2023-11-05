using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductosCasa : MonoBehaviour
{
    public GameObject[] productos;
    public Transform[] posiciones;
    List<GameObject> productosMostrados = new List<GameObject>();
    List<GameObject> productosFaltantes = new List<GameObject>();
    Juego j;

    private void Awake()
    {
        for (int i = 0; i < productos.Length; i++)
        {
            productos[i].SetActive(false);
        }

        j = transform.parent.GetComponentInChildren<Juego>();
        ShuffleLista();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShuffleLista()
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

        //Selecciona los n productos a mostrar
        for (int i = 0; i < j.productosMostrados; i++)
        {
            productosMostrados.Add(productos[i]);
        }

        //Pone activos los primeros n prodcutos que se deben mostrar y les asigna una posicion
        for (int i = 0; i < productosMostrados.Count; i++)
        {            
            productosMostrados[i].SetActive(true);
            productosMostrados[i].transform.position = posiciones[i].position;
        }

        //De la lista de prodcutos mostrados, se selecciona n prodcutos, que faltan
        for (int i = 0; i < j.productosFaltantes; i++)
        {
            productosFaltantes.Add(productosMostrados[i]);
            productosFaltantes[i].GetComponent<Materiales>().SetMaterial(1);
        }
    }

    public List<GameObject> GetListaProductosFaltantes()
    {
        return productosFaltantes;
    }
}
