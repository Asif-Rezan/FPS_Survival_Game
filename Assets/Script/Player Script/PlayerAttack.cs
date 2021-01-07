using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weapon_manager;
    public float fireRate=15f;
    private float nextTimeToFire;
    public float damage=20f;


    private Animator zoomCameraAnim;
    private bool zoomed;

    private Camera mainCam;
    private GameObject crosshair;

    private bool is_Aiming;

    [SerializeField]
    private GameObject arrow_Prefab, spear_Prefab;
    [SerializeField]
    private Transform arrow_Bow_StartPosition;

    private void Awake()
    {
        weapon_manager = GetComponent<WeaponManager>();
        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        mainCam = Camera.main;
    }


    void Start()
    {
        
    }

   
    void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
    }
    void WeaponShoot()
    {
        if(weapon_manager.GetCurrrentSelectedWeapon().fireType==WeaponFireType.MULTIPLE)
        {
            if(Input.GetMouseButton(0) && Time.time>nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                weapon_manager.GetCurrrentSelectedWeapon().ShootAnimation();

                BulletFired();
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(weapon_manager.GetCurrrentSelectedWeapon().tag==Tags.AXE_TAG)
                {
                    weapon_manager.GetCurrrentSelectedWeapon().ShootAnimation();
                }
                if(weapon_manager.GetCurrrentSelectedWeapon().bulletType==WeaponBulletType.BULLET)
                {
                    weapon_manager.GetCurrrentSelectedWeapon().ShootAnimation();
                    BulletFired();
                }
                else
                {
                    if(is_Aiming)
                    {
                        weapon_manager.GetCurrrentSelectedWeapon().ShootAnimation();
                        if(weapon_manager.GetCurrrentSelectedWeapon().bulletType==WeaponBulletType.ARROW)
                        {
                            //throw arrow
                            ThrowArrowOrSpear(true);
                            
                            
                        }
                        else if(weapon_manager.GetCurrrentSelectedWeapon().bulletType==WeaponBulletType.SPEAR)
                        {
                            //throw Spear
                            ThrowArrowOrSpear(false);
                        }
                    }

                }
            }

        }

    }

    void ZoomInAndOut()
    {
        if(weapon_manager.GetCurrrentSelectedWeapon().wapon_aim==WeaponAim.AIM)
        {
            if(Input.GetMouseButtonDown(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);
                crosshair.SetActive(false);
            }

            if (Input.GetMouseButtonUp(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                crosshair.SetActive(true);
            }
        }

        if(weapon_manager.GetCurrrentSelectedWeapon().wapon_aim==WeaponAim.SELF_AIM)
        {
            if(Input.GetMouseButtonDown(1))
            {
                weapon_manager.GetCurrrentSelectedWeapon().Aim(true);
                is_Aiming = true;
            }
            if(Input.GetMouseButtonUp(1))
            {
                weapon_manager.GetCurrrentSelectedWeapon().Aim(false);
                is_Aiming = false;
            }
        }



    }


    void ThrowArrowOrSpear(bool throwArrow)
    {
       if(throwArrow)
        {
            GameObject arrow = Instantiate(arrow_Prefab);
            arrow.transform.position = arrow_Bow_StartPosition.position;

            arrow.GetComponent<ArrowBowScript>().Launch(mainCam);
        }
       else
        {
            GameObject spear = Instantiate(spear_Prefab);
            spear.transform.position = arrow_Bow_StartPosition.position;

            spear.GetComponent<ArrowBowScript>().Launch(mainCam);
        }

    }
    void BulletFired()
    {
        RaycastHit hit;

        if(Physics.Raycast(mainCam.transform.position,mainCam.transform.forward,out hit))
        {

            Debug.Log("Hit: " + hit.transform.tag);
           

    

            if(hit.transform.tag==Tags.ENEMY_TAG)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
        }
    }
}
