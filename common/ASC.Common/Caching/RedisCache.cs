﻿/*
 *
 * (c) Copyright Ascensio System Limited 2010-2021
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
*/


using System;
using System.Collections.Concurrent;
using Google.Protobuf;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace ASC.Common.Caching;

[Singletone]
public class RedisCache<T> : ICacheNotify<T> where T : IMessage<T>, new()
{
    private readonly string CacheId = Guid.NewGuid().ToString();
    private readonly IRedisDatabase _redis;
    private readonly ConcurrentDictionary<Type, ConcurrentBag<Action<object, CacheNotifyAction>>> actions = new ConcurrentDictionary<Type, ConcurrentBag<Action<object, CacheNotifyAction>>>();

    public RedisCache(IRedisCacheClient redisCacheClient)
    {
        _redis = redisCacheClient.GetDbFromConfiguration();
    }

    public void Publish(T obj, CacheNotifyAction action)
    {
        throw new NotImplementedException();
    }

    public void Subscribe(Action<T> onchange, CacheNotifyAction action)
    {
        throw new NotImplementedException();
    }

    public void Unsubscribe(CacheNotifyAction action)
    {
        throw new NotImplementedException();
    }

}
