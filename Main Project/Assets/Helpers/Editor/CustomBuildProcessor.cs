using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Helpers
{
    class CustomBuildProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder { get {  return 0; } }

        public void OnPreprocessBuild(BuildReport report)
        {
            var db = Resources.Load<Database>("Database");
            db.SetItemIDs();
            Debug.Log("MyCustomBuildProcessor.OnPreprocessBuild for target "
                + report.summary.platform + " at path " + report.summary.outputPath);
        }
    }
}
