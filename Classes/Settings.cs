using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Drawing;

namespace Paril
{
	public interface ISettingsWriteRead
	{
		void Save(SettingsContainer settings, Stream stream);
		void Load(SettingsContainer settings, Stream stream);
	}

	public static class Writers
	{
		public static readonly BasicTextWriteRead DefaultTextWriteReader = new BasicTextWriteRead();

		public class BasicTextWriteRead : ISettingsWriteRead
		{
			public void Save(SettingsContainer settings, Stream stream)
			{
				using (System.IO.StreamWriter writer = new StreamWriter(stream))
				{
					foreach (var p in settings.Properties.Values)
						writer.WriteLine(p.Name + " = <" + p.InvokeConvertTo() + '>');
				}
			}

			public void Load(SettingsContainer settings, Stream stream)
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					while (!reader.EndOfStream)
					{
						string[] line = reader.ReadLine().Split(new char[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries);

						string name = line[0].Remove(line[0].IndexOf('=')).Trim();
						string value = (line.Length <= 1) ? "" : line[1];

						if (settings.Properties.ContainsKey(name))
							settings.Properties[name].InvokeConvertFrom(value);
					}
				}
			}
		}

		public class BasicBinaryWriteRead : ISettingsWriteRead
		{
			public void Save(SettingsContainer settings, Stream stream)
			{
				using (BinaryWriter writer = new BinaryWriter(stream))
				{
					writer.Write(settings.Properties.Values.Count);

					foreach (var p in settings.Properties.Values)
					{
						writer.Write(p.Name);
						writer.Write(p.InvokeConvertTo());
					}
				}
			}

