using System;

namespace Oiski.SQL.DatabaseTool
{
    /// <summary>
    /// Represents a setting with a name and a value
    /// </summary>
    public class MySetting
    {
        /// <summary>
        /// This is treated as a unique key by the <see cref="MySettingsCollection"/>
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// This is the actual setting
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// This will check the <see cref="Name"/> for a <see langword="null"/> or empty value
        /// <br/>
        /// <strong>Do not use this to check for <see langword="null"/> on an <see cref="MySetting"/> instance</strong>
        /// </summary>
        /// <returns><see langword="true"/> if the <see cref="Name"/> was <see langword="null"/> or empty</returns>
        public bool IsEmpty()
        {
            if ( string.IsNullOrEmpty(Name) )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Initialize a new <see langword="instance"/> of type <see cref="MySetting"/> with its <see langword="default"/> <see langword="values"/>
        /// <br/>
        /// <strong>Note:</strong> A <see cref="MySetting"/> with <see langword="default"/> <see langword="value"/> is consideret empty
        /// </summary>
        public MySetting()
        {
            Name = null;
        }

        /// <summary>
        /// This will initialize a new instance of the <see cref="MySetting"/> where the <see cref="Name"/> and the <see cref="Value"/> of the setting is set
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public MySetting(string _name, string _value)
        {
            if ( !string.IsNullOrEmpty(_name) )
            {
                Name = _name;
                Value = _value;
            }
            else
            {
                throw new ArgumentNullException("_name", "Null is not a valid value");
            }
        }
    }
}
