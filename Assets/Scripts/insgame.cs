using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insgame : MonoBehaviour
{
    public static insgame instant;
    public  List<Transform> inspos = new List<Transform>();
   

    [Header("Enemys Manager")]
    public List<GameObject> EnemysGame = new List<GameObject>();
    public bool isINs = true;
    internal bool ManagerPose = false;
    internal bool MakeIt = true;
    [Header("Vectors Controller")]
    public GameObject SpawenLocalisation;
    int count=0;
    float time = 0;
    float timedata = 0;
    int countIns = 1;
    int countcishu = 1;

    public int hpcountmin = 10;
    public int hpUPcountmin = 20;
    private void Awake()
    {
        if (instant==null)
        {
            instant = this;
        }
        else
        {
            Destroy(this);
        }
        hpcountmin = 10;
        hpUPcountmin = 20;
        isINs = true;
    }
    // Start is called before the first frame update
    void OnEnable()
    {   //isINs = true;
        count = 0;
        timedata = 0;
        time = Random.Range(10f, 25f);
        countIns = 1;
        countcishu = 1;
        StartCoroutine(SpaweningManager());
       
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        timedata = 0;
        countIns = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (timedata > time)
        {
            time = Random.Range(5f, 10f);
            count = Random.Range(0, EnemysGame.Count);
            timedata = 0;
            countcishu++;
            hpcountmin += Random.Range(1, 5);
            hpcountmin = Mathf.Clamp(hpcountmin, 10, 495);
            hpUPcountmin += Random.Range(5, 10);
            hpUPcountmin = Mathf.Clamp(hpUPcountmin, 20, 500);
            if (countcishu >= 3)
            {
                countcishu = Random.Range(0, 3);
            }
            countIns = countcishu;
        }

        MakeIt = true;
        timedata += Time.deltaTime;
    }
   

    public IEnumerator SpaweningManager()
    {
        yield return new WaitForEndOfFrame();
        count = Random.Range(0, EnemysGame.Count);
        if (ManagerPose == false)
        {
            if (MakeIt == true)
            {
                if (isINs)
                    for (int i = 0; i < countIns; i++)
                    {
                        count = Random.Range(0, EnemysGame.Count);
                        if (SpawenLocalisation.transform.childCount < 200)
                                (Instantiate(EnemysGame[count], pos(), EnemysGame[count].transform.rotation) as GameObject).transform.SetParent(SpawenLocalisation.transform);
                        count = Random.Range(0, EnemysGame.Count);
                        if (SpawenLocalisation.transform.childCount < 200)
                                (Instantiate(EnemysGame[count], pos(), EnemysGame[count].transform.rotation) as GameObject).transform.SetParent(SpawenLocalisation.transform);
                        count = Random.Range(0, EnemysGame.Count);
                        if (SpawenLocalisation.transform.childCount < 200)
                                (Instantiate(EnemysGame[count], pos(), EnemysGame[count].transform.rotation) as GameObject).transform.SetParent(SpawenLocalisation.transform);
                        count = Random.Range(0, EnemysGame.Count);
                        if (SpawenLocalisation.transform.childCount < 200)
                                (Instantiate(EnemysGame[count], pos(), EnemysGame[count].transform.rotation) as GameObject).transform.SetParent(SpawenLocalisation.transform);
                    }
                yield return new WaitForSeconds(Random.Range(0.5f,1.5f));
            }
            StartCoroutine(SpaweningManager());
        }
    }
    Vector3 pos() 
    {
        if (inspos.Count==1)
        {
            return inspos[0].transform .position;
        }
        return new Vector3(Random.Range(inspos[0].transform.position.x, inspos[1].transform.position.x), inspos[0].transform.position.y, inspos[0].transform.position.z);
    }
}
