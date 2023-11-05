using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public enum Ingredientes { Aguacate, Ajo, Axiote, Carne,CarneMolida, Calabaza, Chayote, Chicharo, Chipotle, ChilePasilla, ChileGuajillo, ChilePoblano, 
    ChileSerrano, Cebolla, Cilantro, Crema, Elote, Epazote, Frijoles, Granada, Jitomate, Lechuga, Maiz, Mole, Papa,Pinon, QuesoOaxaca,QuesoPanela,Rabano, Pollo, Sope, Tomate, Tortilla, Totopo, Zanahoria }
public enum Platillo { Tacos, Chilaquiles }
public class ObjectSpawner : MonoBehaviour
{

    //Platillos
    [Header("Platillos")]
    public GameObject tacos;
    public GameObject chilaquiles;


    //Ingredientes
    [Header("Ingredientes")]
    public GameObject tortilla;
    public GameObject cebolla;
    public GameObject zanahoria;
    public GameObject frijoles;
    public GameObject lechuga;
    public GameObject carne;
    public GameObject pollo;
    public GameObject tomate;
    public GameObject epazote;
    public GameObject chileSerrano;
    public GameObject totopo;

    public Dictionary<Ingredientes, GameObject> almacen = new Dictionary<Ingredientes, GameObject>();
    public Dictionary<Platillo, GameObject> recetario = new Dictionary<Platillo, GameObject>();
    List<Ingredientes> receta = new List<Ingredientes>();
    List<Ingredientes> basura = new List<Ingredientes>();

    public Transform[] posicionesVitrina;
    public Transform[] invokePositions;
    private int _posAleatoriaAnterior = 0;
    private int _posAleatoriaAnteriorBasura = 0;

    private List<GameObject> objsVitrina=new List<GameObject>();

    private void Awake()
    {
        LimpiaVitrina();
        //Ingredientes-------------------------------
        almacen.Add(Ingredientes.Tortilla, tortilla);
        almacen.Add(Ingredientes.Cebolla, cebolla);
        almacen.Add(Ingredientes.Zanahoria, zanahoria);
        almacen.Add(Ingredientes.Frijoles, frijoles);
        almacen.Add(Ingredientes.Lechuga, lechuga);
        almacen.Add(Ingredientes.Carne, carne);
        almacen.Add(Ingredientes.Pollo, pollo);
        almacen.Add(Ingredientes.Tomate, tomate);
        almacen.Add(Ingredientes.Epazote, epazote);
        almacen.Add(Ingredientes.ChileSerrano, chileSerrano);
        almacen.Add(Ingredientes.Totopo, totopo);
        //Platillos---------------------------------
        recetario.Add(Platillo.Tacos, tacos);
        recetario.Add(Platillo.Chilaquiles, chilaquiles);


    }


    public void AsignaReceta(Receta _recetaGanadora) {
        receta = _recetaGanadora.ingredientes;

        //for (int i = 0; i < receta.Count; i++)
        //{
        //    Debug.Log(receta[i]);
        //}
        basura.Clear();
        LimpiaVitrina();
        StopAllCoroutines();
        //Defino qué elementos que no pertenecen a la receta se van a  invocar
        foreach (KeyValuePair<Ingredientes, GameObject> elemento in almacen)
        {
            Debug.Log(elemento.Key);
            if (receta.Contains(elemento.Key) == false)
            {
                basura.Add(elemento.Key);
            }
        }
        //Debug.Log(basura.Count);
        //for (int i = 0; i < basura.Count; i++)
        //{
        //    Debug.Log(basura[i]);
        //}
        MuestraIngredientes(_recetaGanadora.nombre);
        ComienzaAInvocar();
    }

    void MuestraIngredientes(Platillo _platilloPrincipal) {
        GameObject platilloVitrina= Instantiate(recetario[_platilloPrincipal], posicionesVitrina[0].position, Quaternion.identity);
        objsVitrina.Add(platilloVitrina);
        for (int i = 0; i < receta.Count; i++) {
            GameObject temp= Instantiate(almacen[receta[i]], posicionesVitrina[i + 1].position, Quaternion.identity);
            Destroy(temp.GetComponent<Ingredient>());
            objsVitrina.Add(temp);
            //temp.transform.parent = posicionesVitrina[i + 1];
            //temp.transform.position = Vector3.zero;
        }
    }

    void ComienzaAInvocar() {

        //InvokeRepeating("InvocaIngrediente", 0.5f, 3f);
        //InvokeRepeating("InvocaBasura", 0.5f, 2f);
        StartCoroutine(InvocaIngrediente());
        StartCoroutine(InvocaBasura());
    }

    IEnumerator InvocaIngrediente() {
        int _posicionAleatoria = Random.Range(0, invokePositions.Length);
        if (_posAleatoriaAnterior == _posicionAleatoria) {
            _posicionAleatoria = Random.Range(0, invokePositions.Length);
        }
        Instantiate(almacen[receta[Random.Range(0, receta.Count)]], invokePositions[_posicionAleatoria].position, Quaternion.identity);
        _posAleatoriaAnterior = _posicionAleatoria;
        yield return new WaitForSeconds(3f);
        StartCoroutine(InvocaIngrediente());
    }

    IEnumerator InvocaBasura()
    {
        int _posicionAleatoria = Random.Range(0, invokePositions.Length);
        if (_posAleatoriaAnteriorBasura == _posicionAleatoria)
        {
            _posicionAleatoria = Random.Range(0, invokePositions.Length);
        }
        Instantiate(almacen[basura[Random.Range(0, basura.Count)]], invokePositions[_posicionAleatoria].position, Quaternion.identity);
        _posAleatoriaAnteriorBasura = _posicionAleatoria;
        yield return new WaitForSeconds(2f);
        StartCoroutine(InvocaBasura());
    }

    void LimpiaVitrina() {
        for (int i = 1; i < posicionesVitrina.Length; i++)
        {
            posicionesVitrina[i].GetChild(0).gameObject.SetActive(false);
        }
        for (int i = 0; i < objsVitrina.Count; i++)
        {
            Destroy(objsVitrina[i].gameObject);
        }
        objsVitrina.Clear();
    }


    public void DetenInvocaciones() {
        StopAllCoroutines();
    }


    public void ActivaPalomita(int pos) {
        posicionesVitrina[pos].GetChild(0).gameObject.SetActive(true);
    }


}
