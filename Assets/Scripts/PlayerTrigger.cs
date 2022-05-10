using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public Gun gun;

    void Start() {
        if(gun == null) {
            Debug.LogError("You forgot to assign the gun here.");
            // try to find it manually, use this method sparingly
            gun = GameObject.Find("Gun").GetComponent<Gun>();
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Ammo Pickup")) {
            // call the gun.getammo() function
            gun.GetAmmo();
            Destroy(other.gameObject);
        }
    }

    // after ontriggerenter?
    // ontriggerstay
    // do damage every three seconds for each enemy touching us.
}
