// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionBuilder.cs" >
// </copyright>
// <summary>
//   Extension methods for add And and Or with parameters rebinder
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Core.Builders
{
	/// <summary>
	/// Class used to build Expressions for Specifications.
	/// </summary>
	public class ExpressionBuilder<TEntity>
	{
		private Expression<Func<TEntity, bool>> _exp;

		/// <summary>Default constructor.</summary>
		public ExpressionBuilder() {
			_exp = null;
		}

		/// <summary>Constructor.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <param name="expression">The expression.</param>
		public ExpressionBuilder(Expression<Func<TEntity, bool>> expression) {
			if (expression == null) {
				throw new ArgumentNullException("expression");
			}

			_exp = expression;
		}

		/// <summary>Ands the given expression.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <param name="expression">The expression.</param>
		public void And(Expression<Func<TEntity, bool>> expression) {
			if (expression == null) {
				throw new ArgumentNullException("expression");
			}

			_exp = _exp == null ? expression : Compose(_exp, expression, Expression.And);
		}

		/// <summary>And also.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <param name="expression">The expression.</param>
		public void AndAlso(Expression<Func<TEntity, bool>> expression) {
			if (expression == null) {
				throw new ArgumentNullException("expression");
			}
			_exp = _exp == null ? expression : Compose(_exp, expression, Expression.AndAlso);
		}

		/// <summary>Ors the given expression.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <param name="expression">The expression.</param>
		public void Or(Expression<Func<TEntity, bool>> expression) {
			if (expression == null) {
				throw new ArgumentNullException("expression");
			}
			_exp = _exp == null ? expression : Compose(_exp, expression, Expression.Or);
		}

		/// <summary>Or else.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <param name="expression">The expression.</param>
		public void OrElse(Expression<Func<TEntity, bool>> expression) {
			if (expression == null) {
				throw new ArgumentNullException("expression");
			}
			_exp = _exp == null ? expression : Compose(_exp, expression, Expression.OrElse);
		}

		/// <summary>Not this object.</summary>
		public void Not() {
			if (_exp != null) {
				_exp = Not(_exp);
			}
		}

		/// <summary>Converts this object to an expression.</summary>
		/// <returns>This object as an Expression&lt;Func&lt;TEntity,bool&gt;&gt;</returns>
		public Expression<Func<TEntity, bool>> ToExpression() {
			return _exp;
		}

		/// <summary>
		/// Not operator
		/// </summary>
		/// <typeparam name="T">Type of param in expression</typeparam>
		/// <param name="first">Right expression in OR operation</param>
		/// <returns>New Or expressions</returns>
		private static Expression<Func<T, bool>> Not<T>(Expression<Func<T, bool>> first) {
			return Expression.Lambda<Func<T, bool>>(Expression.Not(first.Body), first.Parameters.Single());
		}

		/// <summary>
		/// Compose two expression and merge all in a new expression
		/// </summary>
		/// <typeparam name="T">Type of params in expression</typeparam>
		/// <param name="first">Expression instance</param>
		/// <param name="second">Expression to merge</param>
		/// <param name="merge">Function to merge</param>
		/// <returns>New merged expressions</returns>
		private static Expression<T> Compose<T>(Expression<T> first, Expression<T> second,
		                                        Func<Expression, Expression, Expression> merge) {
			// build parameter map (from parameters of second to parameters of first)
			// var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
			var map = new Dictionary<ParameterExpression, ParameterExpression>();
			for (var i = 0; i < first.Parameters.Count; i++) {
				map.Add(second.Parameters[i], first.Parameters[i]);
			}

			// replace parameters in the second lambda expression with parameters from the first
			var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

			// apply composition of lambda expression bodies to parameters from the first expression 
			return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
		}
	}
}