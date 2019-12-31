using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wvr;

public class Move : MonoBehaviour {

    private WVR_DeviceType right = WVR_DeviceType.WVR_DeviceType_Controller_Right;
    private WVR_InputId Touchpad = WVR_InputId.WVR_InputId_Alias1_Touchpad;
    
    private Transform p_transform;
    public float speed = 60f;

    // Use this for initialization
    void Start() {
        p_transform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        if (WaveVR_Controller.Input(right).GetPress(WVR_InputId.WVR_InputId_Alias1_DPad_Down) || Input.GetKey(KeyCode.DownArrow)) {
            p_transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
        if (WaveVR_Controller.Input(right).GetPress(WVR_InputId.WVR_InputId_Alias1_DPad_Up) || Input.GetKey(KeyCode.UpArrow)) {
            p_transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (WaveVR_Controller.Input(right).GetPress(WVR_InputId.WVR_InputId_Alias1_DPad_Right) || Input.GetKey(KeyCode.RightArrow)) {
            p_transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (WaveVR_Controller.Input(right).GetPress(WVR_InputId.WVR_InputId_Alias1_DPad_Left) || Input.GetKey(KeyCode.LeftArrow)) {
            p_transform.Translate(-Vector3.right * Time.deltaTime * speed);
        }
    }
}
