using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wvr;
using UnityEngine.UI;

public class UIManage : MonoBehaviour {
    
    public GameObject canvas;//画布对象
    public GameObject mainMenuPanel;//主菜单面板
    public GameObject surroundingPanel;//环境选择面板
    private Vector3 CANVAS_START_POSITION = new Vector3(0, 40f, -50f);//画布初始位置
    
    //天空盒材质
    public Material mat1;
    public Material mat2;
    public Material mat3;

    private WVR_DeviceType right = WVR_DeviceType.WVR_DeviceType_Controller_Right;
    private WVR_InputId Touchpad = WVR_InputId.WVR_InputId_Alias1_Touchpad;
    private WVR_InputId Menu = WVR_InputId.WVR_InputId_Alias1_Menu;
    private GameObject controllerR;//手柄
    private RaycastHit hit;

    private enum tags { backbutton,exitbutton,surroundingselectbutton,
                        daybutton,sunsetbutton,nightbutton,backmainmenubutton};

    private void Start() {
        canvas.SetActive(false);//进入场景时默认显示画布
        mainMenuPanel.SetActive(true);//进入场景时默认显示主菜单
        surroundingPanel.SetActive(false);//进入场景是默认关闭环境选择菜单
        
        controllerR = GameObject.Find("Player/WaveVR/Generic_MC_R");//获取手柄，以便控制手柄和球拍模型的切换
    }

    // Update is called once per frame
    void Update() {
        //画布处于显示状态时，隐藏球拍，显示射线
        if (canvas.activeInHierarchy) {
            GameObject.Find("Player").GetComponent<Move>().enabled = false;//关闭任务移动脚本，防止按键干涉
            controllerR.transform.Find("bat").gameObject.SetActive(false);
            controllerR.transform.Find("BeamR").gameObject.SetActive(true);
            RayOn();//射线交互
        } else {
            GameObject.Find("Player").GetComponent<Move>().enabled = true;
            controllerR.transform.Find("bat").gameObject.SetActive(true);
            controllerR.transform.Find("BeamR").gameObject.SetActive(false);
        }

        //按菜单键调出画布
        if (WaveVR_Controller.Input(right).GetPress(Menu) || Input.GetKey("m")) {
            canvas.SetActive(true);
            
            controllerR.transform.Find("bat").gameObject.SetActive(false);
            controllerR.transform.Find("BeamR").gameObject.SetActive(true);
        }
    
    }

    void RayOn() {
        //在UI层射线检测        
        if ( Physics.Raycast(controllerR.transform.position, controllerR.transform.forward, out hit, Mathf.Infinity, LayerMask.GetMask("UI")) 
            && (WaveVR_Controller.Input(right).GetPress(Touchpad) || Input.GetKey("n")) ) {
            //Debug.Log("rayon");
            switch (hit.collider.tag) {
                case "backbutton":
                    backButtonClick();
                    break;
                case "exitbutton":
                    exitButtonClick();
                    break;
                case "surroundingselectbutton":
                    surroundingButtonClick();
                    break;
                case "daybutton":
                    dayButtonClick();
                    break;
                case "sunsetbutton":
                    sunsetButtonClick();
                    break;
                case "nightbutton":
                    nightButtonClick();
                    break;
                case "backmainmenubutton":
                    backMainMenuButtonClick();
                    break;
            }
        }
    }
   
    //主菜单的环境选择按钮
    public void surroundingButtonClick() {
        mainMenuPanel.SetActive(false);//隐藏主菜单
        surroundingPanel.SetActive(true);//打开环境选择面板
    }

    //主菜单的退出按钮
    public void exitButtonClick() {
        Application.Quit();//退出应用
    }

    //主菜单的返回按钮
    public void backButtonClick() {
        canvas.SetActive(false);

    }

    //环境面板
    public void dayButtonClick() {
        changeSkybox(mat1);
    }
    public void sunsetButtonClick() {
        changeSkybox(mat2);
    }
    public void nightButtonClick() {
        changeSkybox(mat3);
    }

    
    public void backMainMenuButtonClick() {
        mainMenuPanel.SetActive(true);//显示主菜单
        surroundingPanel.SetActive(false);
    }

    public void changeSkybox(Material mat) {
        RenderSettings.skybox = mat;
        DynamicGI.UpdateEnvironment();
    }


}
