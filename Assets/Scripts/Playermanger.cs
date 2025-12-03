using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Playermanger : MonoBehaviour
{
    float time=0;
    [Header("Manager Controller")]
    private SkeletonAnimation animationController;

    [Header("Objects Controller")]
    public GameObject bulletPrefab;

    [Header("Position Manager")]
    public Transform firePoint;

    [Header("float Manager")]
    internal float bulletSpeed = 20f;
    internal float shootingDelay = 1.5f;
    internal float CurrentHealth = 100f;

    [Header("Boolean Manager")]
    private bool isShooting = false;

    private bool isover = false;
    [Header("playanimationname")]
    public string idle_name = null;
    public string dead_name = null;
    public string attack_name = null;
    public string wark_name = null;

    private GameObject closestEnemy;

    public float attacktime = 0f;
    void Start()
    {
        animationController.AnimationState.Event += HandleAnimationEvent;
        attacktime = animationController.AnimationState.Data.SkeletonData.FindAnimation(attack_name).Duration;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > attacktime+0.25f)
        {
            if (GameManager.instan.Enemy.Count==0)
            {
                return;
            }
            if (GameManager.instan.Enemy.Count>0)
            {
                closestEnemy = GameManager.instan.Enemy[0].gameObject;
                StartCoroutine(ShootingDelay());
            }
            time = 0;
        }
       
    }
   
    private void Awake()
    {
        animationController = GetComponentInChildren<SkeletonAnimation>();
        animationController.AnimationState.SetAnimation(0, idle_name, true);
        shootingDelay = animationController.AnimationState.Data.SkeletonData.FindAnimation(attack_name).Duration;
    }



    private IEnumerator ShootingDelay()
    {
        isShooting = true;
        animationController.AnimationState.SetAnimation(0, attack_name, true);
        if (animationController.AnimationState.Data.SkeletonData.FindAnimation(attack_name).Duration < 0.4f)
        {
            animationController.AnimationState.TimeScale = 0.15f;
        }
        yield return new WaitForSeconds(shootingDelay);
        isShooting = false;
        animationController.AnimationState.SetAnimation(0, idle_name, true);
    }
    private void HandleAnimationEvent(TrackEntry trackEntry, Spine.Event e)
    {
        //Debug.Log("Animation Event: " + e.Data.Name);
        // 在此处执行事件相关的逻辑
        if (e.Data.Name.Equals("hit"))
        {
            if (closestEnemy != null)
            {
                if (closestEnemy.gameObject.GetComponent <Enermy>().isDad)
                {
                    return;
                }
                firePoint.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                GetComponent<AudioSource>().Play();
                Vector2 direction = closestEnemy.transform.position - firePoint.position;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.AngleAxis(angle, Vector3.forward));
                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                bulletRigidbody.velocity = direction * bulletSpeed;
                bullet.GetComponent<Bullet>().shanghai = GameManager.instan.shanghai;
                Destroy(bullet, 4);
            }
        }
    }

}
