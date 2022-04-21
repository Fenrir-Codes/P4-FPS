using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class gun : MonoBehaviour
{
    // This script have to be attached on tha gun object.

    //Weapon settings
    [Header("weapon settings")]
    [SerializeField]
    private float damage = 25f;
    [SerializeField]
    private float range = 1000f;
    [SerializeField]
    private float fireRate = 10f; // fire rate is 600 / min for the ak so we have to divide 600 with 60 = 10 rounds/sec
    [SerializeField]
    private float maxAmmo = 31;
    [SerializeField]
    private float reloadTime = 2.967f;
    [SerializeField]
    private Text MagazinesizeText;


    private float currentAmmo = 0;
    private float hitForce = 30f;
    private float checkWeaponAnimationTime = 3.817f;
    private float fireDelay = 0.5f;


    //objects
    [Header("Objects to attach to weapon")]
    [SerializeField] private GameObject bloodEffect; // the impact effect that spawns
    [SerializeField] private GameObject impactEffect; // the impact effect that spawns
    [SerializeField] private ParticleSystem muzzleFlash; // muzzle particle system
    private Camera muzzle; //main camera as muzzle
    [SerializeField] private AudioSource fireSource; //Weapon
    [SerializeField] private AudioSource reloadSource; //reloadsource
    [SerializeField] private AudioClip reloadClip; // reload sound
    [SerializeField] private AudioClip fireClip; // shoot sound
    [SerializeField] private Animator animator; // animator
    
    //booleans
    private bool canShoot = true;
    private bool isReloading = false;


    private void Awake()
    {

        currentAmmo = maxAmmo;
        
        muzzle = Camera.main;
        
    }

    /* 
     The Main Difference Between Update And FixedUpdate In Unity
     The Update function runs exactly once per frame, while the FixedUpdate function runs at a fixed rate. 
     What this means is: The Update function will run as often as your game’s frame rate. For example,
     if your game runs at 120 frames per second, the Update function will be called 120 times within 1 second.*/
    void FixedUpdate()
    {

        #region check on isReloading boolean
        if (isReloading)
        {
            return;
        }
        #endregion

        #region call reload automatically if magazine is empty
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        #endregion

        #region Call Shooting on button clik or hold
        //shooting single or full auto
        if (canShoot && Input.GetMouseButton(0) && Time.time >= fireDelay)
        {
            animator.SetBool("Fireing", true);
            fireDelay = Time.time + 1f / fireRate;
            fireSource.PlayOneShot(fireClip);
            Shoot();
        }
        else
        {
            animator.SetBool("Fireing", false);
        }
        #endregion

        #region Reload
        if (Input.GetKey(KeyCode.R) && currentAmmo != maxAmmo)
        {
            animator.SetBool("Fireing", false);
            StartCoroutine(Reload());
            return;
        }
        #endregion

        #region Aiming
        //Aiming
        if (Input.GetMouseButton(1))
        {
            Ads();
        }
        else
        {
            animator.SetBool("ADS", false);
        }
        #endregion

        #region Check your weapon animation
        if (Input.GetKey(KeyCode.C))
        {
            StartCoroutine(checkWeapon());
            return;
        }
        #endregion

        MagazinesizeText.text = currentAmmo.ToString();

        
    }

    #region Shooting script
    void Shoot()
    {
        //play particle system
        muzzleFlash.Play();
        currentAmmo --;
        

        //raycast shoot
        RaycastHit hit;
        if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, range))
        {
            //Debug.Log("Hit " + hit.transform.name);
            //Debug.DrawRay(transform.position, transform.forward, Color.green);

            //if the object wich has the enemy script on it not null take the damage
            enemy foe = hit.transform.GetComponent<enemy>();
            if (foe != null)
            {
                foe.takeDamage(damage);
            }

            //if hit hits rigidbody add force to it
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }

            //If the objet we are hitting tagged as Enemy spawn blood splash instead of bullet impact
            if (hit.transform.tag =="Enemy")
            {
                GameObject bloodObject = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(bloodObject, 0.3f);
            }
            else
            {
                GameObject impactObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactObject, 0.3f);
            }

        }

    }

    #endregion

    #region Reload script
    IEnumerator Reload()
    {
        canShoot = false;
        isReloading = true;
        reloadSource.PlayOneShot(reloadClip);

        animator.SetBool("Reloading",true);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;

        isReloading = false;
        canShoot = true;
        animator.SetBool("Reloading", false);
    }
    #endregion

    #region ADS (Aim Down the Sight)
    void Ads()
    {
        canShoot = true;
        animator.SetBool("ADS", true);
    }
    #endregion

    #region Check your weapon animation
    IEnumerator checkWeapon()
    {
        canShoot = false;
        isReloading = false;
        if (isReloading != true && canShoot != true)
        {
            animator.SetBool("CheckWeapon", true);
            yield return new WaitForSeconds(checkWeaponAnimationTime);
            animator.SetBool("CheckWeapon", false);
            canShoot = true;
        }
    }
    #endregion



}
