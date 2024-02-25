using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private float speed = 5f;

    private Vector3 targetPosition = Vector3.zero;

    private void OnEnable()
    {
        _camera = Camera.main;

        Vector3 cameraPos = _camera.transform.position;
        cameraPos.z = 0;
        targetPosition = cameraPos;
    }

    private void Update()
    {
        Seek();

        if (Vector3.Distance(targetPosition, transform.position) <= 0.1f)
        {
            Explode();
        }
    }

    public void Seek()
    {
        if (_camera != null)
        {
            Vector3 directionToCenter = (targetPosition - transform.position);

            directionToCenter.z = 0;
            directionToCenter.Normalize();

            //Rotation on y axis instead of z
            if (directionToCenter.x < 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            //Move towards camera
            transform.Translate(directionToCenter * speed * Time.deltaTime);
        }
    }
    private void Explode()
    {
        float explosionRadius = _camera.orthographicSize * 2f * Screen.width / Screen.height;
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, 1 << LayerMask.NameToLayer("Enemy"));

        foreach (Collider2D enemy in enemyColliders)
        {
            if (enemy.TryGetComponent(out DamageableEnemy de))
            {
                de.Die();
            }
        }
        Destroy(gameObject);
    }
}
