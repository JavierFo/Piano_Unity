using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminRestaurante : MonoBehaviour
{
    int errores=0;
    public Text contadorErrores_text;
    public GameObject felicitacion_text;


    List<Ingredientes> tacosList = new List<Ingredientes>() { Ingredientes.Tortilla, Ingredientes.Frijoles, Ingredientes.Lechuga, Ingredientes.Pollo };
    List<Ingredientes> chilaquilesList = new List<Ingredientes>() { Ingredientes.Totopo, Ingredientes.Tomate, Ingredientes.ChileSerrano, Ingredientes.Cebolla, Ingredientes.Pollo };

    Receta _recetaGanadora;

    public ObjectSpawner invocador;

    List<Receta> _recetario = new List<Receta>();
    List<Ingredientes> aciertos = new List<Ingredientes>();

    Receta tacos = new Receta();
    Receta chilaquiles = new Receta();

    private void OnEnable()
    {
        EventBroker.IngredienteCachado += EvaluaIngrediente;
    }

    private void OnDisable()
    {
        EventBroker.IngredienteCachado -= EvaluaIngrediente;
    }

    private void Awake()
    {
        contadorErrores_text.text = "Incorrectos: " + errores.ToString();
        felicitacion_text.SetActive(false);
    }

    void Start()
    {
        tacos.nombre = Platillo.Tacos;
        tacos.ingredientes = tacosList;
        tacos.numIngredientes = tacosList.Count;

        chilaquiles.nombre = Platillo.Chilaquiles;
        chilaquiles.ingredientes = chilaquilesList;
        chilaquiles.numIngredientes = chilaquilesList.Count;

        _recetario.Add(tacos);
        _recetario.Add(chilaquiles);

        //_recetaGanadora = _recetario[Random.Range(0, _recetario.Count)];
        _recetaGanadora = _recetario[0];

        //_recetaGanadora = tacos;
        invocador.AsignaReceta(_recetaGanadora);
    }


    void EvaluaIngrediente(Ingredientes alimento) {
        int posIngredienteVitrina = _recetaGanadora.ingredientes.IndexOf(alimento);
        Debug.Log(posIngredienteVitrina);
        if (posIngredienteVitrina != -1)
        {
            invocador.ActivaPalomita(posIngredienteVitrina + 1);
            if (aciertos.Contains(alimento) == false) {
                aciertos.Add(alimento);
            }
            EventBroker.Acierto();
        }
        else {
            errores++;
            contadorErrores_text.text = "Incorrectos: " + errores.ToString();
            EventBroker.Fallo();
        }
        Debug.Log("num aciertos"+ aciertos.Count);
        if (aciertos.Count == _recetaGanadora.ingredientes.Count) {
            PasaASigNivel();
        }
    }

    void PasaASigNivel() {
        errores = 0;
        aciertos.Clear();
        invocador.DetenInvocaciones();
        felicitacion_text.SetActive(true);
        StartCoroutine(TiempoAntesDeAvanzar());
    }

    IEnumerator TiempoAntesDeAvanzar() {
        yield return new WaitForSeconds(5f);
        felicitacion_text.SetActive(false);
        contadorErrores_text.text = "Incorrectos: " + errores.ToString();
        _recetaGanadora = _recetario[1];
        invocador.AsignaReceta(_recetaGanadora);
    }


}

public class Receta {
    public Platillo nombre;
    public List<Ingredientes> ingredientes;
    public int numIngredientes;

}
