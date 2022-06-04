using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float launchForceAmount;
    [SerializeField] float maxDragDistance = 5f;
    [SerializeField] float delaySeconds;

    Vector2 startPosition;
    Rigidbody2D birdBody;

    // Start is called before the first frame update
    void Start()
    {
        
        birdBody = GetComponent<Rigidbody2D>();
        birdBody.isKinematic = true;  
        startPosition = birdBody.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(delaySeconds);
        birdBody.position = startPosition;
        birdBody.isKinematic = true;
        birdBody.velocity = Vector2.zero;
    
    }

    void OnMouseDown() 
    {
        GetComponent<SpriteRenderer>().color = Color.red; 
    }

    void OnMouseUp() 
    {
        Vector2 currentPosition = birdBody.position;
        Vector2 direction = startPosition - currentPosition;
        direction.Normalize();
        birdBody.isKinematic = false;
        birdBody.AddForce(direction * launchForceAmount);
        
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void OnMouseDrag() 
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 desiredPosition = mousePosition; 

        float distance = Vector2.Distance(desiredPosition, startPosition);
        if(distance > maxDragDistance)
        {
            Vector2 direction = desiredPosition - startPosition;
            direction.Normalize();
            desiredPosition = startPosition + direction * maxDragDistance;
        }

        if(desiredPosition.x > startPosition.x)
            desiredPosition.x = startPosition.x;

        birdBody.position = desiredPosition;
    }

}
