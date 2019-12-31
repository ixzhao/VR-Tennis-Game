using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wvr;

public class Ball : MonoBehaviour {
    [HideInInspector]
    public int pCourtCollCount=0;//玩家场地碰撞次数
    [HideInInspector]
    public int fCourtCollCount=0;//发球场地碰撞次数
    public int batCollCount=0;
    public int netCollCount=0;

    public AudioSource BatHitSd;
    public AudioSource CourtHitSd;
    private ParticleSystem ptl;


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        
        ballStatusCheck();
        ptl = GetComponentInChildren<ParticleSystem>();

    }

    //碰撞检测
    private void OnCollisionEnter(Collision collision) {
        
        //碰到网
        if (collision.gameObject.tag == "net") {
            //若是发球失败，立即重新发球，不进行分数统计
            if (pCourtCollCount == 0 && fCourtCollCount == 0 && batCollCount == 0) {
                GameObject.Find("BallManager").GetComponent<BallManager>().Initial();
                Destroy(gameObject);
            } else {
                netCollCount++;
            }          
        }

        //碰到发球场地
        if (collision.gameObject.tag == "faqiucourt") {
            //若是发球失败，立即重新发球，不进行分数统计
            if (pCourtCollCount == 0 && netCollCount == 0 && batCollCount == 0) {
                GameObject.Find("BallManager").GetComponent<BallManager>().Initial();
                Destroy(gameObject);
            } else {
                fCourtCollCount++;
                //回球第一次碰撞发球场地时就需计分判断，后续的无法正确判断；也只能在这种情况下需要计分判断，其余只统计回合数
                if (fCourtCollCount == 1) {
                    GameObject.Find("BallManager").GetComponent<Score>().scoreCount(pCourtCollCount, netCollCount, fCourtCollCount, batCollCount);
                }
            }
            
        }

        //碰到玩家场地，触地反弹
        if (collision.gameObject.tag == "playercourt") {
            pCourtCollCount++;
            CourtEffect();
            if (true) {
                ContactPoint contactPoint = collision.contacts[0];//碰撞点
                Vector3 inDir = gameObject.GetComponent<Rigidbody>().velocity;//碰撞时球的速度向量即为入射方向
                Vector3 outDir = Vector3.Reflect(inDir, contactPoint.normal);//入射向量反射为等模的反射向量
                gameObject.GetComponent<Rigidbody>().velocity = outDir;//改变网球速度方向为反射方向
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,1,0)*3f, ForceMode.Impulse);
            }
        }

        //触拍反弹
        if (collision.gameObject.tag == "bat") {
            BatEffect();
            //球落地前接球，需要重新发球
            if (pCourtCollCount == 0) {
                //统计时单独处理
                GameObject.Find("BallManager").GetComponent<Score>().turnCount(-1, -1, -1, -1);
            } else {
                batCollCount++;

                //ContactPoint contactPoint = collision.contacts[0];//碰撞点
                //Vector3 inDir = gameObject.GetComponent<Rigidbody>().velocity;//碰撞时球的速度向量即为入射方向
                //Debug.Log(inDir);
                //Debug.Log("pengpengpeng"+contactPoint.normal);
                //Vector3 outDir = Vector3.Reflect(inDir, contactPoint.normal);//入射向量反射为等模的反射向量
                //gameObject.GetComponent<Rigidbody>().velocity = outDir*1.2f;//改变网球速度方向为反射方向
                //gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,1,0.5f)*2f, ForceMode.Impulse);

                //Debug.Log(outDir);

                //演示所用代码
                Transform m_bat = GameObject.Find("Player/WaveVR/Generic_MC_R/bat").GetComponent<Transform>();
                Vector3 moveDirection;
                if (Vector3.Dot(m_bat.up, this.GetComponent<Transform>().position - m_bat.position) < 0) {
                    moveDirection = -m_bat.up;
                    moveDirection.y = 0;
                } else {
                    moveDirection = m_bat.up;
                    moveDirection.y = 0;
                }

                Rigidbody bRig = this.GetComponent<Rigidbody>();
                if (m_bat.position.z < -140)//后场
                {
                    Debug.Log("后场");
                    bRig.AddForce(moveDirection * 1f, ForceMode.Impulse);
                    Debug.Log(moveDirection);
                    bRig.AddForce(Vector3.up * 2f, ForceMode.Impulse);
                } else//前场
                  {
                    Debug.Log("前前前");
                    bRig.AddForce(moveDirection * 0.6f, ForceMode.Impulse);
                    Debug.Log(moveDirection);
                    bRig.AddForce(Vector3.up * 1.5f, ForceMode.Impulse);
                }

            }

        }

        //if (collision.gameObject.tag == "boundary") {
        //    endThisTurn();
        //}

    }

    //检测球的状态，当出边界或者网球静止时，进行计分判断并重发球。
    void ballStatusCheck() {
        
        if (System.Math.Abs(gameObject.transform.position.x) > 250 //出边界检测
            || (gameObject.transform.position.y < -30 || gameObject.transform.position.y > 200)
            || System.Math.Abs(gameObject.transform.position.z) > 300
            || this.gameObject.GetComponent<Rigidbody>().IsSleeping() //网球已经静止
            || pCourtCollCount>2 || fCourtCollCount>2) { //防止球长时间弹跳

            endThisTurn();
        }
        
    }

    //结束本回合的接发球
    void endThisTurn() {
        //分数统计
        GameObject.Find("BallManager").GetComponent<Score>().turnCount(pCourtCollCount, netCollCount, fCourtCollCount, batCollCount);
        //重新发球
        GameObject.Find("BallManager").GetComponent<BallManager>().Initial();
        //销毁当前网球实例
        Destroy(gameObject);
    }



    //声音和特效
    //球与球拍碰撞效果
    void BatEffect() {
        //GetComponent<Light>().enabled = true;
        ptl.Play();
        BatHitSd.Play();
        Invoke("ClearEffect", 0.5f);//延迟0.5秒后关闭
    }
    //球与地面碰撞效果
    void CourtEffect() {
        CourtHitSd.Play();
        Invoke("ClearEffect", 0.5f);
    }
    //关闭特效
    void ClearEffect() {
        //GetComponent<Light>().enabled = false;
        ptl.Stop();
        BatHitSd.Stop();
        CourtHitSd.Stop();
    }

}
