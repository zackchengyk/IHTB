using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BulletSpawn : MonoBehaviour
{

    [SerializeField] private GameObject _bulletPrefab; 

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnSimpleBullet", 2.0f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnSimpleBullet()
    {
        Debug.Log("called!");
        GameObject temp = Instantiate(_bulletPrefab, this.transform.position, Quaternion.identity) as GameObject;
    }

    /*void SpawnBullet(Vec2 location, Vec2 velocity)
    {
        GameObject temp = Instantiate(Resources.Load("DeathParticles"), location, Quaternion.identity) as GameObject;
    }*/

}
