using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float moveSpeed;

    private SpriteRenderer spriteRender;
    public Sprite[] tankSprites;  //上 右 下 左

    public GameObject bulletPrefab;

    public float sendBulletTime = 2.0f;
    private float curBulletTime = 0.0f;

    public float _changeDirectionTime = 4.0f;
    private float _currentDirectionTimeVal = 0.0f;

    private Const.Direction _direction = Const.Direction.TOP;

    private GameObject defenedGameObject;

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Move();
        TrySendBullet();
    }

    private void Attack()
    {
        //第二种方式调整子弹的方向，改变子弹的旋转，不改变子弹的SpriteRender的sprite，不给子弹设置运动方向
        GameObject bulletObject = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletObject.GetComponent<Bullet>().isMe = false;
    }

    private void Move()
    {
        _currentDirectionTimeVal += Time.fixedDeltaTime;
        if(_currentDirectionTimeVal >= _changeDirectionTime)
        {
            int direction = Random.Range(0, 4);
            if(direction == 0)
            {
                _direction = Const.Direction.TOP;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }else if(direction == 1)
            {
                _direction = Const.Direction.RIGHT;
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if(direction == 2)
            {
                _direction = Const.Direction.DOWN;
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if(direction == 3)
            {
                _direction = Const.Direction.LEFT;
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            _currentDirectionTimeVal = 0.0f;
        }

        transform.Translate(transform.up * moveSpeed * Time.fixedDeltaTime, Space.World);
    }

    private void TrySendBullet()
    {
        if (curBulletTime >= sendBulletTime)
        {
            curBulletTime -= sendBulletTime;
            Attack();
        }
        else
        {
            curBulletTime += Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "enemy":
                //_currentDirectionTimeVal = _changeDirectionTime;
                gameObject.transform.Rotate(new Vector3(0, 0, -90));
                // collision.gameObject.transform.Rotate(new Vector3(0, 0, -90));
                break;
            case "board_wall":
                gameObject.transform.Rotate(new Vector3(0, 0, 180));
                break;
            default:
                _currentDirectionTimeVal = _changeDirectionTime;
                break;
        }
    }
}
