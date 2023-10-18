using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 左右に移動する、果物を落とす
public class Player : MonoBehaviour
{
    // 移動に必要なもの
    float x;
    float speed = 3;

    [SerializeField] GameObject[] fruits;
    [SerializeField] Transform spawnPoint;

    GameObject fruitsObj;
    bool isFall;

    int num = 0;

    // 次にくるフルーツの画像
    [SerializeField]Image nextFruits;
    int nextFruitsNum;

    [SerializeField] Color32[] colors;

    private void Start()
    {
        // フルーツをSpawnPointの位置に生成する
        fruitsObj = Instantiate(fruits[0], spawnPoint.position, Quaternion.identity, this.transform);
        fruitsObj.GetComponent<Fruits>().fruitsNum = num;
        num++;

        // 次にくるフルーツの画像を表示する
        int r = Random.Range(0, fruits.Length);
        nextFruitsNum = r;
        nextFruits.color = colors[nextFruitsNum];
    }

    // 0.02秒くらいに1度自動で呼ばれる
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");

        // スペースキーを押したときに果物を落とす
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isFall == false)
            {
                isFall = true;
                StartCoroutine(FruitsDown());
            }
        }

        // 右に移動
        if (x > 0)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
        // 左に移動
        else if (x < 0)
        {
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }
    }

    IEnumerator FruitsDown()
    {
        // フルーツに重力を与える
        fruitsObj.GetComponent<Rigidbody2D>().gravityScale = 3;

        // Playerの子要素から外してあげる
        fruitsObj.transform.SetParent(null);

        // ちょっと時間を待ってもらう
        yield return new WaitForSeconds(0.3f);

        // 次のフルーツをセットする
        fruitsObj = Instantiate(fruits[nextFruitsNum], spawnPoint.position, Quaternion.identity, this.transform);
        fruitsObj.GetComponent<Fruits>().fruitsNum = num;
        num++;

        yield return null;

        int r = Random.Range(0, fruits.Length);
        nextFruitsNum = r;
        nextFruits.color = colors[nextFruitsNum];

        // また落とせるようにする
        isFall = false;
    }
}
