using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    Player mainCharacter;
    void Start()
    {
        mainCharacter =  gameObject.AddComponent<Player>();
        mainCharacter.instantiate();
    }

    // Update is called once per frame
    void Update()
    {
        mainCharacter.react();
    }
}
