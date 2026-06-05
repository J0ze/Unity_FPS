using System;
using Script.Core.Motion;
using Script.Data;
using Script.ProcessPiepelines.InputPiepelines;
using Script.ProcessPiepelines.MainProcessor;
using UnityEngine;

namespace Script.Player
{
    // 最优先执行 防止相机抽搐
    [DefaultExecutionOrder(-300)]
    public class PlayerController : MonoBehaviour
    {
        [Header("必备依赖组件")]
        [Header("InputReader - 输入读取器")]
        public InputReader.InputReader inputReaderRef;
        
        // 核心数据黑板
        public Data.PlayerRunTimeData playerRunTimeData;
        
        // 核心管线
        public ProcessPiepelines.InputPiepelines.InputPiepelines inputPiepelines { get; private set; }
        public ProcessPiepelines.MainProcessor.MainProcessorPiepelines mainProcessorPiepelines { get; private set; }
        
        // 驱动层组件
        public CharacterController cc { get; private set; }
        
        // 驱动层
        public Core.Motion.MotionDriver motionDriver { get; private set; }
        
        // 骨骼组件
        public GameObject head;
        
        // 初始化
        private void Awake()
        {
            playerRunTimeData = new PlayerRunTimeData();

            if (head == null)
            {
                Debug.LogError("头部未绑定");
            }

            if (inputReaderRef == null)
            {
                Debug.LogError("输入读取器未能成功初始化");
            }
            
            cc = GetComponent<CharacterController>();
            
            // 驱动层初始化
            motionDriver = new MotionDriver(this);
            
            // 管线初始化
            inputPiepelines = new InputPiepelines(inputReaderRef);
            mainProcessorPiepelines = new MainProcessorPiepelines(this);
        }
        
        // 维护的一个唯一的Update
        private void Update()
        {
            inputPiepelines.Update();
            mainProcessorPiepelines.Update();
        }
        
        // 后处理阶段
        private void LateUpdate()
        {
            motionDriver.UpdateMotion();
        }
    }
}