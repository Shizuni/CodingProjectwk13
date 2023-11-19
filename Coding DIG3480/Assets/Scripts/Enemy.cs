using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject exsplosionPreFab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if(whatIHit.tag == "Player")
        {
            whatIHit.GetComponent<PlayerBehavior>().LoseLife();
            Instantiate(exsplosionPreFab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (whatIHit.tag == "Weapon")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(1);
            Instantiate(exsplosionPreFab, transform.position, Quaternion.identity);
            Destroy(whatIHit.gameObject);
            Destroy(this.gameObject);
        }
        
    }


    
}