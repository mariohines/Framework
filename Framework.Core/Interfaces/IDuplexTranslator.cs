﻿using System.Collections.Generic;

namespace Framework.Core.Interfaces
{
	/// <summary>Interface for bi-directional translation of data and domain models.</summary>
	/// <typeparam name="TDataModel">Type of the data model.</typeparam>
	/// <typeparam name="TDomainModel">Type of the domain model.</typeparam>
	public interface IDuplexTranslator<TDataModel, TDomainModel> : ISimplexTranslator<TDataModel, TDomainModel>
	{
		/// <summary>Translates the data model into a domain model.</summary>
		/// <param name="model">The model.</param>
		/// <returns>The translated domain model.</returns>
		TDomainModel Translate(TDataModel model);

		/// <summary>Translates a collection of data models into a collection of domain models.</summary>
		/// <param name="models">The models.</param>
		/// <returns>A collection of translated domain models.</returns>
		IEnumerable<TDomainModel> Translate(IEnumerable<TDataModel> models);
	}
}