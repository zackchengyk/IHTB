using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BulletSpawn : MonoBehaviour
{

    [SerializeField] private GameObject _bulletPrefab; 

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnSimpleBullet", 0.0f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnSimpleBullet()
    {
        Debug.Log("called!");
        GameObject temp = Instantiate(_bulletPrefab, this.transform.position, Quaternion.identity) as GameObject;

        //SpawnBullet(new Vector2(2, 0), new Vector2(1, 1));
    }

    void SpawnBullet(Vector2 location, Vector2 velocity)
    {
        GameObject temp = Instantiate(_bulletPrefab, location, Quaternion.identity) as GameObject;
        temp.GetComponent<Rigidbody2D>().velocity = velocity;
    }

}
