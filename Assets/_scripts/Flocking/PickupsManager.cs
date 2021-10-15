using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsManager : MonoBehaviour
{
    public static PickupsManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private static PickupsManager _instance;


    [SerializeField]
    private GameObject prefabPickup;

    [SerializeField]
    private List<Pickup> loadedPickups;

    [SerializeField, Header("Y Capped at 15")]
    private BoxCollider areaSpawn;

    [SerializeField]
    private int spawnedPickups = 3;

    public List<Pickup> LoadedPickups { get => loadedPickups; }

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;

        loadedPickups = new List<Pickup>();

        StartCoroutine(LoadPickupsInScene(this.spawnedPickups));


    }
    private IEnumerator LoadPickupsInScene(int qty)
    {
        for (int i = 0; i < qty; i++)
        {
            loadedPickups.Add(GeneratePickup(RandomColliderPoint(this.areaSpawn)));
            yield return new WaitForSeconds(0.333f);
        }
    }
    private Vector3 RandomColliderPoint(BoxCollider col)
    {

        Vector3 result = new Vector3();
        result.x = col.bounds.center.x + Random.Range(-col.bounds.max.x, col.bounds.max.x) / 2;
        result.y = 15f;
        result.z = col.bounds.center.z + Random.Range(-col.bounds.max.z, col.bounds.max.z) / 2;


        return result;
    }
    private Pickup GeneratePickup(Vector3 spawnPosition)
    {
        GameObject result;

        result = Instantiate(prefabPickup, spawnPosition, Quaternion.identity, null);

        return result.GetComponent<Pickup>();
    }
}
