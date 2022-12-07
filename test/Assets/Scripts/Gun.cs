using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Gun : MonoBehaviour
{
    private AudioSource audioSource;
    private ParticleSystem particleSystem;
    public int damage;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particleSystem = transform.Find("MuzzleFlashEffect").GetComponent<ParticleSystem>();
    }
    public void Fire() {
        audioSource.PlayOneShot(audioSource.clip);
        particleSystem.Play();

        RaycastHit hit;
        Vector3 origin = particleSystem.transform.position;
        Vector3 direction = particleSystem.transform.right;
        if (Physics.Raycast(origin, direction, out hit, 100f)) {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("Monster"))
            {
                Monster monsterScript = hitObject.GetComponent<Monster>();
                monsterScript.Hurt(damage);
            }
        }
    }
}
