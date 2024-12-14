using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    public LayerMask targetLayer;
    public FireRangeHandler fireRangeHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject FindClosestTarget(Vector3 currentPosition)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, fireRangeHandler.FireRange, targetLayer);
        float closestDistance = Mathf.Infinity; // de luu k/cach ngan nhat toi muc tieu
        GameObject closestTarget = null; //de luu d/tuong gan nhat tim duoc. Null vi chua x/dinh dc muc tieu
       
        if (hits.Length > 0) //kiem tra co doi tuong nao duoc tim thay hay khong
        {
            

            foreach (var hit in hits) // lap qua tung doi tuong trong mang hits
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position); // luu k/cach voi d/tuong hien tai
                if (distance < closestDistance) //neu k/cach voi d/tuong nho hon k/cach ngan nhat da biet
                {
                    closestDistance = distance; // cap nhat k/cach ngan nhat
                    closestTarget = hit.gameObject;  // cap nhat d/tuong gan nhat
                }
            }
            
            return closestTarget; // tra ve muc tieu gan nhat tim duoc
        }

        
        return null;
    }    
}
