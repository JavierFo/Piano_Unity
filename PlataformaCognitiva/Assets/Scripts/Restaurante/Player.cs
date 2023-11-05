using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _velocidad = 1f;
    // Start is called before the first frame update
    float _limIzq = -4.7f;
    float _limDer = 7.4f;
    Animator _anim;

    private void OnEnable()
    {
        EventBroker.PlayerAcerto += ReproduceAnimacionAcierto;
        EventBroker.PlayerFallo += ReproduceAnimacionFallo;
    }

    private void OnDisable()
    {
        EventBroker.PlayerAcerto -= ReproduceAnimacionAcierto;
        EventBroker.PlayerFallo -= ReproduceAnimacionFallo;
    }

    void Start()
    {
        _anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(this.transform.position.x>_limIzq)
                this.transform.position += Vector3.left * Time.deltaTime*_velocidad;
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            if (this.transform.position.x < _limDer)
                this.transform.position += Vector3.right * Time.deltaTime*_velocidad;
        }
    }

    void ReproduceAnimacionAcierto() {
        _anim.SetTrigger("acierto") ;
    }

    void ReproduceAnimacionFallo()
    {
        _anim.SetTrigger("fallo");
    }
}
