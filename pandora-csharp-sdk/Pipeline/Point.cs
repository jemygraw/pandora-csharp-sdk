using Qiniu.Pandora.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qiniu.Pandora.Pipeline
{
    public class Point
    {
        //max point size
        public const int MaxPointSize = 1 * 1024 * 1024;
        private List<Field> fields = new List<Field>();
        private int size;
        public void Append(string key, DateTime dateTime)
        {
            Field f = new Field(key, Util.CreateRFC3339DateTime(dateTime));
            this.Append(f);
        }

        public void Append(string key, Int16 value)
        {
            Field f = new Field(key, value);
            this.Append(f);
        }

        public void Append(string key, Int32 value)
        {
            Field f = new Field(key, value);
            this.Append(f);
        }

        public void Append(string key, Int64 value)
        {
            Field f = new Field(key, value);
            this.Append(f);
        }

        public void Append(string key, UInt16 value)
        {
            Field f = new Field(key, value);
            this.Append(f);
        }

        public void Append(string key, UInt32 value)
        {
            Field f = new Field(key, value);
            this.Append(f);
        }

        public void Append(string key, UInt64 value)
        {
            Field f = new Field(key, value);
            this.Append(f);
        }

        public void Append(string key, string value)
        {

            Field f = new Field(key, value);
            this.Append(f);
        }

        public void Append(string key, float value)
        {
            Field f = new Field(key, value);
            this.Append(f);
        }

        public void Append(string key, double value)
        {
            Field f = new Field(key, value);
            this.Append(f);
        }

        public void Append(string key, bool value)
        {
            Field f = new Field(key, value);
            this.Append(f);
        }

        public void Append<T>(string key, List<T> value)
        {
            Field f = new Field(key, Util.JsonEncode(value));
            this.Append(f);
        }

        public void Append<TKey, TValue>(string key, Dictionary<TKey, TValue> value)
        {
            Field f = new Field(key, Util.JsonEncode(value));
            this.Append(f);
        }

        private void Append(Field f)
        {
            this.fields.Add(f);
            this.size += f.GetSize() + 1;//including the trailing character \t or \n
        }

        public int GetSize()
        {
            return this.size;
        }

        public bool IsTooLarge()
        {
            return this.size > MaxPointSize;
        }

        public override string ToString()
        {
            List<string> fieldsStr = new List<string>();
            foreach(Field f in this.fields)
            {
                fieldsStr.Add(f.ToString());
            }
            return string.Join("\t", fieldsStr) + "\n";
        }
    }


    public class Field
    {
        private string key;
        private object value;
        private int size;
        public Field(string key, object value)
        {
            this.key = key;
            this.value = value;
            this.size = Encoding.UTF8.GetBytes(ToString()).Length;
        }

        public int GetSize()
        {
            return this.size;
        }

        public override string ToString()
        {
            return string.Format("{0}={1}", this.key, this.value.ToString());
        }
    }
}
