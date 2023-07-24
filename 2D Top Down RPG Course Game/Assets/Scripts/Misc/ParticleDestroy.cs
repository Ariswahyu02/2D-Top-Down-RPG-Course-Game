using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    private ParticleSystem slimeParticle;

    private void Awake() 
    {
        slimeParticle = GetComponent<ParticleSystem>();
    }

    private void Update() 
    {
        if(slimeParticle && !slimeParticle.IsAlive())
        {
            Destroy(gameObject);
        }    
    }
}
