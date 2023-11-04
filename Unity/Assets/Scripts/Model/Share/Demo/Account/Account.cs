﻿namespace ET
{
    public enum AccountType
    {
        General = 0,

        /// <summary>
        /// 白名单
        /// </summary>
        White = 1,
    }

    [ChildOf]
    public class Account: Entity, IAwake
    {
        /// <summary>
        /// 用户uid
        /// </summary>
        public string UserUid;

        public string UidBack;

        public string AccountName; //账户名

        public string Password; //账户密码

        public long CreateTime; //账号创建时间

        public AccountType AccountType; //账号类型
    }
}