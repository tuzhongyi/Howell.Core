using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Howell.Net.NetworkDevice.Common
{
    /// <summary>
    /// 
    /// </summary>
    internal class NetworkDeviceConnectionStringBuilderDescriptor : PropertyDescriptor
    {
        // Fields
        private Type _componentType;
        private bool _isReadOnly;
        private Type _propertyType;
        private bool _refreshOnChange;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="componentType"></param>
        /// <param name="propertyType"></param>
        /// <param name="isReadOnly"></param>
        /// <param name="attributes"></param>
        internal NetworkDeviceConnectionStringBuilderDescriptor(string propertyName, Type componentType, Type propertyType, bool isReadOnly, Attribute[] attributes)
            : base(propertyName, attributes)
        {
            this._componentType = componentType;
            this._propertyType = propertyType;
            this._isReadOnly = isReadOnly;
        }
        /// <summary>
        /// 当在派生类中被重写时，返回重置对象时是否更改其值。
        /// </summary>
        /// <param name="component">要测试重置功能的组件。</param>
        /// <returns>如果重置组件更改其值，则为 true；否则为 false。</returns>
        public override bool CanResetValue(object component)
        {
            NetworkDeviceConnectionStringBuilder1 builder = component as NetworkDeviceConnectionStringBuilder1;
            return ((builder != null) && builder.ShouldSerialize(this.DisplayName));
        }
        /// <summary>
        /// 当在派生类中被重写时，获取组件上的属性的当前值。
        /// </summary>
        /// <param name="component"> 具有为其检索值的属性的组件。</param>
        /// <returns> 给定组件的属性的值。</returns>
        public override object GetValue(object component)
        {
            object obj2;
            NetworkDeviceConnectionStringBuilder1 builder = component as NetworkDeviceConnectionStringBuilder1;
            if ((builder != null) && builder.TryGetValue(this.DisplayName, out obj2))
            {
                return obj2;
            }
            return null;
        }
        /// <summary>
        /// 当在派生类中被重写时，将组件的此属性的值重置为默认值。
        /// </summary>
        /// <param name="component">具有要重置为默认值的属性值的组件。</param>
        public override void ResetValue(object component)
        {
            NetworkDeviceConnectionStringBuilder1 builder = component as NetworkDeviceConnectionStringBuilder1;
            if (builder != null)
            {
                builder.Remove(this.DisplayName);
                if (this.RefreshOnChange)
                {
                    builder.ClearPropertyDescriptors();
                }
            }
        }
        /// <summary>
        /// 当在派生类中被重写时，将组件的值设置为一个不同的值。
        /// </summary>
        /// <param name="component">具有要进行设置的属性值的组件。</param>
        /// <param name="value">新值。</param>
        public override void SetValue(object component, object value)
        {
            NetworkDeviceConnectionStringBuilder1 builder = component as NetworkDeviceConnectionStringBuilder1;
            if (builder != null)
            {
                if ((typeof(string) == this.PropertyType) && string.Empty.Equals(value))
                {
                    value = null;
                }
                builder[this.DisplayName] = value;
                if (this.RefreshOnChange)
                {
                    builder.ClearPropertyDescriptors();
                }
            }
        }
        /// <summary>
        /// 当在派生类中被重写时，确定一个值，该值指示是否需要永久保存此属性的值。
        /// </summary>
        /// <param name="component">具有要检查其持久性的属性的组件。</param>
        /// <returns> 如果属性应该被永久保存，则为 true；否则为 false。</returns>
        public override bool ShouldSerializeValue(object component)
        {
            NetworkDeviceConnectionStringBuilder1 builder = component as NetworkDeviceConnectionStringBuilder1;
            return ((builder != null) && builder.ShouldSerialize(this.DisplayName));
        }

        /// <summary>
        /// 当在派生类中被重写时，获取该属性绑定到的组件的类型。
        /// </summary>
        public override Type ComponentType
        {
            get
            {
                return this._componentType;
            }
        }
        /// <summary>
        /// 当在派生类中被重写时，获取指示该属性是否为只读的值。
        /// </summary>
        public override bool IsReadOnly
        {
            get
            {
                return this._isReadOnly;
            }
        }
        /// <summary>
        /// 当在派生类中被重写时，获取该属性的类型。
        /// </summary>
        public override Type PropertyType
        {
            get
            {
                return this._propertyType;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal bool RefreshOnChange
        {
            get
            {
                return this._refreshOnChange;
            }
            set
            {
                this._refreshOnChange = value;
            }
        }
    }
}
