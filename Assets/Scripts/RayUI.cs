using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using wvr;

public class RayUI : MonoBehaviour
{
    public GameObject mainPanel;//开始的主菜单
    public GameObject instructionPanel;//介绍面板

    private WVR_DeviceType right = WVR_DeviceType.WVR_DeviceType_Controller_Right;
    private WVR_InputId Touchpad = WVR_InputId.WVR_InputId_Alias1_Touchpad;
    private Transform m_transform;
    private RaycastHit hit;

    // Use this for initialization
    void Start()
    {
        m_transform = this.GetComponent<Transform>();
        mainPanel.SetActive(true);
        instructionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RayOn();
    }

    void RayOn()
    {
        if (Physics.Raycast(m_transform.position, m_transform.forward, out hit, Mathf.Infinity, LayerMask.GetMask("UI"))) {
            if (hit.collider.tag == "end") {
                Debug.Log("END");
                if (WaveVR_Controller.Input(right).GetPress(Touchpad) || Input.GetKey("e")) {
                    Application.Quit();
                }
                    
            }
            if (hit.collider.tag == "start") {
                Debug.Log("START");
                if (WaveVR_Controller.Input(right).GetPress(Touchpad) || Input.GetKey("s")) {
                    SceneManager.LoadScene("Scenes/Main");
                }
            }
            if (hit.collider.tag == "instruction") {
                Debug.Log("INSTRUCTION");
                if (WaveVR_Controller.Input(right).GetPress(Touchpad) || Input.GetKey("i")) {
                    mainPanel.SetActive(false);//隐藏主面板
                    instructionPanel.SetActive(true);//显示玩法说明面板
                }
            }
            if (hit.collider.tag == "back") {
                Debug.Log("BACK");
                if (WaveVR_Controller.Input(right).GetPress(Touchpad) || Input.GetKey("b")) {
                    mainPanel.SetActive(true);//显示主面板
                    instructionPanel.SetActive(false);//隐藏玩法说明面板
                }
            }

        }
    }
}
