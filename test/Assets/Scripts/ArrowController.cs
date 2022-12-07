using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField]
    private GameObject midPointVisual, arrowPrefab, arrowSpawnPoint;

    [SerializeField]
    private float arrowMaxSpeed = 100;

    private AudioSource audioSource;
    public int damage;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PrepareArrow()
    {
        midPointVisual.SetActive(true);
    }

    public void ReleaseArrow(float strength)
    {
        midPointVisual.SetActive(false);
        audioSource.PlayOneShot(audioSource.clip);audioSource.PlayOneShot(audioSource.clip);

        GameObject arrow = Instantiate(arrowPrefab);
        arrow.transform.position = arrowSpawnPoint.transform.position;
        arrow.transform.rotation = arrowSpawnPoint.transform.rotation;
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddForce(arrowSpawnPoint.transform.forward * strength * arrowMaxSpeed, ForceMode.Impulse);

        RaycastHit hit;
        Vector3 origin = arrow.transform.position;
        Vector3 direction = -arrow.transform.forward;
        if (Physics.Raycast(origin, direction, out hit, 100f))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("Monster"))
            {
                Monster monsterScript = hitObject.GetComponent<Monster>();
                monsterScript.Hurt(damage);
            }
        }
    }
}