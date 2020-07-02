using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool IsLocalPlayer = false;
    Vector2 OldPos;
    Vector2 CurPos;

    public float MoveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        CurPos = this.transform.position;
        OldPos = CurPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer) {
            return;
        }

        Move();
    }
    void Move()
    {
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * MoveSpeed;

        transform.Translate(dir.x, dir.y, 0);

        CurPos = this.transform.position;

        if (CurPos != OldPos)
        {
            //네트워크에 적용
            OldPos = CurPos;
        }
    }
}
