using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace OpenCharas
{
	public class SettingProperty
	{
		string _name;
		PropertyInfo _info;
		MethodInfo _getter, _setter;
		string _defaultValue;
		Type _type;
		GenericConverter.ConvertToHandler _convertTo;
		GenericConverter.ConvertFromStringHandler _convertFrom;

		public string DefaultValue
		{
			get { return _defaultValue; }
			set { _defaultValue = value; }
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public PropertyInfo PropInfo
		{
			get { return _info; }
			set { _info = value; _getter = _info.GetGetMethod(); _type = _getter.ReturnType; _setter = _info.GetSetMethod(); }
		}

		public MethodInfo Getter
		{
			get { return _getter; }
		}

		public Type Type
		{
			get { return _type; }
		}

		public MethodInfo Setter
		{
			get { return _setter; }
		}

		public GenericConverter.ConvertToHandler ConvertTo
		{
			get { return _convertTo; }
		}

		public GenericConverter.ConvertFromStringHandler ConvertFrom
		{
			get { return _convertFrom; }
		}

		public string InvokeConvertTo()
		{
			return ConvertTo(this);
		}

		public void InvokeConvertFrom(string str)
		{
			ConvertFrom(this, str);
		}

		public SettingProperty(string name,
			GenericConverter.ConvertToHandler convertToHandler = null,
			GenericConverter.ConvertFromStringHandler convertFromHandler = null)
		{
			if (convertToHandler == null)
				convertToHandler = GenericConverter.GenericConverterToString;
			if (convertFromHandler == null)
				convertFromHandler = GenericConverter.GenericConverterFromString;

			_name = name;
			_convertTo = convertToHandler;
			_convertFrom = convertFromHandler;
		}
	}

	public static class GenericConverter
	{
		public delegate string ConvertToHandler(SettingProperty property);
		public delegate void ConvertFromStringHandler(SettingProperty property, string value);

		public static string GenericConverterToString (SettingProperty property)
		{
			return property.Getter.Invoke(null, null).ToString();
		}

		public static void GenericConverterFromString(SettingProperty property, string value)
		{
			property.Setter.Invoke(null, new object[] {Convert.ChangeType(value, property.Type)});
		}
	}

	public class SettingsContainer
	{
		Type _type;
		List<SettingProperty> _props = new List<SettingProperty>();

		public SettingsContainer(System.Type type)
		{
			_type = type;
		}

		public Type Type
		{
			get { return _type; }
			set { _type = value; }
		}

		public void Add(params SettingProperty[] prop)
		{
			foreach (var p in prop)
			{
				p.PropInfo = _type.GetProperty(p.Name);
				p.DefaultValue = p.InvokeConvertTo();
				_props.Add(p);
			}
		}

		public SettingProperty Find(string Name)
		{
			foreach (var p in _props)
			{
				if (p.Name == Name)
					return p;
			}

			return null;
		}

		public void Reset()
		{
			foreach (var p in _props)
				p.InvokeConvertFrom(p.DefaultValue);
		}

		public void Save(string FileName)
		{
			using (FileStream fs = new FileStream(FileName, FileMode.Create))
			{
				using (StreamWriter writer = new StreamWriter(fs))
				{
					foreach (var p in _props)
						writer.WriteLine(p.Name + " = <" + p.InvokeConvertTo() + '>');
				}
			}
		}

		public void Load(string FileName)
		{
			if (!File.Exists(FileName))
				return;

			using (FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read))
			{
				using (StreamReader reader = new StreamReader(fs))
				{
					while (!reader.EndOfStream)
					{
						string[] line = reader.ReadLine().Split(new char[]{' ', '<', '>'}, StringSplitOptions.RemoveEmptyEntries);

						string name = line[0];
						string value = (line.Length <= 2) ? "" : line[2];

						Find(name).InvokeConvertFrom(value);
					}
				}
			}
		}
	}
}
