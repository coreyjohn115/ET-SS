namespace ET.Client
{
    public struct ClientUseSkill
    {
        public Unit Unit;
        public int MasterId;
    }

    [ComponentOf(typeof (Unit))]
    public class ClientSkillComponent: Entity, IAwake
    {
    }
}