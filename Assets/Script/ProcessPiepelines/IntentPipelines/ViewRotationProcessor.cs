using UnityEngine;

namespace Script.ProcessPiepelines.IntentPipelines
{
    // 视角移动意图处理管线
    public class ViewRotationProcessor
    {
        private Data.PlayerRunTimeData _runTimeData;

        public ViewRotationProcessor(Data.PlayerRunTimeData runTimeData)
        {
            _runTimeData = runTimeData;
        }
        
        // 传入只读的当帧处理后数据
        public void Update(in InputData.ProcessedInputData inputData)
        {
            Vector2 lookInput = inputData.Look;
            if (lookInput.sqrMagnitude > 0.000001f)
            {
                float yaw = lookInput.x;
                float pitch = lookInput.y;
                
                // 更新黑板数据
                _runTimeData.ViewYaw += yaw;
                _runTimeData.ViewPitch += pitch;
                
                // 做钳制
                _runTimeData.ViewYaw = Mathf.Repeat(_runTimeData.ViewYaw, 360f); // 水平角只能在360到0之间循环
                _runTimeData.ViewPitch = Mathf.Clamp(_runTimeData.ViewPitch, -80f, 80f); // 俯仰角限制在-80到80之间
            }
        }
    }
}