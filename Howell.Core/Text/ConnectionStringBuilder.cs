using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using System.Collections.ObjectModel;

namespace Howell.Text
{
    /// <summary>
    /// 网络设备连接字符串
    /// </summary>
    public class ConnectionStringBuilder : IDictionary, ICollection, IEnumerable
    {
        // Fields
        private string _connectionString;
        private Dictionary<string, object> _currentValues;
        internal readonly int _objectID;
        private static int _objectTypeCount;

        // Methods
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConnectionStringBuilder()
            : this("")
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public ConnectionStringBuilder(String connectionString)
        {
            this.ConnectionString = connectionString;
            this._objectID = Interlocked.Increment(ref _objectTypeCount);
        }
        /// <summary>
        /// 将带有指定键和值的项添加到 Howell.Text.ConnectionStringBuilder 中。
        /// </summary>
        /// <param name="keyword">要添加到 Howell.Text.ConnectionStringBuilder 中的键。</param>
        /// <param name="value">指定键的值。</param>
        /// <exception cref="System.ArgumentNullException">keyword 为空引用（在 Visual Basic 中为 Nothing）。</exception>
        /// <exception cref="System.NotSupportedException">Howell.Text.ConnectionStringBuilder 为只读。- 或 -Howell.Text.ConnectionStringBuilder 具有固定大小。</exception>
        public void Add(string keyword, object value)
        {
            this[keyword] = value;
        }
        /// <summary>
        /// 提供了一种有效而安全的方法，用于将项和值追加到某个现有的 System.Text.StringBuilder 对象。
        /// </summary>
        /// <param name="builder">要向其中添加键/值对的 System.Text.StringBuilder。</param>
        /// <param name="keyword">要添加的键。</param>
        /// <param name="value">提供的键的值。</param>
        public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value)
        {
            ConnectionOptions.AppendKeyValuePairBuilder(builder, keyword, value);
        }

        /// <summary>
        /// 清除 Howell.Text.ConnectionStringBuilder 实例的内容。
        /// </summary>
        public virtual void Clear()
        {
            this._connectionString = "";
            this.CurrentValues.Clear();
        }

        /// <summary>
        /// 确定 Howell.Text.ConnectionStringBuilder 是否包含特定键。
        /// </summary>
        /// <param name="keyword">要在 Howell.Text.ConnectionStringBuilder 中定位的键。</param>
        /// <returns>Howell.Text.ConnectionStringBuilder 包含具有指定键的项，则为 true；否则为 false。</returns>
        /// <exception cref="System.ArgumentNullException">keyword 为 null 引用（在 Visual Basic 中为 Nothing）。</exception>
        public virtual bool ContainsKey(string keyword)
        {
            if(keyword == null)
                throw new ArgumentNullException("keyword");
            return this.CurrentValues.ContainsKey(keyword);
        }


