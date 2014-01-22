using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Chain.Interfaces;

namespace Chain.Tests
{
	class TestTarget
	{
		public TestTarget()
		{

			var assemblies = GetAssemblies().ToArray();
			Compiler = CreateInstance<ICompiler>(assemblies);
			Interpreter = CreateInstance<IInterpreter>(assemblies);
			Lexer = CreateInstance<ILexer>(assemblies);
			Parser = CreateInstance<IParser>(assemblies);

			
		}

        public ICompiler Compiler { get; private set; }
		public IInterpreter Interpreter { get; private set; }
		public ILexer Lexer { get; private set; }
		public IParser Parser { get; private set; }
		

		static IEnumerable<Assembly> GetAssemblies()
		{
			Assembly thisassembly = Assembly.GetExecutingAssembly();
			var directory = Path.GetDirectoryName(thisassembly.Location);
			foreach (var file in Directory.GetFiles(directory))
			{
				Assembly assembly = null;
				try
				{
					assembly = Assembly.LoadFile(file);
				}
				catch (Exception ex)
				{
					assembly = null;
					Trace.TraceInformation(ex.ToString());
				}
				if (assembly != null)
				{
					yield return assembly;
				}
			}
		}
		static IEnumerable<Type> GetTypesImplementing(Assembly assembly, Type @interface)
		{
			return assembly.GetTypes().Where(t => t.IsClass && @interface.IsAssignableFrom(t));
		}
		static T CreateInstance<T>(Assembly[] assemblies)
		{
			Type type = assemblies.SelectMany(a => GetTypesImplementing(a, typeof(T))).FirstOrDefault();
			if (type == null) return default(T);
			return (T)Activator.CreateInstance(type);
		}
	}
}
