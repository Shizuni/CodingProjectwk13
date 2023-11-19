using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    
    public float speed;
    public float horizontalInput;
    public float verticalInput;
    public float horizontalScreenLimit;
    public float verticalScreenLimit;
    public GameObject bulletPrefab;
    public int lives;
    public GameObject exsplosionPreFab;
    public GameObject gM;
    public AudioClip coinSound;
    public AudioClip healthSound;
    public AudioClip powerUpSound;
    public AudioClip powerDownSound;
    private bool betterWeapon;
    public GameObject shield;
    public GameObject thruster;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("test");
        speed = 6.5f;
        lives = 3;
        horizontalScreenLimit = 14.36f;
        verticalScreenLimit = -3.47f;
        gM= GameObject.Find("GameManager");
        betterWeapon = false;
        
    }

    // Update is called once per frame
    void Update()
    {
       Movement();
       Shooting();

    }

    void Movement()
    {
                horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);
        if(transform.position.x > horizontalScreenLimit) 
        {
            //I am outside the screen from right
            transform.position = new Vector3(-horizontalScreenLimit, transform.position.y,0);
        } 
        else if (transform.position.x < -horizontalScreenLimit)
        {
            //I am outside the screen from the left
            transform.position = new Vector3(horizontalScreenLimit, transform.position.y,0);
        } 
        else if (transform.position.y >= 0f) 
        {
            //I am touching the screen from top/middle
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= verticalScreenLimit) 
        {
            //I am touching the screen from bottom
            transform.position = new Vector3(transform.position.x, verticalScreenLimit, 0);
        }
    }

    void Shooting()
    {
        //if press SPACE, create a bullet
        if(Input.GetKeyDown(KeyCode.Space) && !betterWeapon)
        {
            //create a bullet
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
        else if(Input.GetKeyDown(KeyCode.Space) && betterWeapon)
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 1, 0), Quaternion.Euler(0, 0, -45f));
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.Euler(0, 0, 45f));
        }        

    }

    public void LoseLife()
    {
        lives--;
        gM.GetComponent<GameManager>().LivesChange(lives);
        if (lives <= 0)
        {
            //Game Over
           gM.GetComponent<GameManager>().GameOver();
            Instantiate(exsplosionPreFab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
                switch(collision.name)
        {
            case "Coin(Clone)":
            // I picked a Coin up
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            gM.GetComponent<GameManager>().EarnScore(1);
            Destroy(collision.gameObject);
                break;
            case "Health(Clone)":
            // I picked up a Health
            AudioSource.PlayClipAtPoint(healthSound, transform.position);
            if (lives >= 3)
            {
                gM.GetComponent<GameManager>().EarnScore(1);
            }   
            else if (lives < 3)
            {
                lives++;
                gM.GetComponent<GameManager>().LivesChange(lives);
            }
            Destroy(collision.gameObject);
                break;
            case "Power Up(Clone)":
             // I picked up a Powerup
             AudioSource.PlayClipAtPoint(powerUpSound, transform.position);
             Destroy(collision.gameObject); 
             int tempInt;
             tempInt= Random.Range(1, 4);
             if (tempInt == 1)
             {
                //Speed Powerup
                speed = 10f;
                StartCoroutine("SpeedPowerDown");
                gM.GetComponent<GameManager>().PowerupChange("Speed");
                thruster.SetActive(true);
             } 
             else if (tempInt == 2)
             {
                //Weapon Powerup
                betterWeapon = true;
                StartCoroutine("WeaponPowerDown");
                gM.GetComponent<GameManager>().PowerupChange("Weapon");
             }
             else if (tempInt ==3)
             {
                //Shield Powerup
                shield.SetActive(true);
                
             }
                break;
                case "EnemyOne(Clone)":
                Debug.Log("EnemeyOne Collision");
                if (shield.activeSelf)
                {
                     // Shield is active, destroy the shield instead of losing a life
                shield.SetActive(false);
                AudioSource.PlayClipAtPoint(powerDownSound, transform.position);
                gM.GetComponent<GameManager>().PowerupChange("No Power Up");
            }
            break;
         }
         
    }

    IEnumerator SpeedPowerDown ()
    {
        yield return new WaitForSeconds(4f);
        AudioSource.PlayClipAtPoint(powerDownSound, transform.position);
        speed = 6.5f;
        thruster.SetActive(false);
        gM.GetComponent<GameManager>().PowerupChange("No Power Up");
    }

    IEnumerator WeaponPowerDown ()
    {
        yield return new WaitForSeconds(4f);
        AudioSource.PlayClipAtPoint(powerDownSound, transform.position);
        betterWeapon = false;
        gM.GetComponent<GameManager>().PowerupChange("No Power Up");
    }

    






}
    

