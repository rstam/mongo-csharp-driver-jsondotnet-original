﻿/* Copyright 2015 MongoDB Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using MongoDB.Bson;

namespace MongoDB.Integrations.JsonDotNet.Converters
{
    public class BsonObjectIdConverter : JsonConverterBase<BsonObjectId>
    {
        #region static
        private static readonly BsonObjectIdConverter __instance = new BsonObjectIdConverter();

        public static BsonObjectIdConverter Instance
        {
            get { return __instance; }
        }
        #endregion

        // public methods
        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType == Newtonsoft.Json.JsonToken.Null)
            {
                return null;
            }
            else
            {
                var objectId = serializer.Deserialize<ObjectId>(reader);
                return new BsonObjectId(objectId);
            }
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                var bsonObjectId = (BsonObjectId)value;
                serializer.Serialize(writer, bsonObjectId.Value);
            }
        }
    }
}
