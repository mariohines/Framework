using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Framework.Data.Collections
{
	/// <summary>List of objects.</summary>
	[CollectionDataContract(ItemName = "ObjectValue")]
	public class ObjectList : List<object> { }
}