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
using System.Globalization;
using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace MongoDB.Integrations.JsonDotNet
{
    public class JsonWriterAdapter : Newtonsoft.Json.JsonWriter
    {
        // private fields
        private readonly IBsonWriter _wrappedWriter;

        // constructors
        public JsonWriterAdapter(IBsonWriter wrappedWriter)
        {
            _wrappedWriter = wrappedWriter;
        }

        // public properties
        public IBsonWriter WrappedWriter
        {
            get { return _wrappedWriter; }
        }

        // public methods
        public override void Close()
        {
            base.Close();

            if (CloseOutput)
            {
                _wrappedWriter.Close();
            }
        }

        public override void Flush()
        {
            _wrappedWriter.Flush();
        }

        public void WriteBinaryData(BsonBinaryData value)
        {
            SetWriteState(Newtonsoft.Json.JsonToken.Bytes, null);
            _wrappedWriter.WriteBinaryData(value);
        }

        public void WriteDateTime(long millisecondsSinceEpoch)
        {
            SetWriteState(Newtonsoft.Json.JsonToken.Date, null);
            _wrappedWriter.WriteDateTime(millisecondsSinceEpoch);
        }

        protected override void WriteEnd(Newtonsoft.Json.JsonToken token)
        {
            base.WriteEnd(token);
        }

        public override void WriteEndArray()
        {
            base.WriteEndArray();
            _wrappedWriter.WriteEndArray();
        }

        public override void WriteEndConstructor()
        {
            throw new NotSupportedException();
        }

        public override void WriteEndObject()
        {
            base.WriteEndObject();
            _wrappedWriter.WriteEndDocument();
        }

        protected override void WriteIndent()
        {
            // ignore
        }

        protected override void WriteIndentSpace()
        {
            // ignore
        }

        public void WriteInt32(int value)
        {
            SetWriteState(Newtonsoft.Json.JsonToken.Integer, null);
            _wrappedWriter.WriteInt32(value);
        }

        public void WriteJavaScript(string code)
        {
            SetWriteState(Newtonsoft.Json.JsonToken.Undefined, null);
            _wrappedWriter.WriteJavaScript(code);
        }

        public void WriteJavaScriptWithScope(string code)
        {
            SetWriteState(Newtonsoft.Json.JsonToken.Undefined, null);
            _wrappedWriter.WriteJavaScriptWithScope(code);
        }

        public void WriteMaxKey()
        {
            SetWriteState(Newtonsoft.Json.JsonToken.Undefined, null);
            _wrappedWriter.WriteMaxKey();
        }

        public void WriteMinKey()
        {
            SetWriteState(Newtonsoft.Json.JsonToken.Undefined, null);
            _wrappedWriter.WriteMinKey();
        }

        public override void WriteNull()
        {
            base.WriteNull();
            _wrappedWriter.WriteNull();
        }

        public void WriteObjectId(ObjectId value)
        {
            SetWriteState(Newtonsoft.Json.JsonToken.Undefined, null);
            _wrappedWriter.WriteObjectId(value);
        }

        public override void WritePropertyName(string name)
        {
            base.WritePropertyName(name);
            _wrappedWriter.WriteName(name);
        }

        public override void WriteRaw(string json)
        {
            throw new NotSupportedException();
        }

        public override void WriteRawValue(string json)
        {
            throw new NotSupportedException();
        }

        public void WriteRegularExpression(BsonRegularExpression value)
        {
            SetWriteState(Newtonsoft.Json.JsonToken.Undefined, null);
            _wrappedWriter.WriteRegularExpression(value);
        }

        public override void WriteStartArray()
        {
            base.WriteStartArray();
            _wrappedWriter.WriteStartArray();
        }

        public override void WriteStartConstructor(string name)
        {
            throw new NotSupportedException();
        }

        public override void WriteStartObject()
        {
            base.WriteStartObject();
            _wrappedWriter.WriteStartDocument();
        }

        public void WriteSymbol(string value)
        {
            SetWriteState(Newtonsoft.Json.JsonToken.Undefined, null);
            _wrappedWriter.WriteSymbol(value);
        }

        public void WriteTimestamp(long value)
        {
            SetWriteState(Newtonsoft.Json.JsonToken.Undefined, null);
            _wrappedWriter.WriteTimestamp(value);
        }

        public override void WriteUndefined()
        {
            base.WriteUndefined();
            _wrappedWriter.WriteUndefined();
        }

        public override void WriteValue(bool value)
        {
            base.WriteValue(value);
            _wrappedWriter.WriteBoolean(value);
        }

        public override void WriteValue(byte value)
        {
            base.WriteValue(value);
            _wrappedWriter.WriteInt32((int)value);
        }

        public override void WriteValue(byte[] value)
        {
            base.WriteValue(value);
            if (value != null)
            {
                _wrappedWriter.WriteBinaryData(new BsonBinaryData(value));
            }
        }

        public override void WriteValue(char value)
        {
            base.WriteValue(value);
            _wrappedWriter.WriteString(value.ToString(CultureInfo.InvariantCulture));
        }

        public override void WriteValue(DateTime value)
        {
            base.WriteValue(value);
            _wrappedWriter.WriteDateTime(BsonUtils.ToMillisecondsSinceEpoch(value));
        }

        public override void WriteValue(DateTimeOffset value)
        {
            base.WriteValue(value);
            _wrappedWriter.WriteDateTime(BsonUtils.ToMillisecondsSinceEpoch(value.UtcDateTime));
        }

        public override void WriteValue(decimal value)
        {
            base.WriteValue(value);
            _wrappedWriter.WriteDouble(Convert.ToDouble(value, NumberFormatInfo.InvariantInfo));
        }

        public override void WriteValue(double value)
        {
            base.WriteValue(value);
            _wrappedWriter.WriteDouble(value);
        }

        public override void WriteValue(float value)
        {
            base.WriteValue(value);
            _wrappedWriter.WriteDouble((double)value);
        }

        public override void WriteValue(Guid value)
        {
            base.WriteValue(value);
            var guidRepresentation = _wrappedWriter.Settings.GuidRepresentation;
            var bytes = GuidConverter.ToBytes(value, guidRepresentation);
            var subType = guidRepresentation == GuidRepresentation.Standard ? BsonBinarySubType.UuidStandard : BsonBinarySubType.UuidLegacy;
            var binaryData = new BsonBinaryData(bytes, subType, guidRepresentation);
            _wrappedWriter.WriteBinaryData(binaryData);
        }

        public override void WriteValue(int value)
        {
            base.WriteValue(value);
            _wrappedWriter.WriteInt32(value);
        }

        public override void WriteValue(long value)
        {
            base.WriteValue(value);
            _wrappedWriter.WriteInt64(value);
        }

        public override void WriteValue(sbyte value)
        {
            base.WriteValue(value);
            _wrappedWriter.WriteInt32((int)value);
        }

        public override void WriteValue(short value)
        {
            base.WriteValue(value);
            _wrappedWriter.WriteInt32((int)value);
        }

        public override void WriteValue(string value)
        {
            base.WriteValue(value);
            if (value == null)
            {
                // unlike other WriteValue methods the base method does not call WriteNull
                _wrappedWriter.WriteNull();
            }
            else
            {
                _wrappedWriter.WriteString(value);
            }
        }

        public override void WriteValue(TimeSpan value)
        {
            base.WriteValue(value);
            _wrappedWriter.WriteString(value.ToString());
        }

        public override void WriteValue(uint value)
        {
            base.WriteValue(value);
            _wrappedWriter.WriteInt32((int)value);
        }

        public override void WriteValue(ulong value)
        {
            base.WriteValue(value);
            _wrappedWriter.WriteInt64((long)value);
        }

        public override void WriteValue(Uri value)
        {
            base.WriteValue(value);
            if (value != null)
            {
                _wrappedWriter.WriteString(value.ToString());
            }
        }

        public override void WriteValue(ushort value)
        {
            base.WriteValue(value);
            _wrappedWriter.WriteInt32((int)value);
        }

        public override void WriteWhitespace(string ws)
        {
            // ignore
        }
    }
}
