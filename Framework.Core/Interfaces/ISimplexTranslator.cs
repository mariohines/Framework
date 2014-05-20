using System.Collections.Generic;

namespace Framework.Core.Interfaces
{
	/// <summary>Interface for one-way translation of either domain to data or data to domain models.</summary>
	/// <typeparam name="TDataModel">Type of the data model.</typeparam>
	/// <typeparam name="TDomainModel">Type of the domain model.</typeparam>
	public interface ISimplexTranslator<out TDataModel, in TDomainModel>
	{
		/// <summary>Translates the domain model into a data model.</summary>
		/// <param name="model">The model.</param>
		/// <returns>The translated data model.</returns>
		TDataModel Translate(TDomainModel model);

		/// <summary>Translates a collection of domain models into a collection of data models.</summary>
		/// <param name="models">The models.</param>
		/// <returns>A collection of translated data models.</returns>
		IEnumerable<TDataModel> Translate(IEnumerable<TDomainModel> models);
	}
}