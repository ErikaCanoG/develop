using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
   private int vidas = 3;
    
    //float nivelPiso = -4.5f; // Este valor representa el nivel del piso para el personaje 
    float nivelTecho = 15.2f; // Este valor representa la parte superior de la escena  
    float limiteR = 23.2f;  // Este valor representa el limite derecho de la camara para el personaje 
    float limiteL = -23.5f;  // Este valor representa el limite izquierdo de la camara para el personaje
    float velocidad = 4f; // velocidad desplazamiento del personaje 
    float fuerzaSalto = 80;  // x veces la masa del personaje 
    float fuerzaDesplazamiento = 1000;  // Fuerza en Newtons
    
    bool enElPiso = false;
    
     [SerializeField] private AudioSource salto_SFX;
    
    void Start()
    {
        // personaje siempre inicia en la posicion (-10.82, -3.12)
        gameObject.transform.position = new Vector3(-10.82f,nivelTecho,0);
        Debug.Log("INIT");
        Debug.Log("VIDAS: " + vidas);
    }

    void Update()
    {
        if(gameObject.transform.rotation.z > 0.3 || gameObject.transform.rotation.z < -0.3 ){
            Debug.Log("ROTATION: " + gameObject.transform.rotation.z);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        
        if(Input.GetKey("right") && enElPiso){
            Debug.Log("RIGHT");
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(fuerzaDesplazamiento, 0));
        }
        else if(Input.GetKey("left") && gameObject.transform.position.x > limiteL){
            Debug.Log("LEFT");
            gameObject.transform.Translate(-velocidad*Time.deltaTime, 0, 0);
        }

        if(Input.GetKeyDown("space") && enElPiso){
            Debug.Log("UP");
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -fuerzaSalto*Physics2D.gravity[1]*gameObject.GetComponent<Rigidbody2D>().mass));
            salto_SFX.Play();
            enElPiso = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.tag == "Ground"){
            enElPiso = true;
            Debug.Log("GROUND COLLISION");
        } 
        else if(collision.transform.tag == "Obstaculo"){
            enElPiso = true;
            Debug.Log("OBSTACULO COLLISION"); 
        }
    }   

    private void OnTriggerEnter2D(Collider2D collision){
       Debug.Log("Caida"); 
       vidas -= 1;
       Debug.Log("VIDAS: " + vidas);
       if(vidas <= 0){
           Debug.Log("GAME OVER"); 
           vidas = 3;
       }
       gameObject.transform.position = new Vector3(-10.82f,nivelTecho,0);
    }
}


     