// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Specification.cs" >
// </copyright>
// <summary>
//   Represent a Specification.
//   <remarks>
//   Base class for Specification pattern, for more information
//   about this pattern see http://martinfowler.com/apsupp/spec.pdf
//   or http://en.wikipedia.org/wiki/Specification_pattern.
//   This implementation of the Specification pattern is meant to be used with linq,
//   for this reason it implements specifications as Lambda expressions. 
//   </remarks>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Framework.Builders;
using Framework.Data.Interfaces;

namespace Framework.Data.Specifications
{
	/// <summary>Represent a Specification.</summary>
	/// <remarks>This abstract class implements the AND, OR and NOT methods as well as overloading the corresponding operators.</remarks>
	/// <typeparam name="TEntity">Type of item in the criteria.</typeparam>
	public class FilterSpecification<TEntity>
		: ISpecification<TEntity>
		where TEntity : class, IObjectWithChangeTracker, new()
	{
		/// <summary>Internal representation of this Specification as Lambda Expression.</summary>
		private readonly Expression<Func<TEntity, bool>> _expression;

		/// <summary>Delegate to compiled lambda expression.</summary>
		private Func<TEntity, bool> _compiledExpression;

		#region constructors

		/// <summary>Initializes a new instance of the <see cref="FilterSpecification{TEntity}"/> class.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <param name="expression">Lambda Expression to be used for this specification.</param>
		public FilterSpecification(Expression<Func<TEntity, bool>> expression) {
			if (expression == null) {
				throw new ArgumentNullException("expression");
			}

			_expression = expression;
		}

		#endregion

		/// <summary>And operator.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <param name="leftSideFilterSpecification">Left side operand in this OR operation.</param>
		/// <param name="rightSideFilterSpecification">Right side operand in this OR operation.</param>
		/// <returns>New AND specification.</returns>
		public static FilterSpecification<TEntity> operator &(
			FilterSpecification<TEntity> leftSideFilterSpecification, FilterSpecification<TEntity> rightSideFilterSpecification) {
			if (leftSideFilterSpecification == null) {
				throw new ArgumentNullException("leftSideFilterSpecification");
			}

			if (rightSideFilterSpecification == null) {
				throw new ArgumentNullException("rightSideFilterSpecification");
			}

			return leftSideFilterSpecification.And(rightSideFilterSpecification);
		}

		/// <summary>Or operator.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <param name="leftSideFilterSpecification">Left side operand in this OR operation.</param>
		/// <param name="rightSideFilterSpecification">Right side operand in this OR operation.</param>
		/// <returns>New OR specification.</returns>
		public static FilterSpecification<TEntity> operator |(
			FilterSpecification<TEntity> leftSideFilterSpecification, FilterSpecification<TEntity> rightSideFilterSpecification) {
			if (leftSideFilterSpecification == null) {
				throw new ArgumentNullException("leftSideFilterSpecification");
			}

			if (rightSideFilterSpecification == null) {
				throw new ArgumentNullException("rightSideFilterSpecification");
			}

			return leftSideFilterSpecification.Or(rightSideFilterSpecification);
		}

		/// <summary>Not specification.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <param name="filterSpecification">Specification to negate.</param>
		/// <returns>New NOT specification.</returns>
		public static FilterSpecification<TEntity> operator !(FilterSpecification<TEntity> filterSpecification) {
			if (filterSpecification == null) {
				throw new ArgumentNullException("filterSpecification");
			}

			return filterSpecification.Not();
		}

		/// <summary>Override operator false, only for support AND OR operators.</summary>
		/// <param name="filterSpecification">Specification instance.</param>
		/// <returns>See False operator in C#.</returns>
		public static bool operator false(FilterSpecification<TEntity> filterSpecification) {
			// must return false so that conditional operators && and || work as expected.
			return false;
		}

		/// <summary>Override operator True, only for support AND OR operators.</summary>
		/// <param name="filterSpecification">Specification instance.</param>
		/// <returns>See True operator in C#.</returns>
		public static bool operator true(FilterSpecification<TEntity> filterSpecification) {
			// must return false so that conditional operators && and || work as expected.
			return false;
		}

		/// <summary>Returns the Internal Lambda Expression for this Specification.</summary>
		/// <returns>Internal Lambda Expression for this Specification.</returns>
		public Expression<Func<TEntity, bool>> Expression() {
			return _expression;
		}

		/// <summary>Test if candidate satisfies this specification.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <param name="candidate">Candidate to test.</param>
		/// <returns>A value indicating whether the candidate satisfies the specification.</returns>
		public bool IsSatisfiedBy(TEntity candidate) {
			if (candidate == null) {
				throw new ArgumentNullException("candidate");
			}

			// Compile expression if needed.
			if (_compiledExpression == null) {
				_compiledExpression = _expression.Compile();
			}

			// call delegate for lambda expression.
			return _compiledExpression(candidate);
		}

		/// <summary>ANDs a new specification with this specification.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <param name="other">The specfication to And with this specification.</param>
		/// <returns>Returns the results of the AND operation.</returns>
		public FilterSpecification<TEntity> And(FilterSpecification<TEntity> other) {
			if (other == null) {
				throw new ArgumentNullException("other");
			}

			// Build new Expression
			var expressionBuilder = new ExpressionBuilder<TEntity>(Expression());
			expressionBuilder.And(other.Expression());

			// Buid new Specification using the new Expression.
			return new FilterSpecification<TEntity>(expressionBuilder.ToExpression());
		}

		/// <summary>ORs a new specification with this specification.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <param name="other">The specfication to OR with this specification.</param>
		/// <returns>Returns the results of the OR operation.</returns>
		public FilterSpecification<TEntity> Or(FilterSpecification<TEntity> other) {
			if (other == null) {
				throw new ArgumentNullException("other");
			}

			// Build new Expression
			var expressionBuilder = new ExpressionBuilder<TEntity>(Expression());
			expressionBuilder.Or(other.Expression());

			// Buid new Specification using the new Expression.
			return new FilterSpecification<TEntity>(expressionBuilder.ToExpression());
		}

		/// <summary>Negates the current Specification.</summary>
		/// <returns>Returns the results of the NOT operation on this Specification.</returns>
		public FilterSpecification<TEntity> Not() {
			// Build new Expression
			var expressionBuilder = new ExpressionBuilder<TEntity>(Expression());
			expressionBuilder.Not();

			// Buid new Specification using the new Expression.
			return new FilterSpecification<TEntity>(expressionBuilder.ToExpression());
		}

		/// <summary>Convert expression to String representation.</summary>
		/// <returns>A string representation of this Specifications.</returns>
		public override string ToString() {
			return _expression.Body.ToString();
		}
	}
}