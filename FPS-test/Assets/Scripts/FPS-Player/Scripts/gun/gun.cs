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
    [Tooltip("Weapon danage")]
    [SerializeField] private float damage = 12.5f;
    [Tooltip("Weapon range in meters")]
    [SerializeField] private float range = 500f;
    [Tooltip("Rate of fire (fire rate is 600 / min for the ak so we have to divide 600 with 60 = 10 rounds/sec)")]
    [SerializeField] private float fireRate = 10f; // fire rate is 600 / min for the ak so we have to divide 600 with 60 = 10 rounds/sec
    [Tooltip("Maximum ammo inthe magazine")]
    [SerializeField] private float maxAmmo = 31;
    [Tooltip("Reload time (used for wait until the animator finis the animation, it takes almost 3 secondt to animate reload)")]
    [SerializeField] private float reloadTime = 2.967f;
    [Tooltip("Text shown on the right corner (Ammo in the magazine)")]
    [SerializeField] private Text MagazinesizeText;

    private float currentAmmo = 0;
    private float hitForce = 30f;
    private float checkWeaponAnimationTime = 3.817f;
    private float fireDelay = 0.5f;

    //objects
    [Header("Objects to attach to weapon")]
    [Tooltip("The blood spill effect spawn (object)")]
    [SerializeField] private GameObject bloodEffect;
    [Tooltip("The impact effect spawn (object)")]
    [SerializeField] private GameObject impactEffect;
    [Tooltip("Muzzle particle system (object)")]
    [SerializeField] private ParticleSystem muzzleFlash;
    [Tooltip("Main Camera used as muzzle")]
    private Camera muzzle;
    [Tooltip("Source of the fire sound effect (weapon)")]
    [SerializeField] private AudioSource fireSource;
    [Tooltip("Source of the reload sound effect (weapon)")]
    [SerializeField] private AudioSource reloadSource;
    [Tooltip("Reload sound")]
    [SerializeField] private AudioClip reloadClip;
    [Tooltip("Shooting sound")]
    [SerializeField] private AudioClip fireClip;
    [Tooltip("Animator")]
    [SerializeField] private Animator animator;

    [Header("Aim zoom preferences")]
    [Tooltip("Field of view value")]
    [SerializeField] float target = 30f;
    [Tooltip("Zoom multiplier")]
    [SerializeField] public float zoomMultiplier = 10;
    [Tooltip("The default Field of View")]
    [SerializeField] private float defaultFOV = 71f;
    [Tooltip("Duration of the zoom (how much seconds it takes)")]
    [SerializeField] public float zoomDuration = 0.3f;

    //booleans
    private bool canShoot = true;
    private bool isReloading = false;
    private bool canAim = false;


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
            canAim = true;
            Ads();
        }
        else
        {
            canAim = false;
            animator.SetBool("ADS", false);
            float angle = Mathf.Abs((defaultFOV / zoomMultiplier) - defaultFOV);
            muzzle.fieldOfView = Mathf.MoveTowards(muzzle.fieldOfView, defaultFOV, angle / zoomDuration * Time.deltaTime);
        }
        #endregion

        #region Check your weapon animation
        if (Input.GetKey(KeyCode.C) && canAim == false)
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
                damage = 12.5f;
                foe.takeDamage(damage);
            }
            if (hit.collider is SphereCollider)
            {
                damage = 100f;
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
        canAim = false;
        muzzle.fieldOfView = defaultFOV;
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

        if (canAim == true)
        {
            animator.SetBool("ADS", true);

            float angle = Mathf.Abs((defaultFOV / zoomMultiplier) - defaultFOV);
            muzzle.fieldOfView = Mathf.MoveTowards(muzzle.fieldOfView, target, angle / zoomDuration * Time.deltaTime);
        }

    }
    #endregion

    #region Check your weapon animation
    IEnumerator checkWeapon()
    {
        canShoot = false;
        canAim = false;
        isReloading = false;
        muzzle.fieldOfView = defaultFOV;

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
