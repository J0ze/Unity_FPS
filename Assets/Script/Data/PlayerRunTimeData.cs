using UnityEngine;

namespace Script.Data
{
    /// <summary>
    ///  全局唯一的数据黑板 核心中的核心
    /// </summary>
    public class PlayerRunTimeData
    {
        #region 基础输入
        /// <summary>
        /// 角色想要移动的世界方向
        /// </summary>
        public Vector3 DesiredWorldMoveDir;

        #endregion

        #region 视角相关
        /// <summary>
        /// 玩家水平视角
        /// </summary>
        public float ViewYaw;
        /// <summary>
        /// 玩家竖直视角
        /// </summary>
        public float ViewPitch;
        /// <summary>
        /// 头部的旋转量 用于做中心点判断
        /// </summary>
        public Quaternion HeadRotation;

        #endregion
    }
}