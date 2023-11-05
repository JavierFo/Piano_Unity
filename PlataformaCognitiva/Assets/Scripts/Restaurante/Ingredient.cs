using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField]
    float _velocidad = 4f;
    // Start is called before the first frame update

    public Ingredientes nombre;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.down * _velocidad * Time.deltaTime;
        if (this.transform.position.y < -5) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.gameObject.tag.Equals("Player"))
        {
            EventBroker.EvaluaIngrediente(nombre);
            Destroy(this.gameObject);
        }
    }

}
