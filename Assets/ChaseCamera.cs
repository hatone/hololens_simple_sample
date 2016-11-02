using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class ChaseCamera : MonoBehaviour {
    private Camera mainCamera;
    private Rigidbody rigid;
    private Animator animator;
    private bool isWalking = false;

    // Use this for initialization
    void Start () {
        this.mainCamera = Camera.main;
        this.rigid = this.GetComponent<Rigidbody>();
        this.animator = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        var frontPosition = this.mainCamera.transform.position + this.mainCamera.transform.forward * 2.25f;
        frontPosition.y = 0f;
        
        var distance = Vector3.Distance(frontPosition, transform.position);

        if (!this.isWalking)
        {

            this.rigid.velocity = Vector3.zero;
            this.transform.LookAt(this.mainCamera.transform.position);

            // 距離が 0.3 Unit を超える場合
            if (distance > 0.3f)
            {
                // 歩行中にして歩行アニメーションに遷移
                this.isWalking = true;
                this.animator.SetBool("walking", true);
            }
            return;
        }

        this.transform.LookAt(frontPosition);
        if (distance > 0.1f)
        {
            // 移動方向を修正してさらに移動させる
            this.rigid.velocity = transform.TransformDirection(Vector3.forward) * 1.2f;
            return;
        }

        this.isWalking = false;
        this.animator.SetBool("walking", false);
   
    }
}
