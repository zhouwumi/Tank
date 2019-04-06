using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float moveSpeed;

    private SpriteRenderer spriteRender;
    public Sprite[] tankSprites;  //上 右 下 左

    public GameObject bulletPrefab;
    public GameObject shildPrefab;
    public GameObject explosionPrefab;

    public float sendBulletTime = 0.4f;
    private float curBulletTime = 0.0f;

    public float defendedTime = 4.0f;
    public bool isInDefended = true;

    private Const.Direction _direction = Const.Direction.TOP;

    private GameObject defenedGameObject;

    public AudioClip moveAudioClip;
    public AudioClip ildeAudioClip;

    public AudioSource audioSource;

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        defenedGameObject = Instantiate(shildPrefab, transform);
    }
	
	// Update is called once per frame
	void Update () {
        UpdateDefened();
    }

    void UpdateDefened()
    {
        if(isInDefended)
        {
            defendedTime -= Time.deltaTime;
            if (defendedTime <= 0.0f)
            {
                if (defenedGameObject)
                {
                    isInDefended = false;
                    Destroy(defenedGameObject);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (PlayerManager.Instance.IsEnd)
        {
            return;
        }
        Move();
        TrySendBullet();
    }

    private void Attack()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
            //第一种方式调整子弹的方向，不调整子弹的旋转，改变子弹的SpriteRender的sprite，但是给子弹设置运动方向
            //GameObject bulletObject = Instantiate(bulletPrefab, transform.position, transform.rotation);
            //bulletObject.GetComponent<Bullet>().SetDirection(_direction);


            //第二种方式调整子弹的方向，改变子弹的旋转，不改变子弹的SpriteRender的sprite，不给子弹设置运动方向
            float addRotation = 0;
            if(_direction == Const.Direction.TOP)
            {
                addRotation = 0;
            }else if(_direction == Const.Direction.RIGHT)
            {
                addRotation = -90;
            }else if(_direction == Const.Direction.DOWN)
            {
                addRotation = 180;
            }else if(_direction == Const.Direction.LEFT)
            {
                addRotation = 90;
            }
            GameObject bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + addRotation));
            bulletObject.GetComponent<Bullet>().isMe = true;
            bulletObject.GetComponent<Bullet>().PlayAudio();
        //}
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //第一种方式
        //transform.Translate(new Vector3(h * Time.deltaTime * moveSpeedX, v * Time.deltaTime * moveSpeedY, 0));

        //第二种方式
        //transform.Translate(h * Time.deltaTime * moveSpeedX, v * Time.deltaTime * moveSpeedY, 0);

        //第三种方式 
        //transform.Translate(h * Vector3.right * moveSpeedX * Time.deltaTime, Space.Self);
        //transform.Translate(v * Vector3.up * moveSpeedY * Time.deltaTime, Space.Self);

        //上面三种方式都是以自身为坐标系移动，当物体自身有旋转时，沿着自己局部坐标系下的xy轴运动//

        //第四种方式  当自己有旋转的时候，和上面三种方式就有很明显的区别.
        //transform.Translate(h * Vector3.right * moveSpeedX * Time.deltaTime, Space.World);
        //transform.Translate(v * Vector3.up * moveSpeedY * Time.deltaTime, Space.World);

        if(h != 0 || v != 0)
        {
            if(h >= 0.05f || v >= 0.05f)
            {
                audioSource.clip = moveAudioClip;
            }
            else
            {
                audioSource.clip = ildeAudioClip;
            }
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        if (h != 0)
        {
            transform.Translate(h * Vector3.right * moveSpeed * Time.fixedDeltaTime, Space.World);
            if (h > 0)
            {
                spriteRender.sprite = this.tankSprites[1];
                _direction = Const.Direction.RIGHT;
            }
            else
            {
                spriteRender.sprite = tankSprites[3];
                _direction = Const.Direction.LEFT;
            }

        }
        else if (v != 0)
        {
            transform.Translate(v * Vector3.up * moveSpeed * Time.fixedDeltaTime, Space.World);
            if (v > 0)
            {
                spriteRender.sprite = tankSprites[0];
                _direction = Const.Direction.TOP;
            }
            else
            {
                spriteRender.sprite = tankSprites[2];
                _direction = Const.Direction.DOWN;
            }
        }
    }

    private void TrySendBullet()
    {
        if(curBulletTime >= sendBulletTime)
        {
            curBulletTime -= sendBulletTime;
            Attack();
        }
        else
        {
            curBulletTime += Time.fixedDeltaTime;
        }
    }

    public void Die()
    {
        if(this.isInDefended)
        {
            return;
        }
        PlayerManager.Instance.lastPlayerPosition = transform.position;
        PlayerManager.Instance.lastPlayerQuaternion = transform.rotation;
        PlayerManager.Instance.isDead = true;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
