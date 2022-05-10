using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    enum healthType {Player, Enemy, Object};

    [SerializeField]
    healthType hType = healthType.Object;

    public int health = 10;

    public Gun.elements elType = Gun.elements.Fire;

    private bool isDying = false;

    void Update() {
        if(health <= 0 && !isDying) {
            Death();
        }

        if(hType == healthType.Player) {
            UIManager.playerHealthText.text = "Health: " + health.ToString();
        }
    }

    void Death() {
        isDying = true;
        if(hType == healthType.Object) {
            Destroy(this.GetComponent<Collider>());     // keep it from colliding with its parts
            Destroy(this.GetComponent<Renderer>());          //make it disappear
            for(int i = 0; i < 4; i++) {
                // randomize color of each part
                // have each part inherit bullet velocity
                // change the number of parts based on the size of the object.
                // have each part be randomly placed inside of the object, instead of centered.
                GameObject part = GameObject.CreatePrimitive(PrimitiveType.Cube);
                part.transform.localScale = Vector3.one * Random.Range(0.5f, 0.8f);
                part.transform.position = this.transform.position;
                part.transform.Translate(Random.Range(-.5f, .5f), Random.Range(-.5f, 5f), Random.Range(-.5f, 5f));
                part.AddComponent<Rigidbody>();
            }
            Destroy(this.gameObject, 1);
        } else if(hType == healthType.Enemy) {
            this.gameObject.AddComponent<Rigidbody>(); // make enemy fall to death
            Destroy(this.gameObject, 5);

        } else if (hType == healthType.Player) {
            Application.LoadLevel(0);           //restart the level.
        }
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("Bullet")) {
            Destroy(other.gameObject); //deletes bullets
            Debug.Log("I got hit by a bullet!");

            Bullet bullet = other.gameObject.GetComponent<Bullet>();

            if(bullet.elType == this.elType) {
                // half daamage
                health -= bullet.damage / 2;
            } else if(((int)bullet.elType + 2) % 4 == (int)this.elType) {
                //double damage
                health -= bullet.damage * 2;
            } else {
                // regular damage
                health -= bullet.damage;
            }

            if(health <= 0) {
                Destroy(this.gameObject);
            }
        }
    }
}
