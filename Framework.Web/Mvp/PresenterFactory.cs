using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Framework.Core.IoC;
using Framework.Core.IoC.Ninject;
using Framework.Web.Mvp.Interface;

namespace Framework.Web.Mvp
{
	/// <summary>The factory to create a presenter.</summary>
	public class PresenterFactory<TPresenter>
		where TPresenter : class, IPresenter
	{
		/// <summary>Creates a presenter.</summary>
		/// <param name="view">The view.</param>
		/// <returns>The new presenter.</returns>
		public static TPresenter CreatePresenter(Page view) {
			var viewType = view.GetType();
			var constructor = typeof (TPresenter).GetConstructors().First();
			var parameters = constructor.GetParameters().ToList();
			var arguments = new List<object>(parameters.Count);
			parameters.ForEach(parameter =>
							   {
								   if (parameter.ParameterType.IsAssignableFrom(viewType)) {
									   arguments.Add(view);
								   }
								   else {
									   var type = parameter.ParameterType;
									   var binding = GenericIocManager.IsInUse 
										   ? GenericIocManager.GetBindingOfType(type) 
										   : NinjectManager.GetBindingOfType(type);
									   arguments.Add(binding);
								   }
							   });
			return constructor.Invoke(arguments.ToArray()) as TPresenter;
		}
	}
}