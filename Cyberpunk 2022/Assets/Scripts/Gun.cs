using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] private Transform _fireingPoint;
    [SerializeField] private GameObject _projectilePrefab;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot() {
        Instantiate(_projectilePrefab, _fireingPoint.position, _fireingPoint.rotation);
    }
}
