﻿// <copyright>
// Copyright 2013 by the Spark Development Network
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//

using System;
using System.Configuration;
using System.Reflection;
using System.Windows;

namespace Excavator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        [System.STAThreadAttribute()]
        public static void Main()
        {
            Excavator.App app = new Excavator.App();
            string embedPath = ConfigurationManager.AppSettings["EmbeddedResources"];

            AppDomain.CurrentDomain.AssemblyResolve += ( sender, args ) =>
            {
                string resourceName = embedPath + "." + new AssemblyName( args.Name ).Name + ".dll";
                using ( var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream( resourceName ) )
                {
                    Byte[] assemblyData = new Byte[stream.Length];

                    stream.Read( assemblyData, 0, assemblyData.Length );

                    return Assembly.Load( assemblyData );
                }
            };

            app.InitializeComponent();

            app.Run();
        }
    }
}
