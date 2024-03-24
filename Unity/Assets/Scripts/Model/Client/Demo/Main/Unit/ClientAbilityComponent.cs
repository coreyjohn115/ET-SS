namespace ET.Client
{
    [ComponentOf(typeof (Unit))]
    public class ClientAbilityComponent: Entity, IAwake
    {
        /// <summary>
        /// 本地能力值
        /// </summary>
        public int value;

        /// <summary>
        /// 本地能力列表
        /// </summary>
        public int[] abilityList = new int[(int)RoleAbility.End];

        /// <summary>
        /// 服务器能力值
        /// </summary>
        public int serverValue;
    }
}