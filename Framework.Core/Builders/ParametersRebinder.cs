﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParametersRebinder.cs" >
// </copyright>
// <summary>
//   Helper class for rewritting epressions. 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Core.Builders
{
	/// <summary>
	/// Helper class for rewritting epressions. 
	/// </summary>
	public class ParameterRebinder : ExpressionVisitor
	{
		/// <summary>
		/// Expression parameters map.
		/// </summary>
		private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

		/// <summary>
		/// Initializes a new instance of the <see cref="ParameterRebinder"/> class. 
		/// </summary>
		/// <param name="map">
		/// Map specification
		/// </param>
		public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map) {
			_map = map;
		}

		/// <summary>
		/// Replate parameters in expression with a Map information
		/// </summary>
		/// <param name="map">Map information</param>
		/// <param name="exp">Expression to replace parameters</param>
		/// <returns>Expression with parameters replaced</returns>
		public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp) {
			return new ParameterRebinder(map).Visit(exp);
		}

		/// <summary>
		/// Visit pattern method
		/// </summary>
		/// <param name="p">A Parameter expression</param>
		/// <returns>New visited expression</returns>
		protected override Expression VisitParameter(ParameterExpression p) {
			ParameterExpression replacement;
			if (_map.TryGetValue(p, out replacement)) {
				p = replacement;
			}

			return base.VisitParameter(p);
		}
	}
}