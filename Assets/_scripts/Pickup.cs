using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float health = 100;
    public float eatenPerSec = 25;

    public bool canLoseHealth;

    Coroutine beingAte;

    private IEnumerator EatenCoroutine()
    {
        while (health > 0)
        {
            health -= eatenPerSec;
            yield return new WaitForSeconds(1f);
            Debug.Log("health " + health);
        }

        PickupsManager.Instance.LoadedPickups.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (beingAte == null && other.CompareTag("Player")) {
            Debug.Log("Entering food source");
            beingAte = StartCoroutine(EatenCoroutine());
        }

    }

    //// Start is called before the first frame update
    void Start()
    {
        if (!PickupsManager.Instance.LoadedPickups.Contains(this.gameObject))
        {
            PickupsManager.Instance.LoadedPickups.Add(this.gameObject);
        }
    }

}
