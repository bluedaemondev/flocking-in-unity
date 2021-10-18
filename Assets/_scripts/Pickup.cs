using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float health = 100;
    public float eatenPerSec = 25;

    float originalForce;

    public bool canLoseHealth;

    Coroutine beingAte;

    [Range(0.1f, 5f)]
    public float rangeBeingEat = 2.13f;
    public bool hasSeeker;

    public void BeEaten(Boid b)
    {
        originalForce = b.MaxForce;
        b.MaxForce = 0;
        StartCoroutine(EatenCoroutine(b));
    }
    private IEnumerator EatenCoroutine(Boid b)
    {
        while (health > 0)
        {
            health -= eatenPerSec;

            yield return new WaitForSeconds(3f);
            Debug.Log("health " + health);
        }

        PickupsManager.Instance.LoadedPickups.Remove(this);
        yield return null;

        b.MaxForce = originalForce;
        Destroy(this.gameObject);
    }

    //// Start is called before the first frame update
    void Start()
    {
        if (!PickupsManager.Instance.LoadedPickups.Contains(this))
        {
            PickupsManager.Instance.LoadedPickups.Add(this);
        }
    }

}
