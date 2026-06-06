using Script.Data;
using UnityEngine;
namespace Script.ProcessPiepelines.IntentPipelines
{
    /// <summary>
    /// 角色的移动意图处理器
    /// </summary>
    public class MovementProcessor
    {
        private PlayerRunTimeData _runTimeData;

        public MovementProcessor(PlayerRunTimeData runTimeData)
        {
            _runTimeData = runTimeData;
        }

        public void Update(in InputData.ProcessedInputData inputData)
        {
            Vector2 moveAxis = inputData.Move;
            if (moveAxis.sqrMagnitude >= 0.001f)
            {
                CaculateDesireDirection(moveAxis);
            }
            else
            {
                _runTimeData.DesiredWorldMoveDir = Vector3.zero;
            }
            
            Debug.Log(moveAxis);
        }

        #region 辅助函数

        private void CaculateDesireDirection(Vector2 moveAxis)
        {
            // 得到摄像机的前向向量
            Quaternion verticalForward = Quaternion.Euler(0, _runTimeData.ViewYaw, 0);
            Vector3 forwardDirection = verticalForward * Vector3.forward;
            Vector3 rightDirection = verticalForward * Vector3.right;
            _runTimeData.DesiredWorldMoveDir = (forwardDirection * moveAxis.y + rightDirection * moveAxis.x).normalized;
        }
        

        #endregion
    }
}