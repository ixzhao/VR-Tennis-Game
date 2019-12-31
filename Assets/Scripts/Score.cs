using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wvr;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    [HideInInspector]
    public int score=0;
    [HideInInspector]
    public int turnTotalCount=0;
    [HideInInspector]
    public Text text;
	// Use this for initialization
	void Start () {
        text = GameObject.Find("Player/WaveVR/head/ScoreCanvas/ScoreText").transform.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		text.text = "当前分数 : " + score.ToString() + "/" + turnTotalCount.ToString();
    }

    //分数统计
    public void scoreCount(int pCourtCollCount, int netCollCount, int fCourtCollCount, int batCollCount) {
        
        Debug.Log("分数测试"+pCourtCollCount+ netCollCount + fCourtCollCount + batCollCount);
        if (pCourtCollCount == 1 && batCollCount == 1 && fCourtCollCount == 1) {
            score++;
        }
        Debug.Log(score);
    }
    //统计回合数
    public void turnCount(int pCourtCollCount, int netCollCount, int fCourtCollCount, int batCollCount) {
        int i = pCourtCollCount + netCollCount + fCourtCollCount + batCollCount;
        switch (i) {
            case 0://发球出界
                break;
            case -4://球碰地前接球
                turnTotalCount--;
                break;
            default:
                turnTotalCount++;
                break;
        }
    }

}
