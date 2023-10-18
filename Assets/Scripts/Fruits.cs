using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    // 自分がなんのフルーツなのか自覚を持ってもらう
    public enum TYPE
    {
        Cherry,
        Melon,
        Suika,
        BlueBerry,
        Tofu,
    }
    public TYPE type = TYPE.Cherry;

    // 自分の次の進化先のフルーツを持っておく
    [SerializeField] GameObject nextFruits;

    // フルーツに通し番号をつけよう
    public int fruitsNum;

    float rotateSpeed;
    bool isRotate;

    private void Start()
    {
        rotateSpeed = Random.Range(0.2f, 1f);
    }

    private void Update()
    {
        if (isRotate)
        {
            transform.Rotate(0, 0, rotateSpeed);
        }
    }

    // フルーツ同士がぶつかったときの処理
    // コライダーがついてるものにぶつかったときに自動で呼ばれる関数
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isRotate = true;
        // フルーツに当たったら～
        if (collision.gameObject.tag == "Fruits")
        {
            Debug.Log("フルーツだよ！");

            /* // 下のやつの別の書き方
            Fruits fruitsCS = collision.gameObject.GetComponent<Fruits>();
            TYPE colType = fruitsCS.type;
            int colNum = fruitsCS.fruitsNum;*/

            // 自分と同じフルーツだったら～
            TYPE colType = collision.gameObject.GetComponent<Fruits>().type;
            int colNum = collision.gameObject.GetComponent<Fruits>().fruitsNum;

            if (type == colType)
            {
                // MAXレベルのフルーツの処理
                if (type == TYPE.Suika)
                {
                    // 自分自身は消える
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    // 普通のフルーツは合体してOK
                    // 通し番号を比較して、大きい子に処理をやってもらう
                    if (fruitsNum > colNum)
                    {
                        // フルーツ同士を合体させて、進化したフルーツを生成する
                        GameObject fruits = Instantiate(nextFruits, transform.position, transform.rotation);
                        // 重力を設定してあげる
                        fruits.GetComponent<Rigidbody2D>().gravityScale = 3;
                        fruits.GetComponent<Fruits>().fruitsNum = fruitsNum;

                        // 自分自身は消える
                        Destroy(collision.gameObject);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

}
