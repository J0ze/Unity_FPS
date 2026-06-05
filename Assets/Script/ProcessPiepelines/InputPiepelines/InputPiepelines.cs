using TMPro;
using UnityEngine;

namespace Script.ProcessPiepelines.InputPiepelines
{
    /// <summary>
    /// 输入处理管线 对InputReader读取的生数据进行加工 供意图处理管线判断并写入意图
    /// </summary>
    public class InputPiepelines
    {
        // InputReader的引用
        readonly private InputReader.IInputReader inputReader;
        
        // 长期存储数据
        private InputData.InputDataPack inputData;
        
        //对外暴露的数据接口
        public ref readonly InputData.InputDataPack current => ref inputData;
        
        // 建立栈上数据缓存 避免冗余生成
        // 会做数据清洗 防止意图混淆
        private InputData.RawInputData _rawInputData;
        
        //物理帧计数器
        private ulong _frameIndex;
        
        /// <summary>
        /// 必要依赖注入
        /// </summary>
        /// <param name="inputReader"></param>
        public InputPiepelines(InputReader.IInputReader inputReader)
        {
            this.inputReader = inputReader;
            
            // 初始化栈缓存数据
            inputData = new InputData.InputDataPack();
            inputData.currentFrameData = new InputData.FrameInputData {frameIndex = 0};
            inputData.lastFrameData = new InputData.FrameInputData {frameIndex = 0};

            _rawInputData = default;
        }
        
        // 自定义的Update流转函数
        public void Update()
        {
            // 物理帧流转
            inputData.lastFrameData = inputData.currentFrameData;
            // 调用生数据获取函数获取生数据
            if (inputReader != null)
            {
                inputReader.FetchInput(ref _rawInputData); // 获取数据
            }

            processRawInputData(); // 加工数据
            
            // 处理完当前帧事物后进行帧推进（这里的frameIndex永远指代下一帧的编码
            _frameIndex++;
            
            
        }

        #region 核心函数

        private void processRawInputData()
        {
            // 制作当帧数据
            var currentFrameData = new InputData.FrameInputData
            {
                frameIndex = _frameIndex,
                rawInputData = _rawInputData,
                processedInputData = default
            };
            
            //============= 以下进行当前帧数据的数据处理 ================//
            
            // 位移数据
            // 防抖处理
            if (_rawInputData.RawMoveAxis.sqrMagnitude > 0.01f)
            {
                currentFrameData.processedInputData.Move = _rawInputData.RawMoveAxis;
            }
            else
            {
                currentFrameData.processedInputData.Move = Vector2.zero;
            }
            
            // 旋转输入
            // 鼠标位移输入是增量数据 其记录的是每一帧多位移了多少 直接记录即可
            currentFrameData.processedInputData.Look = _rawInputData.RawLookAxis;
            
            // 写回数据对象
            inputData.currentFrameData = currentFrameData;
        }
        #endregion
    }
}