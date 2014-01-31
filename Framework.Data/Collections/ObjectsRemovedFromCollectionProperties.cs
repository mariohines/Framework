using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Framework.Data.Collections
{
	/// <summary>Objects removed from collection properties.</summary>
	[CollectionDataContract(Name = "ObjectsRemovedFromCollectionProperties",
				ItemName = "DeletedObjectsForProperty", KeyName = "CollectionPropertyName", ValueName = "DeletedObjects")]
	public class ObjectsRemovedFromCollectionProperties : Dictionary<string, ObjectList> { }
}