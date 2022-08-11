using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
using UnityEngine.UI;
public class AK47_Advanced : MonoBehaviour
{
    [SerializeField] public int maxAmmo = 10;
    [SerializeField] public int currentAmmo = -1;
    [SerializeField] public float reloadTime = 1f;
    public Text ammoText;
    private bool isReloading;
    public Animator reloadAnimation;


    public float effectSpawnRate = 10;
    public float timetoSpawnEffect = 0;
    public float fireRate = 3;
    public float Damage = 30;
    public LayerMask whatToHit; /// What we want to hit
    public Transform BulletTrailPrefab;


    private float timeToFire = 0;
    private Transform firepoint;
    public Transform MuzzleFlashPrefab;
    public event EventHandler<OnShootEventArgs> OnShoot;

    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;

    }

    void Start()
    {
        if (currentAmmo == 1)
        {
            currentAmmo = maxAmmo;
        }
    }
    void OnEnable()
    {
        isReloading = false;
        aimAnimator.SetBool("Reloading", false);
    }

    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private Animator aimAnimator;
    private object currentHealth;



    // Start is called before the first frame update
    void Awake()
    {

        firepoint = transform.Find("FirePoint");
        if (firepoint == null)
        {
            Debug.LogError("No firepoint found...");
        }
    }

    // Update is called once per frame
    void Update()
    {


        ammoText.text = currentAmmo.ToString();

        /*
        if (isReloading)
            return;
        */

        if (fireRate == 0) ///This will shoot
        {
            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;

            }

            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
                aimAnimator.SetTrigger("Active"); ///This inititates the pistol shooting animation
                OnShoot?.Invoke(this, new OnShootEventArgs
                {

                    gunEndPointPosition = aimGunEndPointTransform.position,
                    shootPosition = mousePosition,


                });
            }
        }

        else ///If it is not single fire it will shoot 
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }

    }

    IEnumerator Reload()
    {
        isReloading = true;

        aimAnimator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - 0.25f);
        aimAnimator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firepoint.position.x, firepoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit); ///100 is an continous raycast
        UtilsClass.ShakeCamera(0.05f, 0.1f);

        currentAmmo--;

        ///Vector3 shootDir = (e.shootPosition - e.gunEndPointPosition).normalized;
        ///UtilsClass.ApplyRotationToVector(shootDir, -90f);
        ///ShellParticleSystemHandler.Instance.SpawnShell(e.shellPosition,shootDir);
        if (Time.time >= timetoSpawnEffect) //Object pooling for bullet
        {
            Effect();
            Effect2();
            timetoSpawnEffect = Time.time + 1 / effectSpawnRate;
        }

        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition * 100), Color.yellow);

        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit" + hit.collider.name + "and did" + Damage + "damage with the pistol.");
        }

    }

    void Effect() ///Bullet Trail
    {
        Instantiate(BulletTrailPrefab, firepoint.position, firepoint.rotation);

    }




    void Effect2() ///MuzzleFlash
    {
        var MuzzleFlashInstanceClone = (Transform)Instantiate(MuzzleFlashPrefab, firepoint.position, firepoint.rotation);
        MuzzleFlashInstanceClone.parent = firepoint;
        float size = UnityEngine.Random.Range(7.5f, 8.8f);
        MuzzleFlashInstanceClone.localScale = new Vector3(7.5f, 7.5f, 0);


    }


    public int addAmmo()
    {

        return currentAmmo += 30;

    }


}

