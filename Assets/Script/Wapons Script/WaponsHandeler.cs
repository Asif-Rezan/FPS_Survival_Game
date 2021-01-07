using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum WeaponAim
{
    NONE,
    SELF_AIM,
    AIM
}
public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}
public enum WeaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}



public class WaponsHandeler : MonoBehaviour
{
    private Animator anim;
    public WeaponAim wapon_aim;
    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField]
    private AudioSource shoot_sound, reload_sound;

    public WeaponFireType fireType;
    public WeaponBulletType bulletType;
    public GameObject atttack_Point;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void ShootAnimation()
    {
        anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }

    public void Aim(bool canAim)
    {
        anim.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }
    void TurnOnMuzzleFlush()
    {
        muzzleFlash.SetActive(true);
    }
    void TurnOffMuzzleFlush()
    {
        muzzleFlash.SetActive(false);
    }
    void PlayShootSound()
    {
        shoot_sound.Play();
    }
    void PlayRelodeSound()
    {
        reload_sound.Play();
    }
    void TurnOnAttackPoint()
    {
        atttack_Point.SetActive(true);
    }
    void TurnOffAttackPoint()
    {
        if(atttack_Point.activeInHierarchy)
        {
            atttack_Point.SetActive(false);
        }
    }







    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
