using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class Juego : MonoBehaviour
{
    public Text textoCasa, textoTienda, textoTiempo;
    public GameObject objetosCasa, objetosTienda;
    public int productosMostrados;
    public int productosFaltantes;
    public float tiempoVisualizacion;
    
    ProductosCasa pc;
    ProductosTienda pt;
    bool pausa;
    int aciertos;
    // Start is called before the first frame update
    void Start()
    {
        textoCasa.text = "Te faltan " + productosFaltantes.ToString() + " " + "productos.";
        textoTienda.text = "Puedes comprar hasta " + productosFaltantes.ToString() + " " + "productos.";
        textoTiempo.text = tiempoVisualizacion.ToString();

        textoCasa.gameObject.SetActive(true);
        textoTienda.gameObject.SetActive(false);
        textoTiempo.gameObject.SetActive(true);

        objetosCasa.SetActive(true);
        objetosTienda.SetActive(false);

        pc = transform.parent.GetComponentInChildren<ProductosCasa>();
        pt = transform.parent.GetComponentInChildren<ProductosTienda>();
        pausa = false;
        aciertos = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Demo_Memoria");
        }

        if (tiempoVisualizacion > 0)
        {
            tiempoVisualizacion -= Time.fixedDeltaTime;
            textoTiempo.text = Math.Truncate(tiempoVisualizacion).ToString();
            if (tiempoVisualizacion < 0)
            {
                textoTiempo.text = "0";

                pt.SetTienda(pc.GetListaProductosFaltantes());

                objetosCasa.SetActive(false);
                textoCasa.gameObject.SetActive(false);
                objetosTienda.SetActive(true);
                textoTienda.gameObject.SetActive(true);

                textoTiempo.gameObject.SetActive(false);
            }
        }

        if (!pausa && pt.GetCuentaLPC().Equals(productosFaltantes))
        {
            pausa = true;
            ComparaListas(pc.GetListaProductosFaltantes(), pt.GetListaProductosComprados());
        }
    }

    private void ComparaListas(List<GameObject> listaCasa, List<GameObject> listaTienda)
    {
        for (int i = 0; i < listaTienda.Count; i++)
        {
            for (int j = 0; j < listaCasa.Count; j++)
            {
                if (listaTienda[i].tag.Equals(listaCasa[j].tag))
                {
                    aciertos++;
                    
                    //Colocar el material normal
                    listaCasa[j].GetComponent<Materiales>().SetMaterial(0);
                }
            }
        }

        //Debug.Log(aciertos + " " + productosFaltantes);

        if (aciertos == productosFaltantes)
        {
            textoCasa.text = "Compraste todo lo que necesitabas. :D";
        }
        else
        {
            textoCasa.text = "Te faltaron " + (productosFaltantes - aciertos).ToString() + " " + "productos. D:";
        }

        objetosCasa.SetActive(true);
        textoCasa.gameObject.SetActive(true);
        objetosTienda.SetActive(false);
        textoTienda.gameObject.SetActive(false);
    }
}
