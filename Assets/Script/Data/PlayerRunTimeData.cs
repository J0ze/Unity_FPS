using UnityEngine;

namespace Script.Data
{
    /// <summary>
    ///  全局唯一的数据黑板 核心中的核心
    /// </summary>
    public class PlayerRunTimeData
    {
        #region 基础输入

        public Vector2 MoveInput;

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
        #endregion
    }
}