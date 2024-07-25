using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public float speed = 2;

    void Update()
    {
        float _newZPosition = transform.position.z - Time.fixedDeltaTime * speed;
        transform.position = new Vector3(transform.position.x, 0, _newZPosition);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("DestroyObstacles"))
        {
            Destroy(this.gameObject);
        }
    }
}


