using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Bravo_Taksi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.ApiKey = "AAPK05e27ec62f684fca9536efb199173f63ts0YikM_OWvJ4Qf8plcRw_Y_S1UpNCZOi30Cb1poRfRE1zasQxfgFKycBg0R9uVr";
        }
    }
}
