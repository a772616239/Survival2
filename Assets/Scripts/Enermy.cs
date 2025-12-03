using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.UI;

public class Enermy : MonoBehaviour
{
    private SkeletonAnimation animationController;
    public GameObject effectenergy;
    public GameObject UItexte;
    public Transform UItransfrom;
    public AudioSource Audio;
    [Range(20, 400), Tooltip("·¶Î§ÊÇ20-200µÄÑªÁ¿")]
    public int HP;
    private float timedata = 0;
    public bool isDad = false;
    [Header("playanimationname")]
    public string idle_name = null;
    public string dead_name = null;
    public string attack_name = null;
    public string wark_name = null;
    public  float attacktime=0f;
    void Start()
    {
        timedata = 0;
        HP = Random.Range(insgame.instant.hpcountmin, insgame.instant.hpUPcountmin);
        animationController = GetComponentInChildren<SkeletonAnimation>();
        ChangeAnimation(wark_name);
        attacktime = animationController.AnimationState.Data.SkeletonData.FindAnimation(attack_name).Duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDad)
        {
            return;
        }
        transform.position -= new Vector3(0, 0.5f * Time.deltaTime, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDad)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            Audio.Play();
            ChangeAnimation(attack_name);
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HP -= collision.gameObject.GetComponent<Bullet>().shanghai;
            var a = Instantiate(UItexte, UItransfrom).GetComponent<Text>();
            a.text = collision.gameObject.GetComponent<Bullet>().shanghai.ToString();
            Destroy(a.gameObject, 1f);
            if (HP <= 0)
            {
                ChangeAnimation(dead_name);
            }
            Destroy(collision.gameObject);
            Audio.Play();
        }
        if (collision.gameObject.CompareTag("Bolt"))
        {
            HP -= collision.gameObject.GetComponent<Bullet>().shanghai;
            var a = Instantiate(UItexte, UItransfrom).GetComponent<Text>();
            a.text = collision.gameObject.GetComponent<Bullet>().shanghai.ToString();
            Destroy(a.gameObject, 1f);
            if (HP <= 0)
            {
                ChangeAnimation(dead_name);
            }
            Audio.Play();
        }
        if (collision.gameObject.CompareTag("Spiner"))
        {
            HP -= collision.gameObject.GetComponent<Bullet>().shanghai;
            var a = Instantiate(UItexte, UItransfrom).GetComponent<Text>();
            a.text = collision.gameObject.GetComponent<Bullet>().shanghai.ToString();
            Destroy(a.gameObject, 1f);
            if (HP <= 0)
            {
                ChangeAnimation(dead_name);
            }
            Audio.Play();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (isDad)
        {
            timedata = 0;
            return;
        }

        if (other.CompareTag("Bolt"))
        {
            timedata += Time.deltaTime;
            if (timedata > 1.5f)
            {
                Audio.Play();
                HP -= other.gameObject.GetComponent<Bullet>().shanghai;
                var a = Instantiate(UItexte, UItransfrom).GetComponent<Text>();
                a.text = other.gameObject.GetComponent<Bullet>().shanghai.ToString();
                a.color = Color.red;
                Destroy(a.gameObject, 1f);
                if (HP <= 0)
                {
                    ChangeAnimation(dead_name);
                }
                timedata = 0;
            }
        }
        if (other.CompareTag("Spiner"))
        {
            timedata += Time.deltaTime;
            if (timedata > 1.5f)
            {
                Audio.Play();
                HP -= other.gameObject.GetComponent<Bullet>().shanghai;
                var a = Instantiate(UItexte, UItransfrom).GetComponent<Text>();
                a.text = other.gameObject.GetComponent<Bullet>().shanghai.ToString();
                a.color = Color.red;
                Destroy(a.gameObject, 1f);
                if (HP <= 0)
                {
                    ChangeAnimation(dead_name);
                }
                timedata = 0;
            }
        }
        if (other.CompareTag("Finish"))
        {
            timedata += Time.deltaTime;
            if (timedata > attacktime+1f)
            {
                GameManager.instan.HP -= Random.Range(3,11);
                if (GameManager.instan.HP <= 0)
                {
                    GameManager.instan.gameOver();
                }
                timedata = 0;
            }
        }
    }
    public void ChangeAnimation(string AnimationName)
    {
        if (animationController == null)
            return;

        bool IsLoop = true;
        if (AnimationName == dead_name)
        {
            isDad = true;
            IsLoop = false;
            var teng = GetComponents<Collider2D>();
            foreach (var item in teng)
            {
                item.enabled = false;
            }
            GetComponent<Rigidbody2D>().useAutoMass = true;
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            var game = Instantiate(effectenergy, transform.position, effectenergy.transform.rotation);
            Destroy(game.gameObject, 0.5f);
            GameManager.instan.Rankexperience += Random.Range(3, 11);
            if (GameManager.instan.Rankexperience >= GameManager.instan.RankexperienceUP)
            {
                GameManager.instan.UpSetGrad();
            }
            Destroy(this.gameObject, animationController.AnimationState.Data.SkeletonData.FindAnimation(dead_name).Duration - 0.1f);
        }
        //set the animation state to the selected one
        animationController.AnimationState.SetAnimation(0, AnimationName, IsLoop);
    }
}
