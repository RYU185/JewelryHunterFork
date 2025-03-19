using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float deleteTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        // deleteTime�� ������ ������ �ı�
        Destroy(this.gameObject, deleteTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // �������ϰ� �ε�ġ�� ��� ����
        Destroy(this.gameObject);
    }
}
