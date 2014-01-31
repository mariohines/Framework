using System;
using System.Transactions;
using PostSharp.Aspects;

namespace Framework.Data.Aspects
{
	/// <summary>Transaction aspect.</summary>
	[Serializable]
	public class TransactionAspect : OnMethodBoundaryAspect
	{
		/// <summary>The transaction scope.</summary>
		[NonSerialized]
		private TransactionScope _scope;

		/// <summary>Method executed <b>before</b> the body of methods to which this aspect is applied.</summary>
		/// <param name="args">Event arguments specifying which method is being executed, which are its arguments, and how should the execution
		/// continue after the execution of
		/// <see cref="M:PostSharp.Aspects.IOnMethodBoundaryAspect.OnEntry(PostSharp.Aspects.MethodExecutionArgs)" />.</param>
		public override void OnEntry(MethodExecutionArgs args) {
			_scope = new TransactionScope(TransactionScopeOption.RequiresNew);
			base.OnEntry(args);
		}

		/// <summary>
		/// Method executed <b>after</b> the body of methods to which this aspect is applied, but only when the method successfully returns (i.e.
		/// when no exception flies out the method.).
		/// </summary>
		/// <param name="args">Event arguments specifying which method is being executed and which are its arguments.</param>
		public override void OnSuccess(MethodExecutionArgs args) {
			_scope.Complete();
			base.OnSuccess(args);
		}

		/// <summary>
		/// Method executed <b>after</b> the body of methods to which this aspect is applied, even when the method exists with an exception (this
		/// method is invoked from the <c>finally</c> block).
		/// </summary>
		/// <param name="args">Event arguments specifying which method is being executed and which are its arguments.</param>
		public override void OnExit(MethodExecutionArgs args) {
			_scope.Dispose();
			base.OnExit(args);
		}

		/// <summary>
		/// Method executed <b>after</b> the body of methods to which this aspect is applied, in case that the method resulted with an exception.
		/// </summary>
		/// <param name="args">Event arguments specifying which method is being executed and which are its arguments.</param>
		public override void OnException(MethodExecutionArgs args) {
			args.FlowBehavior = FlowBehavior.Continue;
			Transaction.Current.Rollback(args.Exception);
			base.OnException(args);
		}
	}
}