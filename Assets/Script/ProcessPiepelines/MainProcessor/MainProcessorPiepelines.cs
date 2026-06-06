using Script.ProcessPiepelines.IntentPipelines;
using Unity.Entities;
using UnityEngine;

namespace Script.ProcessPiepelines.MainProcessor
{
    public class MainProcessorPiepelines
    {
        private Data.PlayerRunTimeData _runtimeData;
        private Player.PlayerController _player;
        private InputPiepelines.InputPiepelines _inputPiepelines;
        
        // 主处理管线持有的子处理管线
        private ViewRotationProcessor viewRotationProcessor;
        private MovementProcessor movementProcessor;

        public MainProcessorPiepelines(Player.PlayerController player)
        {
            _player = player;
            _runtimeData = _player.playerRunTimeData;
            _inputPiepelines = _player.inputPiepelines;
            
            viewRotationProcessor = new ViewRotationProcessor(_runtimeData);
            movementProcessor = new MovementProcessor(_runtimeData);
        }

        // 自定义驱动流转函数
        public void Update()
        {
            ref readonly InputData.ProcessedInputData inputSnapShot = ref _inputPiepelines.current.currentFrameData.processedInputData;
            viewRotationProcessor.Update(inputSnapShot);
            movementProcessor.Update(inputSnapShot);
        }
    }
}