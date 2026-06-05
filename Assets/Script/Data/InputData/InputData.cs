using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class InputData
{
    /// <summary>
    /// 仅提供于InputReader使用的生输入数据
    /// </summary>
    public struct RawInputData
    {
        public Vector2 RawMoveAxis;
        public Vector2 RawLookAxis;
        
    }
    
    /// <summary>
    /// 处理过后的输入数据
    /// </summary>
    public struct ProcessedInputData
    {
        public Vector2 Move;
        public Vector2 Look;
    }
    
    // 帧数据闭包
    public struct FrameInputData
    {
        public ulong frameIndex;
        public RawInputData rawInputData;
        public ProcessedInputData processedInputData;
    }
    
    // 当前帧数据闭包
    public struct InputDataPack
    {
        public FrameInputData currentFrameData;
        public FrameInputData lastFrameData;
    }
}