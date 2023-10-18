using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���E�Ɉړ�����A�ʕ��𗎂Ƃ�
public class Player : MonoBehaviour
{
    // �ړ��ɕK�v�Ȃ���
    float x;
    float speed = 3;

    [SerializeField] GameObject[] fruits;
    [SerializeField] Transform spawnPoint;

    GameObject fruitsObj;
    bool isFall;

    int num = 0;

    // ���ɂ���t���[�c�̉摜
    [SerializeField]Image nextFruits;
    int nextFruitsNum;

    [SerializeField] Color32[] colors;

    private void Start()
    {
        // �t���[�c��SpawnPoint�̈ʒu�ɐ�������
        fruitsObj = Instantiate(fruits[0], spawnPoint.position, Quaternion.identity, this.transform);
        fruitsObj.GetComponent<Fruits>().fruitsNum = num;
        num++;

        // ���ɂ���t���[�c�̉摜��\������
        int r = Random.Range(0, fruits.Length);
        nextFruitsNum = r;
        nextFruits.color = colors[nextFruitsNum];
    }

    // 0.02�b���炢��1�x�����ŌĂ΂��
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");

        // �X�y�[�X�L�[���������Ƃ��ɉʕ��𗎂Ƃ�
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isFall == false)
            {
                isFall = true;
                StartCoroutine(FruitsDown());
            }
        }

        // �E�Ɉړ�
        if (x > 0)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
        // ���Ɉړ�
        else if (x < 0)
        {
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }
    }

    IEnumerator FruitsDown()
    {
        // �t���[�c�ɏd�͂�^����
        fruitsObj.GetComponent<Rigidbody2D>().gravityScale = 3;

        // Player�̎q�v�f����O���Ă�����
        fruitsObj.transform.SetParent(null);

        // ������Ǝ��Ԃ�҂��Ă��炤
        yield return new WaitForSeconds(0.3f);

        // ���̃t���[�c���Z�b�g����
        fruitsObj = Instantiate(fruits[nextFruitsNum], spawnPoint.position, Quaternion.identity, this.transform);
        fruitsObj.GetComponent<Fruits>().fruitsNum = num;
        num++;

        yield return null;

        int r = Random.Range(0, fruits.Length);
        nextFruitsNum = r;
        nextFruits.color = colors[nextFruitsNum];

        // �܂����Ƃ���悤�ɂ���
        isFall = false;
    }
}