			public void Load(SettingsContainer settings, Stream stream)
			{
				using (BinaryReader reader = new BinaryReader(stream))
				{
					int c = reader.ReadInt32();

					for (int i = 0; i < c; ++i)
					{
						string name = reader.ReadString();
						string value = reader.ReadString();

						if (settings.Properties.ContainsKey(name))
							settings.Properties[name].InvokeConvertFrom(value);
					}
				}
			}
		}
	}

	public abstract class SettingsValue
	{
		string _name, _defaultValue;
		Type _type;
		SettingsTypeConverter _converter;

		public SettingsValue(string name)
		{
			_name = name;
		}

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

		public Type Type
		{
			get { return _type; }
			protected set { _type = value; }
		}

		public SettingsTypeConverter Converter
		{
			get { return _converter; }
			set { _converter = value; }
		}

		public abstract string InvokeConvertTo();
		public abstract void InvokeConvertFrom(string str);
	}	

	public class SettingsProperty : SettingsValue
	{
		PropertyInfo _info;

		public SettingsProperty(string name) :
			base(name)
		{
		}

		public PropertyInfo PropInfo
		{
			get { return _info; }
			set { _info = value; Type = _info.PropertyType; }
		}

		public override string InvokeConvertTo()
		{
			return Converter.Converter.ConvertTo(this, _info.GetValue(null, null));
		}

		public override void InvokeConvertFrom(string str)
		{
			_info.SetValue(null, Converter.Converter.ConvertFrom(this, str), null);
		}
	}

	public class SettingsField : SettingsValue
	{
		FieldInfo _info;

		public SettingsField(string name) :
			base(name)
		{
		}

		public FieldInfo FieldInfo
		{
			get { return _info; }
			set { _info = value; Type = _info.FieldType; }
		}

		public override string InvokeConvertTo()
		{
			return Converter.Converter.ConvertTo(this, _info.GetValue(null));
		}

		public override void InvokeConvertFrom(string str)
		{
			_info.SetValue(null, Converter.Converter.ConvertFrom(this, str));
		}
	}

	public interface ISettingConverter
	{
		string ConvertTo(SettingsValue property, object currentValue);
		object ConvertFrom(SettingsValue property, string value);
	}

	public static class DefaultConverters
	{
		public static readonly SettingsTypeConverter DefaultConverter = new SettingsTypeConverter(null, new GenericConverter());

		public class GenericConverter : ISettingConverter
		{
			public string ConvertTo(SettingsValue property, object currentValue)
			{
				return currentValue.ToString();
			}

			public object ConvertFrom(SettingsValue property, string value)
			{
				return Convert.ChangeType(value, property.Type);
			}
		}

		public class PointConverter : ISettingConverter
		{
			public string ConvertTo(SettingsValue property, object value)
			{
				Point p = (Point)value;
				return p.X.ToString() + ' ' + p.Y.ToString();
			}

			public object ConvertFrom(SettingsValue property, string value)
			{
				return new Point(
					int.Parse(value.Substring(0, value.IndexOf(' '))),
					int.Parse(value.Substring(value.IndexOf(' ') + 1))
					);
			}
		}

		public class PointFConverter : ISettingConverter
		{
			public string ConvertTo(SettingsValue property, object value)
			{
				PointF p = (PointF)value;
				return p.X.ToString() + ' ' + p.Y.ToString();
			}

			public object ConvertFrom(SettingsValue property, string value)
			{
				return new PointF(
					float.Parse(value.Substring(0, value.IndexOf(' '))),
					float.Parse(value.Substring(value.IndexOf(' ') + 1))
					);
			}
		}

		public class SizeConverter : ISettingConverter
		{
			public string ConvertTo(SettingsValue property, object value)
			{
				Size p = (Size)value;
				return p.Width.ToString() + ' ' + p.Height.ToString();
			}

			public object ConvertFrom(SettingsValue property, string value)
			{
				return new Size(
					int.Parse(value.Substring(0, value.IndexOf(' '))),
					int.Parse(value.Substring(value.IndexOf(' ') + 1))
					);
			}
		}

		public class SizeFConverter : ISettingConverter
		{
			public string ConvertTo(SettingsValue property, object value)
			{
				SizeF p = (SizeF)value;
				return p.Width.ToString() + ' ' + p.Height.ToString();
			}

			public object ConvertFrom(SettingsValue property, string value)
			{
				return new SizeF(
					float.Parse(value.Substring(0, value.IndexOf(' '))),
					float.Parse(value.Substring(value.IndexOf(' ') + 1))
					);
			}
		}

		public class ColorConverter : ISettingConverter
		{
			public string ConvertTo(SettingsValue property, object value)
			{
				Color p = (Color)value;
				return p.A.ToString() + ' ' + p.R.ToString() + ' ' + p.G.ToString() + ' ' + p.B.ToString();
			}

			public object ConvertFrom(SettingsValue property, string value)
			{
				var p = value.Split();
				return Color.FromArgb(
					byte.Parse(p[0]),
					byte.Parse(p[1]),
					byte.Parse(p[2]),
					byte.Parse(p[3])
					);
			}
		}

		public class FontConverter : ISettingConverter
		{
			public string ConvertTo(SettingsValue property, object value)
			{
				Font p = (Font)value;

				string val = p.FontFamily.Name + ", " + p.Size.ToString();

				if (p.Style != FontStyle.Regular)
					val += ", " + Enum.Format(typeof(FontStyle), p.Style, "g").Replace(",", " |");

				return val;
			}

			public object ConvertFrom(SettingsValue property, string value)
			{
				var p = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

				for (int i = 0; i < p.Length; ++i)
					p[i] = p[i].Trim();

				string fontName = p[0];
				float fontSize = float.Parse(p[1]);
				FontStyle fontFlags = 0;

				if (p.Length >= 3)
				{
					var flags = p[2].Split('|');

					for (int i = 0; i < flags.Length; ++i)
						flags[i] = flags[i].Trim();

					foreach (var f in flags)
						fontFlags |= (FontStyle)Enum.Parse(typeof(FontStyle), f);
				}

				return new Font(fontName, fontSize, fontFlags);
			}
		}

		public class GuidConverter : ISettingConverter
		{
			public string ConvertTo(SettingsValue property, object value)
			{
				Guid p = (Guid)value;
				return p.ToString();
			}

			public object ConvertFrom(SettingsValue property, string value)
			{
				return new Guid(value);
			}
		}

		public class TimeSpanConverter : ISettingConverter
		{
			public string ConvertTo(SettingsValue property, object value)
			{
				TimeSpan p = (TimeSpan)value;
				return p.ToString();
			}

			public object ConvertFrom(SettingsValue property, string value)
			{
				return TimeSpan.Parse(value);
			}
		}

		public class EnumConverter<TEnum> : ISettingConverter
		{
			public virtual string ConvertTo (SettingsValue property, object value)
			{
				return Enum.Format(typeof(TEnum), value, "g");
			}

			public object ConvertFrom (SettingsValue property, string value)
			{
				return Enum.Parse(typeof(TEnum), value);
			}
		}

		public class EnumFlagsConverter<TEnum> : EnumConverter<TEnum>, ISettingConverter
		{
			public override string ConvertTo (SettingsValue property, object value)
			{
				return Enum.Format(typeof(TEnum), value, "f");
			}
		}

		public static SettingsTypeConverter[] _defaultConverters = new SettingsTypeConverter[]
		{
			new SettingsTypeConverter(typeof(Point), new PointConverter()),
			new SettingsTypeConverter(typeof(PointF), new PointFConverter()),
			new SettingsTypeConverter(typeof(Size), new SizeConverter()),
			new SettingsTypeConverter(typeof(SizeF), new SizeFConverter()),
			new SettingsTypeConverter(typeof(Color), new ColorConverter()),
			new SettingsTypeConverter(typeof(Font), new FontConverter()),
			new SettingsTypeConverter(typeof(Guid), new GuidConverter()),
			new SettingsTypeConverter(typeof(TimeSpan), new TimeSpanConverter()),
		};

		public static SettingsTypeConverter[] GetDefaultConverters()
		{
			return _defaultConverters;
		}
	}

	public class SettingsTypeConverter
	{
		Type _type;
		ISettingConverter _converter;

		public Type Type
		{
			get { return _type; }
		}

		public ISettingConverter Converter
		{
			get { return _converter; }
		}

		public SettingsTypeConverter(Type type, ISettingConverter converter)
		{
			_type = type;
			_converter = converter;
		}
	}

	public enum SettingsBinds
	{
		Instance = 1,
		Properties = 2
	}

	public class SettingsContainer
	{
		Dictionary<string, SettingsValue> _props = new Dictionary<string, SettingsValue>();
		Dictionary<Type, SettingsTypeConverter> _converters = new Dictionary<Type, SettingsTypeConverter>();
		SettingsBinds _binds = SettingsBinds.Properties;
		ISettingsWriteRead _saver = Writers.DefaultTextWriteReader;

		public SettingsContainer(bool defaultConverters = true)
		{
			if (defaultConverters)
				AddConverters(DefaultConverters.GetDefaultConverters());
		}

		public Dictionary<string, SettingsValue> Properties
		{
			get { return _props; }
		}

		public Dictionary<Type, SettingsTypeConverter> Converters
		{
			get { return _converters; }
		}

		public SettingsBinds Binds
		{
			get { return _binds; }
			set { _binds = value; }
		}

		public ISettingsWriteRead Saver
		{
			get { return _saver; }
			set { _saver = value; }
		}

		public void AddType(Type t)
		{
			if ((_binds & SettingsBinds.Properties) != 0)
			{
				foreach (var prop in t.GetProperties(BindingFlags.Public | BindingFlags.Static))
				{
					var p = new SettingsProperty(prop.Name);
					p.PropInfo = prop;
					AddProperty(p, t);
				}
			}

			if ((_binds & SettingsBinds.Instance) != 0)
			{
				foreach (var mem in t.GetFields(BindingFlags.Public | BindingFlags.Static))
				{
					var p = new SettingsField(mem.Name);
					p.FieldInfo = mem;
					AddField(p, t);
				}
			}
		}

		public void AddTypes(IEnumerable<Type> types)
		{
			foreach (var t in types)
				AddType(t);
		}

		public void AddConverter(SettingsTypeConverter convert)
		{
			_converters.Add(convert.Type, convert);
		}

		public void AddConverters(IEnumerable<SettingsTypeConverter> convert)
		{
			foreach (var p in convert)
				_converters.Add(p.Type, p);
		}

		public void AddProperty(SettingsProperty prop, Type type)
		{
			if (prop.PropInfo == null)
				prop.PropInfo = type.GetProperty(prop.Name);

			if (_converters.ContainsKey(prop.Type))
				prop.Converter = _converters[prop.Type];
			else
				prop.Converter = DefaultConverters.DefaultConverter;

			prop.DefaultValue = prop.InvokeConvertTo();

			_props.Add(prop.Name, prop);
		}

		public void AddProperties(IEnumerable<SettingsProperty> prop, Type type)
		{
			foreach (var p in prop)
				AddProperty(p, type);
		}

		public void AddField(SettingsField prop, Type type)
		{
			if (prop.FieldInfo == null)
				prop.FieldInfo = type.GetField(prop.Name);

			if (_converters.ContainsKey(prop.Type))
				prop.Converter = _converters[prop.Type];
			else
				prop.Converter = DefaultConverters.DefaultConverter;

			prop.DefaultValue = prop.InvokeConvertTo();

			_props.Add(prop.Name, prop);
		}

		public void AddFields(IEnumerable<SettingsField> prop, Type type)
		{
			foreach (var p in prop)
				AddField(p, type);
		}

		public void Reset()
		{
			foreach (var p in _props.Values)
				p.InvokeConvertFrom(p.DefaultValue);
		}

		public void Save(string FileName)
		{
			System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(FileName));

			using (FileStream fs = new FileStream(FileName, FileMode.Create))
				_saver.Save(this, fs);
		}

		public void Load(string FileName)
		{
			if (!File.Exists(FileName))
				return;

			using (FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read))
				_saver.Load(this, fs);
		}
	}
}
