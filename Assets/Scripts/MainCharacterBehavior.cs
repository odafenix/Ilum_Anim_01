using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterBehavior : MonoBehaviour
{
    //Estadisticas de velocidad para personaje protagonico controlable
    [Header ("Movement Settings")]
    public float moveSpeed = 4f;
    public float rotationSpeed = 100f;
    public bool isRunning = false; 
    public float touchingGroundRay;
    public float jumpForce;
    public bool isTouchingGround;
    //Necesitamos el controlador de animator para referencias las animaciones
    [Header ("Animator Settings")]
    public Animator _animator;
    public Rigidbody characterRigidbody;


    //Update ocurre cada frame. Private significa que esta oculta para otras clases. Void devuelve vacio
    private void Update() {
        
        Running();
        CheckingForGround();
        Jump();
        Movement();
        

        characterRigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
        characterRigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
        
    }

    private void FixedUpdate() {
        
        
    }
    private void Movement(){
        //Entrada(Input) moverse arriba/abajo
        float move = Input.GetAxis("Vertical");
        //Entrada(Input) rotación derecha/izquierda
        float rotation = Input.GetAxis("Horizontal");

        //Usaremos los atributos ya capturados del GameObject(Jugador)
        //Incrementamos su posicion con Forward * La velocidad * Input(Move) logica de fotograma
        transform.position += transform.forward * moveSpeed * move * Time.deltaTime;
        //Cambiamos su rotación, en el eje Y 
        transform.Rotate(Vector3.up, rotationSpeed * rotation * Time.deltaTime);
            
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
            _animator.SetFloat("MovementSpeed", moveSpeed);
        }
        else {
            _animator.SetFloat("MovementSpeed", 0f);
        }

    }
    private void Running(){
        if (Input.GetKeyDown(KeyCode.LeftShift)){
                // Debug.Log("LeftShift is being pressed and running is true");
                isRunning = !isRunning;
                if (isRunning == true){
                    moveSpeed = 8f;
                }
                if (isRunning == false){
                    moveSpeed = 4f;
                }
                else{
            return;
                
            }
            
        }
    }
    private void CheckingForGround(){
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, Vector3.down);

        Debug.DrawRay(transform.position, Vector3.down * touchingGroundRay);

        
        if(Physics.Raycast(landingRay, out hit, touchingGroundRay)){
                if(hit.collider.tag == "Environment"){
                    Debug.Log("I'm touching the ground");
                    _animator.SetBool("IsJumping", false);
                    isTouchingGround = true;
                }
                else{
                    return;
                }
            }
        if(!Physics.Raycast(landingRay, out hit, touchingGroundRay)){
            _animator.SetBool("IsJumping", true);
                    isTouchingGround = false;
            }
                 else{
                    return;}
    }
    

    private void Jump(){
       if (Input.GetKeyDown(KeyCode.Space)){
           if(isTouchingGround == true)
           Debug.Log("Estoy saltando");
           characterRigidbody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
           }
           if(isTouchingGround == false){
               return;
           }
       }
}
    
 
          
 
        



