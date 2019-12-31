using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wvr;

public class BallManager : MonoBehaviour {

    public GameObject ball;//网球prefab
    private GameObject ball_instance;//自动生成的网球实例
    private Rigidbody bRig;//网球实例的刚体组件
    private Vector3 bInitPos;//网球初始位置
    private Vector3 bInitDir;//网球发射方向
    RaycastHit hit;


    // Use this for initialization
    void Start() {
        Initial();
    }

    // Update is called once per frame
    void Update() {
        
    }

    //重新开始发球
    public void Initial() {
        
        RandomPos();
        Serving();
    }

    //在指定区域的随机位置生成网球，作为网球的初始位置
    void RandomPos() {
        bInitPos.x = Random.Range(-Constant.COURT_W / 6, Constant.COURT_W / 6);
        bInitPos.z = Random.Range(Constant.COURT_L / 6, Constant.COURT_L / 4);
        bInitPos.y = Random.Range(45f, 50f);
        ball_instance = Instantiate(ball, bInitPos, Quaternion.identity);
        RandomDir();     
    }

    //生成随机方向（速度）
    void RandomDir() {
        bInitDir.x = Random.Range(-0.15f, 0.15f);
        bInitDir.z = Random.Range(-0.8f, -0.6f);
        bInitDir.y = Random.Range(-0.15f, -0.05f);//只有下压的发球，发球会有一定的触网率。
        bInitDir = bInitDir.normalized;  
    }

    //发球前检测发射方向是否触网或者出界，降低发球失误率
    void PreServing() {
        //Ray ray = new Ray(bInitPos, bInitDir);//以发射方向和网球初始位置生成射线
        if (Physics.Raycast(bInitPos, bInitDir, out hit)) {
            if (hit.collider.gameObject.tag == "playercourt") {
                return;//碰到玩家场地时进行发球
            }
        } else {
            RandomDir();//其他情况重新生成方向
        }        
    }

    //发球
    void Serving() {
        PreServing();
        bRig = ball_instance.GetComponent<Rigidbody>();//脚本挂在prefab下，需要获取的组件是实例下的，而不是prefab下的。
        //给网球一个初速度，以下都能实现。
        //bRig.velocity=bInitDir*500f;//赋予初速度
        bRig.AddForce(bInitDir*6f,ForceMode.Impulse);//使用网球的重力，添加一个瞬时的力
    }
  
  
    
}
