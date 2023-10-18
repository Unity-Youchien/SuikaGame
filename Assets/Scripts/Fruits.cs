using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    // �������Ȃ�̃t���[�c�Ȃ̂����o�������Ă��炤
    public enum TYPE
    {
        Cherry,
        Melon,
        Suika,
        BlueBerry,
        Tofu,
    }
    public TYPE type = TYPE.Cherry;

    // �����̎��̐i����̃t���[�c�������Ă���
    [SerializeField] GameObject nextFruits;

    // �t���[�c�ɒʂ��ԍ������悤
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

    // �t���[�c���m���Ԃ������Ƃ��̏���
    // �R���C�_�[�����Ă���̂ɂԂ������Ƃ��Ɏ����ŌĂ΂��֐�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isRotate = true;
        // �t���[�c�ɓ���������`
        if (collision.gameObject.tag == "Fruits")
        {
            Debug.Log("�t���[�c����I");

            /* // ���̂�̕ʂ̏�����
            Fruits fruitsCS = collision.gameObject.GetComponent<Fruits>();
            TYPE colType = fruitsCS.type;
            int colNum = fruitsCS.fruitsNum;*/

            // �����Ɠ����t���[�c��������`
            TYPE colType = collision.gameObject.GetComponent<Fruits>().type;
            int colNum = collision.gameObject.GetComponent<Fruits>().fruitsNum;

            if (type == colType)
            {
                // MAX���x���̃t���[�c�̏���
                if (type == TYPE.Suika)
                {
                    // �������g�͏�����
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    // ���ʂ̃t���[�c�͍��̂���OK
                    // �ʂ��ԍ����r���āA�傫���q�ɏ���������Ă��炤
                    if (fruitsNum > colNum)
                    {
                        // �t���[�c���m�����̂����āA�i�������t���[�c�𐶐�����
                        GameObject fruits = Instantiate(nextFruits, transform.position, transform.rotation);
                        // �d�͂�ݒ肵�Ă�����
                        fruits.GetComponent<Rigidbody2D>().gravityScale = 3;
                        fruits.GetComponent<Fruits>().fruitsNum = fruitsNum;

                        // �������g�͏�����
                        Destroy(collision.gameObject);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

}
