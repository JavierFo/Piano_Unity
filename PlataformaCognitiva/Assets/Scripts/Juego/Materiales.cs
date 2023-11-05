using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materiales : MonoBehaviour
{
    [SerializeField]
    Material[] materiales = new Material [2];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaterial (int material)
    { 
        //0 - hay
        //1 - falta
        this.GetComponent<Renderer>().material = materiales[material];
    }
}
