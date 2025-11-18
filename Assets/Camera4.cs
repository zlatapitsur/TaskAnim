using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera4 : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector2 deadZoneSize = new Vector2(2f, 1f);


    
    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        Vector3 diff = target.position - transform.position;
     

        if (Mathf.Abs(diff.x) > deadZoneSize.x)
        {
            pos.x = Mathf.Lerp(pos.x, target.position.x, smoothSpeed * Time.deltaTime);
           
        }

        if (Mathf.Abs(diff.y) > deadZoneSize.y)
        {
            pos.y = Mathf.Lerp(pos.y, target.position.y, smoothSpeed * Time.deltaTime);
            
        }

        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}