using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    // elemental type

    public List<Gun> gunPrefabs;    // should have 4 guns, one for each elemental type.

    //public List<string> fireNames;
    //public List<string> iceNames;
    //public List<string> earthNames;
    //public List<string> windNames;


    //public Vector2 fireDamageRange = new Vector2(4,8);
    //public Vector2 iceDamageRange = new Vector2(3,6);
    //public Vector2 earthDamageRange = new Vector2(6,10);
    //public Vector2 windDamageRange = new Vector2(3,6);


    //public Vector2 fireRateOfFireRange = new Vector2(0.2f, 0.4f);   // fast, high damage
    //public Vector2 iceRateOfFireRange = new Vector2(0.3f, 0.6f);    // slow, low damage
    //public Vector2 earthRateOfFireRange = new Vector2(0.5f, 1.0f);  // slow, high damage
    //public Vector2 windRateOfFireeRange = new Vector2(0.5f, 0.15f); // fast, low damage

    private bool onCooldown = false;

    void SpawnItem() {
        if(!onCooldown) {
            // picking a random number to choose the elementType.
            int elementType = Random.Range(0,4);

            Gun newGun = Instantiate(gunPrefabs[Random.Range(0, gunPrefabs.Count)], transform.position, transform.rotation);

            // creating that type as an enum.
            newGun.elType = (Gun.elements)elementType;
            newGun.Randomize();
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            SpawnItem();
        }
    }

    IEnumerator Cooldown() {
        onCooldown = true;
        yield return new WaitForSeconds(3);
        onCooldown = false;
    }



}
