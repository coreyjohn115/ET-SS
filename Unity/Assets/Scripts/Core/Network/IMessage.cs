﻿using System.Collections.Generic;

namespace ET
{
    // 不需要返回消息
    public interface IMessage
    {
    }

    public interface IRequest: IMessage
    {
        int RpcId
        {
            get;
            set;
        }
    }

    public interface IResponse: IMessage
    {
        int Error
        {
            get;
            set;
        }

        List<string> Message
        {
            get;
            set;
        }

        int RpcId
        {
            get;
            set;
        }
    }
}