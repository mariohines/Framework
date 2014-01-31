using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Framework.Interfaces;
using LinqKit;

namespace Framework.Extensions
{
	public static partial class Extensions
	{
		/// <summary>Enumerates across the assemble to get interfaces in this collection.</summary>
		/// <param name="assembly">The assembly to act on.</param>
		/// <returns>An enumerator that allows foreach to be used to process get interfaces in this collection.</returns>
		public static IEnumerable<Type> GetInterfaces(this Assembly assembly) {
			IEnumerable<Type> types;
			try {
				types = assembly.GetTypes().Where(t => t.IsInterface);
			}
			catch (Exception) {
				types = Enumerable.Empty<Type>();
			}
			return types;
		}

		/// <summary>An Assembly extension method that gets an interface implementations.</summary>
		/// <param name="assembly">The assembly to act on.</param>
		/// <param name="interface">The interface.</param>
		/// <returns>The interface implementations.</returns>
		public static IList<Type> GetInterfaceImplementations(this Assembly assembly, Type @interface) {
			IList<Type> implementations;
			try {
				implementations = assembly.GetTypes().Where(t => (@interface.IsAssignableFrom(t) || t.IsAssignableFrom(@interface)) && !t.IsAbstract).ToList();
			}
			catch (Exception) {
				implementations = Enumerable.Empty<Type>().ToList();
			}
			
			return implementations;
		}

		/// <summary>An Assembly extension method that gets an interface implementations.</summary>
		/// <param name="assemblies">The assemblies to act on.</param>
		/// <param name="interface">The interface.</param>
		/// <returns>The interface implementations.</returns>
		public static IList<Type> GetInterfaceImplementations(this IList<Assembly> assemblies, Type @interface) {
			var types = new List<Type>();
			assemblies.ForEach(asm =>
							   {
								   try {
									   types.AddRange(asm.GetTypes().Where(t => @interface.IsAssignableFrom(t) && !t.IsAbstract));
								   }
								   catch (Exception) {
									   return;
								   }
							   });
			return types;
		}

		/// <summary>An Assembly extension method that bind assembly.</summary>
		/// <param name="assembly">The assembly to act on.</param>
		/// <param name="injector">The interfaces.</param>
		public static void BindAssembly(this Assembly assembly, IDependencyInjector injector) {
			var interfaces = assembly.GetInterfaces();
			foreach (var @interface in interfaces) {
				var currentInterface = @interface;
				var implementations = assembly.GetInterfaceImplementations(currentInterface).ToList();
				if (!implementations.Any()) continue;
				var inheritors = currentInterface.GetInterfaces().Where(i => i.Assembly.GetName().Name.StartsWith("System")).ToList();
				implementations.ForEach(i =>
										{
											injector.Bind(currentInterface, i);
											inheritors.ForEach(inheritor => injector.Bind(inheritor, i));
										});
			}
		}

		/// <summary>An IList&lt;Assembly&gt; extension method that bind assemblies.</summary>
		/// <param name="assemblies">The assemblies to act on.</param>
		/// <param name="injector">The interfaces.</param>
		public static void BindAssemblies(this IList<Assembly> assemblies, IDependencyInjector injector) {
			var interfaces = assemblies.SelectMany(a => a.GetInterfaces()).ToList();
			foreach (var @interface in interfaces) {
				var currentInterface = @interface;
				var implementations = assemblies.GetInterfaceImplementations(currentInterface).ToList();
				if (!implementations.Any()) continue;
				var inheritors = currentInterface.GetInterfaces().Where(i => i.Assembly.GetName().Name.StartsWith("System")).ToList();
				implementations.ForEach(i =>
										{
											injector.Bind(currentInterface, i);
											inheritors.ForEach(inheritor => injector.Bind(inheritor, i));
										});
			}
		}
	}
}