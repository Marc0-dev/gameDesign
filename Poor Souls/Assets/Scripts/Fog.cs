using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public Renderer thisRenderer;
    public Collider thisCollider;
    bool isColliding = false;
    SingleWave parent;
    void OnTriggerEnter(Collider other)
    {
        if(isColliding || other.CompareTag("Entity") == false)return;
        isColliding = true;
        parent.CollisionDetected();
    }
    void Start()
    {
        thisCollider = this.gameObject.GetComponent<Collider>();
        thisRenderer = this.gameObject.GetComponent<Renderer>();
        thisRenderer.enabled = false;
        parent = GetComponentInParent<SingleWave>();
    }
}
