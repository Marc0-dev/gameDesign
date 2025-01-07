using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    List<Switch> nearSwitches = new List<Switch>();
    List<Passive> nearNpcs = new List<Passive>();
    Player mainCharacter;
    void Start(){
        mainCharacter = GameObject.Find("mainCharacter").GetComponent<Player>();
    }
    void OnTriggerEnter(Collider other)
    {
        IInteractive otherInterface = other.gameObject.GetComponent<IInteractive>();
        if(otherInterface != null){
            if(other.CompareTag("Switch")){
                nearSwitches.Add(other.gameObject.GetComponent<Switch>());
            }
            else if(other.CompareTag("Passive")){
                nearNpcs.Add(other.gameObject.GetComponent<Passive>());
            }
            else if(other.CompareTag("Collectable")){
                other.GetComponent<Collectable>().Interact();
            }
        }
        else{
            Debug.Log("Object in range");
        }
    }
    void OnTriggerExit(Collider other)
    {
        nearSwitches.Remove(other.gameObject.GetComponent<Switch>());
    }
    public void Interact(){
        if(nearSwitches.Count() != 0){
            Switch nearest = nearSwitches.First();
            nearSwitches.Remove(nearest);
            nearest.Interact();
        }
        if(nearNpcs.Count() != 0){
            Passive nearest = nearNpcs.First();
            nearNpcs.Remove(nearest);
            nearest.Interact();
        }
        else
            Debug.Log("not a switch");
    }
}
