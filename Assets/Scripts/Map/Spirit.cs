using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour {
    Vector3 destination;
    float lifeTime;
    float maxLifeTime;
    float acc;
    public void InitSetting(Vector3 destination,float lifeTime)
    {
        this.destination = destination;
        this.maxLifeTime = lifeTime;
        float moveTime = maxLifeTime - GetComponent<ParticleSystem>().main.startLifetime.constant;
        acc = 2 * (destination - transform.position).magnitude / (moveTime * moveTime);
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= maxLifeTime)
        {
            Destroy(this.gameObject);
        }
        transform.position += (destination - transform.position).normalized * acc*lifeTime*Time.deltaTime;
        if ((transform.position - destination).magnitude < 0.5f)
        {
            GetComponent<ParticleSystem>().Stop();
        }
    }
}
