using System.Collections.Generic;

namespace Framework.Core.Interfaces
{
	/// <summary>Interface for translator.</summary>
	/// <typeparam name="TDataModel">Type of the data model.</typeparam>
	/// <typeparam name="TDomainModel">Type of the domain model.</typeparam>
	public interface ITranslator<TDataModel, TDomainModel>
		where TDataModel : class, new()
		where TDomainModel : class, new()
	{
		/// <summary>Enumerates convert in this collection.</summary>
		/// <param name="model">The model.</param>
		/// <returns>An enumerator that allows foreach to be used to process convert in this collection.</returns>
		TDataModel Translate(TDomainModel model);

		/// <summary>Enumerates convert in this collection.</summary>
		/// <param name="model">The model.</param>
		/// <returns>An enumerator that allows foreach to be used to process convert in this collection.</returns>
		TDomainModel Translate(TDataModel model);

		/// <summary>Enumerates convert in this collection.</summary>
		/// <param name="models">The models.</param>
		/// <returns>An enumerator that allows foreach to be used to process convert in this collection.</returns>
		IEnumerable<TDataModel> Translate(IEnumerable<TDomainModel> models);

		/// <summary>Enumerates convert in this collection.</summary>
		/// <param name="models">The models.</param>
		/// <returns>An enumerator that allows foreach to be used to process convert in this collection.</returns>
		IEnumerable<TDomainModel> Translate(IEnumerable<TDataModel> models);
	}
}