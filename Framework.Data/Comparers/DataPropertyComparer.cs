using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Data.Comparers
{
	///<summary>Data property comparer.</summary>
	///<typeparam name="TEntity">Type of the entity.</typeparam>
	public class DataPropertyComparer<TEntity> : IEqualityComparer<TEntity>
	{
		private readonly List<PropertyInfo> _propertyInfos;

		///<summary>Constructor.</summary>
		///<remarks>Mhines, 11/29/2012.</remarks>
		///<param name="properties">A variable-length parameters list containing properties.</param>
		public DataPropertyComparer(params string[] properties) {
			_propertyInfos = new List<PropertyInfo>();
			var typeProperties = typeof (TEntity).GetProperties().Where(p => properties.Contains(p.Name));
			_propertyInfos.AddRange(typeProperties);
		}

		///<summary>Tests if two TEntity objects are considered equal.</summary>
		///<param name="x">T entity to be compared.</param>
		///<param name="y">T entity to be compared.</param>
		///<returns>true if the objects are considered equal, false if they are not.</returns>
		public bool Equals(TEntity x, TEntity y) {
			var matches = new List<bool>();
			_propertyInfos.ForEach(p => {
				var xPropertyValue = p.GetValue(x).ToString();
				var yPropertyValue = p.GetValue(y).ToString();
				matches.Add(string.Equals(xPropertyValue, yPropertyValue, StringComparison.Ordinal));
			});
			return matches.All(m => m);
		}

		///<summary>Calculates the hash code for this object.</summary>
		///<param name="obj">The object.</param>
		///<returns>The hash code for this object.</returns>
		public int GetHashCode(TEntity obj) {
			var hashValue = 0;
			_propertyInfos.ForEach(p => {
				hashValue ^= p.GetValue(obj, null).GetHashCode();
			});
			return hashValue;
		}
	}
}