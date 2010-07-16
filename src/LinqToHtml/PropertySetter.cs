using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LinqToHtml
{
	public class PropertySetter
	{
		public static readonly PropertySetter Boolean = new PropertySetter(typeof(bool), (d, p, v) =>
			{
				try
				{
					bool value = Convert.ToBoolean(v);
					p.SetValue(d, value, null);
				}
				catch
				{
				}
			});
		public static readonly PropertySetter DateTime = new PropertySetter(typeof(DateTime), (d, p, v) =>
			{
				try
				{
					var value = Convert.ToDateTime(v);
					p.SetValue(d, value, null);
				}
				catch
				{
				}
			});
		public static readonly PropertySetter Decimal = new PropertySetter(typeof(decimal), (d, p, v) =>
			{
				try
				{
					decimal value = Convert.ToDecimal(v);
					p.SetValue(d, value, null);
				}
				catch
				{
				}
			});
		public static readonly PropertySetter Default = new PropertySetter(typeof(object), (d, p, v) => { });
		public static readonly PropertySetter Double = new PropertySetter(typeof(double), (d, p, v) =>
			{
				try
				{
					double value = Convert.ToDouble(v);
					p.SetValue(d, value, null);
				}
				catch
				{
				}
			});
		public static readonly PropertySetter Int = new PropertySetter(typeof(int), (d, p, v) =>
			{
				try
				{
					int value = Convert.ToInt32(v);
					p.SetValue(d, value, null);
				}
				catch
				{
				}
			});
		private static readonly IList<PropertySetter> PropertySetters = new List<PropertySetter>();
		public static readonly PropertySetter String = new PropertySetter(typeof(string), (d, p, v) =>
			{
				try
				{
					p.SetValue(d, v, null);
				}
				catch
				{
				}
			});

		private readonly Type _type;

		static PropertySetter()
		{
			PropertySetters.Add(String);
			PropertySetters.Add(Decimal);
			PropertySetters.Add(Double);
			PropertySetters.Add(Int);
			PropertySetters.Add(Boolean);
			PropertySetters.Add(DateTime);
		}

		private PropertySetter(Type type, Action<object, PropertyInfo, string> setValue)
		{
			_type = type;
			SetValue = setValue;
		}

		public Action<object, PropertyInfo, string> SetValue { get; private set; }

		public static PropertySetter GetFor(Type type)
		{
			return PropertySetters.FirstOrDefault(x => x.IsMatch(type)) ?? Default;
		}

		public bool IsMatch(Type type)
		{
			return type.IsAssignableFrom(_type);
		}
	}
}