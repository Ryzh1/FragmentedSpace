using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooting : MonoBehaviour
{
    public int ammoCount;
    public int shotsTaken;
    public Rigidbody2D projectile;
    public Transform barrel;
    public UnityEngine.Rendering.Universal.Light2D muzzle;
    public Animator animator;
    public AudioSource audio;
    private bool canShoot = true;
    void Start()
    {
        muzzle.enabled = false ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && ammoCount > 0 && canShoot)
        {
            shotsTaken += 1;
            canShoot = false;
            animator.SetTrigger("Shoot");
            muzzle.enabled = true;
            StartCoroutine(MuzzleFlash());
            Rigidbody2D proj = Instantiate(projectile, barrel.position, barrel.rotation);
            proj.AddForce(proj.transform.up * 20, ForceMode2D.Impulse);
            audio.Play();
            ammoCount--;
            StartCoroutine(WaitForAnim());
        }
    }


    IEnumerator MuzzleFlash()
    {
        yield return new WaitForSeconds(0.1f);
        muzzle.enabled = false;
    }
    IEnumerator WaitForAnim()
    {
        yield return new WaitForSeconds(0.2f);
        canShoot = true;
    }
}
