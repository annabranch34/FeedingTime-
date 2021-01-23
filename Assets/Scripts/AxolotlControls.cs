using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class AxolotlControls : MonoBehaviour

{  
    public float speed;
    

    
    private Rigidbody2D rd2d;
    private bool facingRight = true;

    public Text shrimpText;
    private int shrimp = 4;

    public GameObject BackgroundMusic;
    public GameObject IntroMusic;
    AudioSource audioSource;

    
    public AudioClip loseMusic;
    public AudioClip winMusic;

    public AudioClip shrimpSound;

    public Text timerText;
    public int secondsLeft = 15;
    public bool takingAway = false;

    public bool gameOver = false;


    public Text winloseText;
    
  
    Animator anim;



    




void Start()
   {
      rd2d = GetComponent<Rigidbody2D>();
      audioSource = GetComponent<AudioSource>();
      anim = GetComponent<Animator>();

      shrimp = 4;
      shrimpText.text = "Shrimp :  4" ;

      timerText.text = " " + secondsLeft;

      winloseText.text = "" ;

   }

   // Update is called once per frame
   void Update()
   {

       if (takingAway == false && secondsLeft > 0)
       {
           StartCoroutine(TimerTake());
       }

       else if (shrimp == 0)
       {
           StopAllCoroutines();
           Destroy(timerText);
           
       }

  
       
     
   }


   IEnumerator TimerTake()
   {

       
       takingAway = true;
       yield return new WaitForSeconds(1);
       secondsLeft -= 1;
       if (secondsLeft < 15)
       {
           timerText.text = " " + secondsLeft;
           
        
       }

       else
       {
   
           
          
       }
        takingAway = false;


        if (secondsLeft == 0)
        {
            gameOver=true;
            speed = 0;
            winloseText.text = "You Lose! Press R To Restart";
            anim.SetInteger("State", 1);
            Destroy(BackgroundMusic);
            audioSource.clip = loseMusic;
            audioSource.Play();
            rd2d.velocity = Vector3.zero;
            
            
        }


        
   }









   void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }


   void FixedUpdate()
    {
        {
            float hozMovement = Input.GetAxis("Horizontal");
            float vertMovement = Input.GetAxis("Vertical");
        
            rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

             if (facingRight == false && hozMovement > 0)
            {
                Flip();
            }
        else if (facingRight == true && hozMovement < 0)
            {
                Flip();
            }
        }

                if (Input.GetKey("escape"))
            {
                Application.Quit();
            }



            if (Input.GetKey(KeyCode.R))

        {

            if (gameOver == true)

            {

              SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 

            }


        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if(collision.collider.tag == "Shrimp")
        {
            shrimp -= 1;
            shrimpText.text = "Shrimp :  " + shrimp.ToString();
            
            Destroy(collision.collider.gameObject);
            audioSource.clip = shrimpSound;
            audioSource.Play();
            audioSource.loop = false;

        }

        if (shrimp == 0)
        {
            gameOver=true;
            speed = 0;
            Destroy(BackgroundMusic);
            audioSource.clip = winMusic;
            audioSource.Play();
            audioSource.loop = false;
            rd2d.velocity = Vector3.zero;
            winloseText.text = "You Win! Game Created By Anna Branch";
 
            

        }






    }


   






}





