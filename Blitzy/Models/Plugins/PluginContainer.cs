﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Anotar.NLog;
using Blitzy.Models.Commands;
using Blitzy.PluginInterfaces;
using Blitzy.Utilities;
using Ninject;

namespace Blitzy.Models.Plugins
{
	internal interface IPluginContainer
	{
		Task LoadPlugins();

		Task UnloadPlugins();

		ICollection<IPlugin> LoadedPlugins { get; }
	}

	internal class PluginContainer : IPluginContainer
	{
		public PluginContainer( IPluginHost host, ICommandTree tree, ISettings settings )
		{
			Host = host;
			Settings = settings;

			CommandTree = tree;
			LoadedPlugins = new List<IPlugin>();
		}

		public async Task LoadPlugins()
		{
			LogTo.Info( "Loading plugins..." );

			Type pluginType = typeof( IPlugin );
			var dllFiles = FileSystem.ListFiles( "plugins", "*.dll" );

			foreach( var dll in dllFiles )
			{
				LogTo.Info( $"Trying to load plugins from {dll}" );

				IAssembly asm;
				try
				{
					asm = FileSystem.LoadAssemblyFromFile( Path.GetFullPath( dll ) );
				}
				catch( Exception ex )
				{
					LogTo.WarnException( $"Failed to load file {dll}", ex );
					continue;
				}

				var plugins = asm.GetTypes().Where( t => !t.IsAbstract && pluginType.IsAssignableFrom( t ) ).ToArray();
				foreach( var type in plugins )
				{
					LogTo.Info( $"Trying to load plugin {type}" );

					IPlugin plugin;
					try
					{
						plugin = TypeActivator.CreateInstance<IPlugin>( type );
					}
					catch( Exception ex )
					{
						LogTo.WarnException( $"Failed to create plugin type {type}", ex );
						continue;
					}

					try
					{
						await plugin.Load( Host );
					}
					catch( Exception ex )
					{
						LogTo.ErrorException( $"Failed to load plugin type {type}", ex );
						continue;
					}

					try
					{
						LoadPluginCommands( plugin );
					}
					catch( Exception ex )
					{
						LogTo.ErrorException( $"Failed to load commands from plugin {type}", ex );
						continue;
					}

					LoadedPlugins.Add( plugin );
				}
			}

			LogTo.Info( $"{LoadedPlugins.Count} plugins loaded" );
		}

		public async Task UnloadPlugins()
		{
			LogTo.Info( "Unloading all plugins" );
			await Task.WhenAll( LoadedPlugins.Select( p => p.Unload() ) );
		}

		private void LoadPluginCommands( IPlugin plugin )
		{
			if( Settings.StoreCommandsInRoot )
			{
				foreach( var node in plugin.GetNodes() )
				{
					CommandTree.InjectRoot( node );
				}
			}
			else
			{
				CommandTree.InjectRoot( new PluginCommandNodeRoot( plugin ) );
			}
		}

		[Inject]
		public IFileSystem FileSystem { get; set; }

		public ICollection<IPlugin> LoadedPlugins { get; }

		[Inject]
		public ITypeActivator TypeActivator { get; set; }

		private readonly ICommandTree CommandTree;
		private readonly IPluginHost Host;
		private readonly ISettings Settings;
	}
}