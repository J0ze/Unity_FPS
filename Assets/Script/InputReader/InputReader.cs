using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.InputReader
{
    /// <summary>
    /// 输入读取接口
    /// 负责读取硬件输入 转化为生输入数据 与上层解耦 不负责任何输入处理
    /// </summary>
    public class InputReader : MonoBehaviour, IInputReader
    {
        [Header("基础位移输入引用")]
        public InputActionReference  moveAction;
        public InputActionReference  lookAction;
        
        [Header("鼠标输入设定")]
        [Tooltip("是否反转X")]
        public bool inverseX = false;
        
        [Tooltip("是否反转Y")]
        public bool inverseY = false;
        
        [Tooltip("鼠标灵敏度")]
        public float mouseSensitivity = 1.0f;

        private void OnEnable()
        {
            ActionTrigger(true);
        }

        private void OnDisable()
        {
            ActionTrigger(false);
        }

        #region 核心接口实现

        public void FetchInput(ref InputData.RawInputData rawInputData)
        {
            // 直接获取移动输入相对于摄像机视角的偏移量
            rawInputData.RawMoveAxis = moveAction.action.ReadValue<Vector2>();
            // 考虑到鼠标DPI的以及屏幕坐标与Unity的左手坐标系导致上抬头视角下移问题，我们对Look输入进行初步加工
            Vector2 rawLookAxis = lookAction.action.ReadValue<Vector2>(); 
            rawLookAxis.x *= inverseX ? -1 : 1;
            rawLookAxis.y *= inverseY ? -1 : 1;
            rawInputData.RawLookAxis = rawLookAxis *  mouseSensitivity;
        }
        #endregion
    

        #region 辅助函数
        /// <summary>
        /// 初始化输入系统
        /// </summary>
        private void ActionTrigger(bool enable)
        {
            InputActionReference[] inputActions =
            {
                moveAction,
                lookAction
            };
            // 激活所有Action
            foreach (InputActionReference inputActionReference in inputActions)
            {
                if (inputActionReference != null)
                {
                    if (enable)
                    {
                        inputActionReference.action.Enable();
                    }
                    else
                    {
                        inputActionReference.action.Disable();
                    }
                }
                else
                {
                    Debug.LogError($"{inputActionReference.name} 的引用丢失，请检查Inspector面板");
                }
            }
        }
        #endregion
    }
}
