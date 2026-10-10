using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    public float Timer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Dest());

       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Dest()
    {
        yield return new WaitForSeconds(Timer);
        Destroy(gameObject);
    }
}
