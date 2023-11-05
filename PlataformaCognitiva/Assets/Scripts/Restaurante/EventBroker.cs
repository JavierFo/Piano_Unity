using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBroker 
{

    public static event Action<Ingredientes> IngredienteCachado;
    public static event Action PlayerAcerto;
    public static event Action PlayerFallo;


    public static void EvaluaIngrediente(Ingredientes alimento) {
        if (IngredienteCachado != null) {
            IngredienteCachado(alimento);
        }
    }
    // Start is called before the first frame update

    public static void Acierto()
    {
        if (PlayerAcerto != null)
        {
            PlayerAcerto();
        }
    }

    public static void Fallo()
    {
        if (PlayerFallo != null)
        {
            PlayerFallo();
        }
    }

}
