using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instan;
    public GradeShanghai Grade;
    public List<Enermy> Enemy = new List<Enermy>();
    public Transform playertrage;
    private int gradecount = 0;
    public int grade = 0;
    public int shanghai = 10;
    public int Rankexperience = 0;
    public int RankexperienceUP = 100;
    public int HP = 100;
    [Header("UI")]
    public Text gradeTex;
    public Text RankexperienceTex;
    public Text HpTex;
    public Image RankexperienceImage;

    [Space(10), Tooltip("添加道具GameObject")]
    public GameObject AddMoveWap;
    [Tooltip("添加道具的按钮")]
    public List<UIGrader> AddBth = new List<UIGrader>();
    [Tooltip("道具对象按照配置表顺序")]
    public List<GameObject> prop = new List<GameObject>();

    private List<GameObject> BthGame = new List<GameObject>();
    List<int> xuanzhong = new List<int>();
    List<int> Endxuanzhong = new List<int>();
    List<int> itemxuanzhong = new List<int>();
    public GameObject End;
    private void Awake()
    {
        if (instan == null)
        {
            instan = this;
        }
        else
        {
            Destroy(this);
        }
        setgrad(Grade.Gradeproperties[0]);
        gradecount = 0;
        Endxuanzhong.Clear();
        for (int i = 0; i < Grade.GradeDrone.gradeDrones.Count; i++)
        {
            Endxuanzhong.Add(i);
        }
    }
    public void UpSetGrad()
    {
        if (Randonprop())
        {
            AddMoveWap.SetActive(true);
            Time.timeScale = 0;
            gradecount++;
            if (gradecount > Grade.Gradeproperties.Count - 1)
            {
                gradecount = Grade.Gradeproperties.Count - 1;
                Rankexperience = 0;
                return;
            }
            setgrad(Grade.Gradeproperties[gradecount]);
            Rankexperience = 0;
        }

    }
    bool Randonprop()
    {
        BthGame.Clear();
        itemxuanzhong.Clear();
        if (xuanzhong.Count == 0)
        {
            //
            int item = Endxuanzhong[Random.Range(0, Endxuanzhong.Count)];
            AddBth[0].gradeDrone = Grade.GradeDrone.gradeDrones[item];
            AddBth[0].SetGradDrone();
            BthGame.Add(prop[item]);
            itemxuanzhong.Add(item);
            //
            item = Endxuanzhong[Random.Range(0, Endxuanzhong.Count)];
            if (itemxuanzhong.Contains(item))
            {
                List<int> itemnum = new List<int>();
                foreach (var p in Endxuanzhong)
                {
                    if (!itemxuanzhong.Contains(p))
                    {
                        itemnum.Add(p);
                    }
                }
                item = itemnum[Random.Range(0, itemnum.Count)];
            }
            AddBth[1].gradeDrone = Grade.GradeDrone.gradeDrones[item];
            AddBth[1].SetGradDrone();
            BthGame.Add(prop[item]);
            itemxuanzhong.Add(item);
            //
            item = Endxuanzhong[Random.Range(0, Endxuanzhong.Count)];
            if (itemxuanzhong.Contains(item))
            {
                List<int> itemnum = new List<int>();
                foreach (var p in Endxuanzhong)
                {
                    if (!itemxuanzhong.Contains(p))
                    {
                        itemnum.Add(p);
                    }
                }
                item = itemnum[Random.Range(0, itemnum.Count)];
            }
            AddBth[2].gradeDrone = Grade.GradeDrone.gradeDrones[item];
            AddBth[2].SetGradDrone();
            BthGame.Add(prop[item]);
            itemxuanzhong.Add(item);

            return true;
        }
        else
        {
            if (Endxuanzhong.Count < 3)
            {
                if (Endxuanzhong.Count == 0)
                {
                    return false;
                }
                int countxuan = Endxuanzhong.Count;
                for (int i = 0; i < countxuan; i++)
                {
                    int item = Endxuanzhong[Random.Range(0, Endxuanzhong.Count)];
                    if (itemxuanzhong.Count > 0)
                    {
                        if (itemxuanzhong.Contains(item))
                        {
                            List<int> itemnum = new List<int>();
                            foreach (var p in Endxuanzhong)
                            {
                                if (!itemxuanzhong.Contains(p) && !xuanzhong.Contains(p))
                                {
                                    itemnum.Add(p);
                                }
                            }
                            item = itemnum[Random.Range(0, itemnum.Count)];
                        }
                    }
                    AddBth[i].gradeDrone = Grade.GradeDrone.gradeDrones[item];
                    AddBth[i].SetGradDrone();
                    BthGame.Add(prop[item]);
                    itemxuanzhong.Add(item);
                }
                for (int i = 2; i >= countxuan; i--)
                {
                    AddBth[i].gameObject.SetActive(false);
                }
                return true;
            }
            else
            {
                //
                int item = Endxuanzhong[Random.Range(0, Endxuanzhong.Count)];
                if (xuanzhong.Contains(item))
                {
                    List<int> itemnum = new List<int>();
                    foreach (var p in Endxuanzhong)
                    {
                        if (!xuanzhong.Contains(p))
                        {
                            itemnum.Add(p);
                        }
                    }
                    item = itemnum[Random.Range(0, itemnum.Count)];
                }
                AddBth[0].gradeDrone = Grade.GradeDrone.gradeDrones[item];
                AddBth[0].SetGradDrone();
                BthGame.Add(prop[item]);
                itemxuanzhong.Add(item);
                //
                item = Endxuanzhong[Random.Range(0, Endxuanzhong.Count)];
                if (itemxuanzhong.Contains(item))
                {
                    List<int> itemnum = new List<int>();
                    foreach (var p in Endxuanzhong)
                    {
                        if (!itemxuanzhong.Contains(p) && !xuanzhong.Contains(p))
                        {
                            itemnum.Add(p);
                        }
                    }
                    item = itemnum[Random.Range(0, itemnum.Count)];
                }
                AddBth[1].gradeDrone = Grade.GradeDrone.gradeDrones[item];
                AddBth[1].SetGradDrone();
                BthGame.Add(prop[item]);
                itemxuanzhong.Add(item);
                //
                item = Endxuanzhong[Random.Range(0, Endxuanzhong.Count)];
                if (itemxuanzhong.Contains(item))
                {
                    List<int> itemnum = new List<int>();
                    foreach (var p in Endxuanzhong)
                    {
                        if (!itemxuanzhong.Contains(p) && !xuanzhong.Contains(p))
                        {
                            itemnum.Add(p);
                        }
                    }
                    item = itemnum[Random.Range(0, itemnum.Count)];
                }
                AddBth[2].gradeDrone = Grade.GradeDrone.gradeDrones[item];
                AddBth[2].SetGradDrone();
                BthGame.Add(prop[item]);
                itemxuanzhong.Add(item);

                return true;
            }
        }

    }
    void setgrad(gradeproperty gradeproperty)
    {
        grade = gradeproperty.grade;
        shanghai = gradeproperty.shanghai;
        RankexperienceUP = gradeproperty.RankexperienceUP;
        RankexperienceUP = gradeproperty.RankexperienceUP;
        HP = gradeproperty.HPUP;
    }
    void Update()
    {
        gradeTex.text = $"等级为{grade + 1}";
        RankexperienceTex.text = $"{Rankexperience}/{RankexperienceUP}";
        HpTex.text = $"{HP}";
        RankexperienceImage.fillAmount = (float)Rankexperience / (float)RankexperienceUP;
        EnemyIns();
    }

    private void EnemyIns()
    {
        Enemy.Clear();
        var ene = FindObjectsOfType<Enermy>();
        if (ene.Length <= 0)
        {
            return;
        }
        foreach (var item in ene)
        {
            Enemy.Add(item);
        }
        if (Enemy.Count > 0)
        {
            Enemy.RemoveAll(a => a.isDad == true);
        }
        if (Enemy.Count > 1)
        {
            Enemy.Sort(delegate (Enermy x, Enermy y)
            {
                //Debug.Log(Vector3.Distance(x.transform.position, playertrage.transform.position).CompareTo(Vector3.Distance(y.transform.position, playertrage.transform.position)));
                return Vector3.Distance(x.transform.position, playertrage.transform.position).CompareTo(Vector3.Distance(y.transform.position, playertrage.transform.position));
            });
        }
    }

    public void gameOver()
    {
        insgame.instant.gameObject.SetActive(false);
        for (int i = 0; i < Enemy.Count; i++)
        {
            Destroy(Enemy[i].gameObject);
        }
        foreach (var item in prop)
        {
            item.gameObject.SetActive(false);
        }
        End.SetActive(true);
    }
    public void TimeReset()
    {
        Time.timeScale = 1;
    }
    public void gameResta()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LeftBth()
    {
        BthGame[0].gameObject.SetActive(true);
        Endxuanzhong.Remove(itemxuanzhong[0]);
        xuanzhong.Add(itemxuanzhong[0]);
        AddMoveWap.gameObject.SetActive(false);
        BthGame.Clear();
        itemxuanzhong.Clear();
    }

    public void CenetBth()
    {
        BthGame[1].gameObject.SetActive(true);
        Endxuanzhong.Remove(itemxuanzhong[1]);
        xuanzhong.Add(itemxuanzhong[1]);
        AddMoveWap.gameObject.SetActive(false);
        BthGame.Clear();
        itemxuanzhong.Clear();
    }
    public void RightBth()
    {
        BthGame[2].gameObject.SetActive(true);
        Endxuanzhong.Remove(itemxuanzhong[2]);
        xuanzhong.Add(itemxuanzhong[2]);
        AddMoveWap.gameObject.SetActive(false);
        BthGame.Clear();
        itemxuanzhong.Clear();
    }
}
