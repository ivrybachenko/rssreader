using Ninject.Modules;
using Ninject;
using System.IO;
using System;
using RSSreader.Commands;
using RSSreader.Services;

namespace RSSreader{

    public class Bindings : NinjectModule
    {

        public override void Load()
        {
            Bind<IEnvironment>().To<RealEnvironment>().InSingletonScope();
            Bind<IAppDirBuilder>().To<MyDocumentsAppDirBuilder>().InSingletonScope();
            Bind<IConfigManager>().To<ConfigManager>().InSingletonScope();
            Bind<IRSSStorage>().To<RSSStorage>().InSingletonScope();;
            Bind<ICommandEngine>().To<CommandEngine>().InSingletonScope();
            Bind<IRSSLoader>().To<RSSLoader>().InSingletonScope();
            Bind<IFeedPrinter>().To<FeedPrinter>().InSingletonScope();
        }
    }

}