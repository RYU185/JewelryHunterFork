using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    public float length = 0.0f; // �� ���̾ȿ� �÷��̾ ������ ���Ͻ�Ŵ
    public float distance = 0.0f; // ���� �÷��̾���� �ǽð� �Ÿ�
    bool isFell = false;
    Rigidbody2D rbody;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Kinematic;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // �÷��̾���� distance ���
            Vector2 diff = this.transform.position - player.transform.position;
            //distance = Mathf.Sqrt((diff.x * diff.x) + (diff.y * diff.y));
            //if (distance <= length)
            distance = (diff.x * diff.x) + (diff.y * diff.y);
            if (distance <= (length*length)) // �� ��쿡�� �������� ���ϴ°��� �� ����
            {
                rbody.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
