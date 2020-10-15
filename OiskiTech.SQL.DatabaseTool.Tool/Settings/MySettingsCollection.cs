using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Oiski.SQL.DatabaseTool
{
    /// <summary>
    /// Represents a collection of <see cref="MySetting"/> objects with <see cref="XMLIO"/> Serialization options
    /// </summary>
    public sealed class MySettingsCollection
    {
        /// <summary>
        /// The name of the setting. This is used as the file name when saving the state of the <see cref="MySettingsCollection"/> as well as when loading a previous state from file.
        /// <br/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The collection of <see cref="MySetting"/>s
        /// </summary>
        public List<MySetting> Settings { get; set; } = new List<MySetting>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_setting"></param>
        /// <returns>The first occurence in the <see cref="MySettingsCollection"/> that matches the specified <paramref name="_setting"/> <see langword="value"/></returns>
        public MySetting this[string _setting]
        {
            get
            {
                MySetting setting = Settings.Where(item => item.Name == _setting).FirstOrDefault();

                return setting;
            }
        }

        /// <summary>
        /// This will initialize and store a new instance of the <see cref="MySetting"/> type in the collection
        /// </summary>
        /// <param name="_setting">This is a unique identifier for the setting. (<i>The value my not be used by another setting wihtin this collection</i>)</param>
        /// <param name="_value">This is the actual value of the setting</param>
        /// <returns> <see langword="false"/> if the value of <paramref name="_setting"/> was found as the identifier for another <see cref="MySetting"/> in the collection.</returns>
        public bool AddSetting(string _setting, string _value)
        {
            if ( this[_setting] == null )
            {
                MySetting setting = new MySetting(_setting, _value);
                Settings.Add(setting);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_setting"></param>
        /// <returns> <see langword="false"/> if the value of <paramref name="_setting"/> already exists in the collection.</returns>
        public bool AddSetting(MySetting _setting)
        {
            if ( this[_setting.Name] == null )
            {
                Settings.Add(_setting);
                return true;
            }

            return false;
        }

        /// <summary>
        /// This will locate and remove the <see cref="MySetting"/> object from this collection where the identifier matches the value of <paramref name="_setting"/>  
        /// </summary>
        /// <param name="_setting"></param>
        /// <returns><see langword="false"/> if the collection holds no instance that matches the specified value of <paramref name="_setting"/></returns>
        public bool RemoveSetting(string _setting)
        {
            MySetting setting = this[_setting];

            if ( setting != null )
            {
                return Settings.Remove(setting);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_setting"></param>
        /// <returns><see langword="false"/> if the collection holds no instance that matches the specified <paramref name="_setting"/></returns>
        public bool RemoveSetting(MySetting _setting)
        {
            if ( this[_setting.Name] != null )
            {
                return RemoveSetting(_setting.Name);
            }

            return false;
        }

        /// <summary>
        /// Saves the current state of the <see cref="MySettingsCollection"/> into a file (.xml) at the specified <paramref name="_path"/> target location
        /// </summary>
        /// <param name="_path">The full path where the <see cref="MySettingsCollection"/> should save its state. (<i>This must be passed without the file name and extenstion</i>)</param>
        public void Save(string _path)
        {
            if ( File.Exists($"{_path}\\{Name}.xml") )
            {
                MySettingsCollection existingSettings = XMLIO.DeserializeXML<MySettingsCollection>($"{_path}\\{Name}");

                foreach ( MySetting setting in Settings )
                {
                    if ( existingSettings[setting.Name] == null )
                    {
                        existingSettings.AddSetting(setting);
                    }
                    else
                    {
                        existingSettings[setting.Name].Value = setting.Value;
                    }
                }

                Settings = existingSettings.Settings;
            }

            XMLIO.SerializeXML(this, $"{_path}\\{Name}");
        }

        /// <summary>
        /// Loads a former state of a <see cref="MySettingsCollection"/> from a file (.xml) at the specified <paramref name="_path"/> target location and overrides this collection
        /// </summary>
        /// <param name="_path">The full path where the <see cref="MySettingsCollection"/> should load the state. (<i>This must be passed without the file name and extension</i>)</param>
        /// <returns><see langword="true"/> if the state of the <see cref="MySettingsCollection"/> could be read from the file (.xml) at the specified <paramref name="_path"/> target location</returns>
        public bool Load(string _path)
        {
            if ( File.Exists($"{_path}\\{Name}.xml") )
            {
                MySettingsCollection loadedSettings = XMLIO.DeserializeXML<MySettingsCollection>($"{_path}\\{Name}");

                Name = loadedSettings.Name;
                Settings = loadedSettings.Settings;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Initializes a new <see langword="instance"/> of type <see cref="MySettingsCollection"/> with its <see langword="default"/> <see langword="values"/>
        /// </summary>
        public MySettingsCollection()
        {

        }

        /// <summary>
        /// This will initialize a new <see cref="MySettingsCollection"/> where the name of the collection is set
        /// </summary>
        /// <param name="_name"></param>
        public MySettingsCollection(string _name)
        {
            Name = _name;
        }
    }
}
