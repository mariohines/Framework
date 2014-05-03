using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Dynamic;
using System.Linq;

namespace Framework.Core.Collections
{
	/// <summary>A dynamic class for setting up dynamic configuration collections.</summary>
	public class DynamicCollection : DynamicObject
	{
		private readonly Dictionary<string, object> _collection;

		/// <summary>Default constructor.</summary>
		public DynamicCollection ()
			: this(new Dictionary<string, object>()) {

		}

		/// <summary>Parameterized constructor for NameValueCollection.</summary>
		/// <param name="collection">A NameValueCollection.</param>
		public DynamicCollection(NameValueCollection collection) {
			var dictionary = collection.AllKeys.ToDictionary(k => k, v => (object) collection[v]);
			_collection = new Dictionary<string, object>(dictionary);
		}

		/// <summary>Parameterized constructor for IDictionary.</summary>
		/// <param name="dictionary">An IDictionary collection.</param>
		public DynamicCollection(IDictionary<string, object> dictionary) {
			_collection = new Dictionary<string, object>(dictionary);
		}

		/// <summary>Parameterized constructor for IDataReader.</summary>
		/// <param name="reader">An IDataReader.</param>
		/// <remarks>Close reader after this is used and/or called.</remarks>
		public DynamicCollection(IDataReader reader) {
			_collection = new Dictionary<string, object>();
			var fieldCount = reader.FieldCount;
			var row = 0;
			while (reader.Read()) {
				++row;
				var rowstring = string.Format("_Row{0}", row);
				for (var i = 0; i < fieldCount; i++) {
					var name = reader.GetName(i);
					_collection[name + rowstring] = reader[name];
				}
			}
		}

		/// <summary>
		/// Provides the implementation for operations that get member values. Classes derived from the
		/// <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as getting
		/// a value for a property.
		/// </summary>
		/// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name
		/// of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty)
		/// statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class,
		/// binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
		/// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property
		/// value to <paramref name="result" />.</param>
		/// <returns>
		/// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the
		/// behavior. (In most cases, a run-time exception is thrown.)
		/// </returns>
		public override bool TryGetMember(GetMemberBinder binder, out object result) {
			var name = binder.Name;
			return _collection.TryGetValue(name, out result);
		}

		/// <summary>
		/// Provides the implementation for operations that set member values. Classes derived from the
		/// <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as setting
		/// a value for a property.
		/// </summary>
		/// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name
		/// of the member to which the value is being assigned. For example, for the statement sampleObject.SampleProperty = "Test", where
		/// sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns
		/// "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
		/// <param name="value">The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where sampleObject is an
		/// instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, the <paramref name="value" /> is "Test".</param>
		/// <returns>
		/// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the
		/// behavior. (In most cases, a language-specific run-time exception is thrown.)
		/// </returns>
		public override bool TrySetMember(SetMemberBinder binder, object value) {
			_collection[binder.Name] = value;
			return true;
		}

		/// <summary>Returns the enumeration of all dynamic member names.</summary>
		/// <returns>A sequence that contains dynamic member names.</returns>
		public override IEnumerable<string> GetDynamicMemberNames() {
			return _collection.Keys;
		}

		/// <summary>Adds a property to 'value'.</summary>
		/// <typeparam name="T">Generic type parameter.</typeparam>
		/// <param name="key">.</param>
		/// <param name="value">.</param>
		public void AddProperty<T>(string key, T value = default(T)) {
			_collection[key] = value;
		}

		/// <summary>Adds a property.</summary>
		/// <param name="typeName">.</param>
		/// <param name="key">.</param>
		/// <param name="value">.</param>
		public void AddProperty(string typeName, string key, object value = null) {
			var type = Type.GetType(typeName);
			if (type == null) return;
			_collection[key] = Convert.ChangeType(value, type);
		}
	}
}