        /// <summary>
        /// 将此 Howell.Text.ConnectionStringBuilder 对象中的连接信息与提供的对象中的连接信息进行比较。
        /// </summary>
        /// <param name="connectionStringBuilder">要与此 Howell.Text.ConnectionStringBuilder 对象进行比较的 Howell.Text.ConnectionStringBuilder。</param>
        /// <returns>如果两个 Howell.Text.ConnectionStringBuilder 对象中的连接信息生成等效的连接字符串，则为 true；否则为 false。</returns>
        public virtual bool EquivalentTo(ConnectionStringBuilder connectionStringBuilder)
        {
            if (connectionStringBuilder == null)
                throw new ArgumentNullException("connectionStringBuilder");
            if ((base.GetType() != connectionStringBuilder.GetType()) || (this.CurrentValues.Count != connectionStringBuilder.CurrentValues.Count))
            {
                return false;
            }
            foreach (KeyValuePair<string, object> pair in this.CurrentValues)
            {
                object obj2;
                if (!connectionStringBuilder.CurrentValues.TryGetValue(pair.Key, out obj2) || !pair.Value.Equals(obj2))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// ObjectToString
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private string ObjectToString(object keyword)
        {
            string str;
            try
            {
                str = (string)keyword;
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException("keyword", "not a string");
            }
            return str;
        }
        /// <summary>
        /// 移除 Howell.Text.ConnectionStringBuilder 实例中具有指定键的项。
        /// </summary>
        /// <param name="keyword">要从此 Howell.Text.ConnectionStringBuilder 中的连接字符串移除的键/值对中的键。</param>
        /// <returns>如果该键存在于连接字符串中并被移除，则为 true；如果该键不存在，则为 false。</returns>
        public virtual bool Remove(string keyword)
        {
            if (keyword == null)
                throw new ArgumentNullException("keyword");
            if (this.CurrentValues.Remove(keyword))
            {
                this._connectionString = null;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 指示此 Howell.Text.ConnectionStringBuilder 实例中是否存在指定键。
        /// </summary>
        /// <param name="keyword">要在 Howell.Text.ConnectionStringBuilder 中定位的键。</param>
        /// <returns>如果 Howell.Text.ConnectionStringBuilder 包含具有指定键的项，则为 true；否则为 false。</returns>
        public virtual bool ShouldSerialize(string keyword)
        {
            if (keyword == null)
                throw new ArgumentNullException("keyword");
            return this.CurrentValues.ContainsKey(keyword);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.Collection.CopyTo(array, index);
        }

        void IDictionary.Add(object keyword, object value)
        {
            this.Add(this.ObjectToString(keyword), value);
        }

        bool IDictionary.Contains(object keyword)
        {
            return this.ContainsKey(this.ObjectToString(keyword));
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return this.Dictionary.GetEnumerator();
        }

        void IDictionary.Remove(object keyword)
        {
            this.Remove(this.ObjectToString(keyword));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Collection.GetEnumerator();
        }
        /// <summary>
        /// 返回与此 Howell.Text.ConnectionStringBuilder 关联的连接字符串。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ConnectionString;
        }
        
        /// <summary>
        /// 从此 Howell.Text.ConnectionStringBuilder 中检索与所提供的键对应的值。
        /// </summary>
        /// <param name="keyword">要检索的项的键。</param>
        /// <param name="value">与 key 对应的值。</param>
        /// <returns>如果在连接字符串中找到 keyword，则为 true；否则为 false。</returns>
        /// <exception cref="System.ArgumentNullException">keyword 包含一个空值（在 Visual Basic 中为 Nothing）。</exception>
        public virtual bool TryGetValue(string keyword, out object value)
        {
            if (keyword == null)
                throw new ArgumentNullException("keyword");
            return this.CurrentValues.TryGetValue(keyword, out value);
        }
        private ICollection Collection
        {
            get
            {
                return this.CurrentValues;
            }
        }

        /// <summary>
        /// 获取或设置与 Howell.Text.ConnectionStringBuilder 关联的连接字符串。
        /// </summary>
        /// <exception cref="System.ArgumentException">提供的字符串参数无效</exception>
        [RefreshProperties(RefreshProperties.All), Category("DataCategory_Data"), Description("DbConnectionString_ConnectionString")]
        public string ConnectionString
        {
            get
            {
                string str = this._connectionString;
                if (str == null)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (string str2 in this.Keys)
                    {
                        object obj2;
                        if (this.ShouldSerialize(str2) && this.TryGetValue(str2, out obj2))
                        {
                            string str3 = (obj2 != null) ? Convert.ToString(obj2, System.Globalization.CultureInfo.InvariantCulture) : null;
                            AppendKeyValuePair(builder, str2, str3);
                        }
                    }
                    str = builder.ToString();
                    this._connectionString = str;
                }
                return str;
            }
            set
            {
                ConnectionOptions options = new ConnectionOptions(value, null);
                string connectionString = this.ConnectionString;
                this.Clear();
                try
                {
                    for (NameValuePair pair = options.KeyChain; pair != null; pair = pair.Next)
                    {
                        if (pair.Value != null)
                        {
                            this[pair.Name] = pair.Value;
                        }
                        else
                        {
                            this.Remove(pair.Name);
                        }
                    }
                    this._connectionString = null;
                }
                catch (ArgumentException)
                {
                    this.ConnectionString = connectionString;
                    this._connectionString = connectionString;
                    throw;
                }
            }
        }
        /// <summary>
        /// 获取 Howell.Text.ConnectionStringBuilder.ConnectionString 属性中当前包含的键的数目。
        /// </summary>
        [Browsable(false)]
        public virtual int Count
        {
            get
            {
                return this.CurrentValues.Count;
            }
        }

        private Dictionary<string, object> CurrentValues
        {
            get
            {
                Dictionary<string, object> dictionary = this._currentValues;
                if (dictionary == null)
                {
                    dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    this._currentValues = dictionary;
                }
                return dictionary;
            }
        }

        private IDictionary Dictionary
        {
            get
            {
                return this.CurrentValues;
            }
        }
        /// <summary>
        /// 获取一个值，该值指示 Howell.Text.ConnectionStringBuilder 是否具有固定大小。
        /// </summary>
        [Browsable(false)]
        public virtual bool IsFixedSize
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 获取一个值，该值指示 Howell.Text.ConnectionStringBuilder 是否为只读
        /// </summary>
        [Browsable(false)]
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 获取或设置与指定的键相关联的值。
        /// </summary>
        /// <param name="keyword">要获取或设置的项的键。</param>
        /// <returns>与指定的键相关联的值。如果未找到指定的键，尝试获取它将返回空引用（在 Visual Basic 中为 Nothing），尝试设置它将使用指定的键创建新元素。
        /// 传递空（在 Visual Basic 中为 Nothing）键将引发 System.ArgumentNullException。赋予空值将移除键/值对。</returns>
        /// <exception cref="System.ArgumentNullException">keyword 为空引用（在 Visual Basic 中为 Nothing）。</exception>
        /// <exception cref="System.NotSupportedException">
        /// 设置该属性，而且 System.Data.Common.DbConnectionStringBuilder 为只读。
        /// 设置该属性，集合中不存在 keyword，而且 Howell.Text.ConnectionStringBuilder 具有固定大小。
        /// </exception>
        [Browsable(false)]
        public virtual object this[string keyword]
        {
            get
            {
                object obj2;
                if (keyword == null)
                    throw new ArgumentNullException("keyword");
                if (!this.CurrentValues.TryGetValue(keyword, out obj2))
                {
                    throw new NotSupportedException(String.Format("keyword:{0} is not supported", keyword));
                }
                return obj2;
            }
            set
            {
                if (keyword == null)
                    throw new ArgumentNullException("keyword");
                bool flag = false;
                if (value != null)
                {
                    string str = ConnectionStringBuilderUtil.ConvertToString(value);
                    ConnectionOptions.ValidateKeyValuePair(keyword, str);
                    flag = this.CurrentValues.ContainsKey(keyword);
                    this.CurrentValues[keyword] = str;
                }
                else
                {
                    flag = this.Remove(keyword);
                }
                this._connectionString = null;
            }
        }
        /// <summary>
        /// 获取包含 Howell.Text.ConnectionStringBuilder 中的键的 System.Collections.ICollection。
        /// </summary>
        [Browsable(false)]
        public virtual ICollection Keys
        {
            get
            {
                return this.Dictionary.Keys;
            }
        }

        internal int ObjectID
        {
            get
            {
                return this._objectID;
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return this.Collection.IsSynchronized;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                return this.Collection.SyncRoot;
            }
        }

        object IDictionary.this[object keyword]
        {
            get
            {
                return this[this.ObjectToString(keyword)];
            }
            set
            {
                this[this.ObjectToString(keyword)] = value;
            }
        }
        /// <summary>
        /// 获取包含 Howell.Text.ConnectionStringBuilder 中的值的 System.Collections.ICollection。
        /// </summary>
        [Browsable(false)]
        public virtual ICollection Values
        {
            get
            {
                ICollection<string> keys = (ICollection<string>)this.Keys;
                IEnumerator<string> enumerator = keys.GetEnumerator();
                object[] items = new object[keys.Count];
                for (int i = 0; i < items.Length; i++)
                {
                    enumerator.MoveNext();
                    items[i] = this[enumerator.Current];
                }
                return new ReadOnlyCollection<object>(items);
            }
        }
    }
}