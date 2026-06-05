using UnityEngine;

namespace Script.Core.Motion
{
    public class MotionDriver
    {
        private readonly Data.PlayerRunTimeData _runTimeData;
        private readonly Player.PlayerController _player;
        private readonly CharacterController _cc;
        private readonly Transform _transform;

        private readonly GameObject _head;

        public MotionDriver(Player.PlayerController player)
        {
            _player = player;
            _runTimeData = _player.playerRunTimeData;
            _cc = _player.cc;
            _head = _player.head;
            _transform = _player.transform;
        }
        

        #region 驱动函数

        public void UpdateMotion()
        {
            // 旋转驱动
            RotationDriver();
        }

        #endregion

        #region 辅助驱动函数

        private void RotationDriver()
        {
            // 应用旋转
            ApplyRotation();
        }

        #region 辅助函数
        // 辅助用平滑转向函数
        private void ApplyRotation()
        {
            // 第一人称不做视角平滑
            _transform.rotation = Quaternion.Euler(0f, _runTimeData.ViewYaw, 0f);
            
            // 俯仰相关 只用于头部的本地坐标系
            _head.transform.localRotation =  Quaternion.Euler(_runTimeData.ViewPitch,0f, 0f);
        }
        #endregion
        

        #endregion
    }
}